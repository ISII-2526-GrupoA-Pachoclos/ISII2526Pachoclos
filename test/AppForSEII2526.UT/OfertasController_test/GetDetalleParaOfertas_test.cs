using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AppForSEII2526.UT.OfertasController_test
{
    public class GetDetalleParaOferta_test : AppForSEII25264SqliteUT
    {
        public GetDetalleParaOferta_test()
        {
            // Datos de prueba: fabricantes
            var fabricantes = new List<fabricante>()
            {
                new fabricante { nombre = "Pepe" },
                new fabricante { nombre = "Ana" },
                new fabricante { nombre = "Luis" },
            };

            // Añadimos fabricantes y herramientas primero para que tengan ids
            var herramientas = new List<Herramienta>()
            {
                new Herramienta("Martillo", "Acero", 15.9f, "", fabricantes[0]),
                new Herramienta("Destornillador", "Acero", 5.5f, "", fabricantes[1]),
                new Herramienta("Brocas", "Metal", 8.0f, "", fabricantes[2])
            };

            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.SaveChanges();

            // Ahora creamos los OfertaItem vinculados a las herramientas ya persistidas
            var ofertaItems = new List<OfertaItem>()
            {
                new OfertaItem { porcentaje = 25, precioFinal = 11.93f, herramienta = herramientas[0], herramientaid = herramientas[0].id },
                new OfertaItem { porcentaje = 10, precioFinal = 4.95f, herramienta = herramientas[1], herramientaid = herramientas[1].id },
                new OfertaItem { porcentaje = 20, precioFinal = 6.4f, herramienta = herramientas[2], herramientaid = herramientas[2].id }
            };

            // Creamos la oferta
            var oferta = new Oferta
            {
                fechaInicio = DateTime.UtcNow.AddDays(30),
                fechaFin = DateTime.UtcNow.AddDays(60),
                fechaOferta = DateTime.UtcNow,
                metodoPago = tiposMetodoPago.Efectivo,
                paraSocio = tiposDirigidaOferta.Clientes,
                ofertaItems = ofertaItems
            };

            // Añadimos oferta (con sus items) y guardamos
            _context.Add(oferta);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_GetDetalleParaOferta_Ok()
        {
            // Debe coincidir con lo que se inserta en el constructor:
            var ofertaItemsDTO = new List<OfertaItemDTO>()
            {
                new OfertaItemDTO(1, "Martillo", "Acero", "Pepe", 15.9f, 11.93f),
                new OfertaItemDTO(2, "Destornillador", "Acero", "Ana", 5.5f, 4.95f),
                new OfertaItemDTO(3, "Brocas", "Metal", "Luis", 8.0f, 6.40f),
            };

            var ofertaDetalleEsperada = new OfertaDetalleDTO(
                1,
                DateTime.UtcNow.AddDays(30),
                DateTime.UtcNow.AddDays(60),
                DateTime.UtcNow,
                tiposMetodoPago.Efectivo,
                tiposDirigidaOferta.Clientes,
                ofertaItemsDTO
            );

            return new List<object[]>
            {
                new object[] { 1, ofertaDetalleEsperada }
            };
        }

        [Theory]
        [MemberData(nameof(TestCasesFor_GetDetalleParaOferta_Ok))]
        public async Task GetDetalleParaOferta_Ok(int ofertaId, OfertaDetalleDTO expectedDTO)
        {
            // Arrange
            var controller = new OfertasController(_context, null);

            // Act
            var actionResult = await controller.GetDetalles_Oferta(ofertaId);

            // Assert: 200 OK y DTO
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var value = okResult.Value!;

            // Si controller devuelve { detalle = ofertas, precioTotal = n }, extraer detalle
            OfertaDetalleDTO actualDTO;
            var detalleProp = value.GetType().GetProperty("detalle");
            if (detalleProp != null)
            {
                actualDTO = Assert.IsType<OfertaDetalleDTO>(detalleProp.GetValue(value));
            }
            else
            {
                // Si la acción devolviera directamente el DTO
                actualDTO = Assert.IsType<OfertaDetalleDTO>(value);
            }

            // Después usar actualDTO como antes
            Assert.Equal(expectedDTO.Id, actualDTO.Id);
            Assert.Equal(expectedDTO.metodoPago, actualDTO.metodoPago);
            Assert.Equal(expectedDTO.tiposDirigidaOferta, actualDTO.tiposDirigidaOferta);

            // Comparar items (ignorar posibles diferencias menores en timestamps)
            Assert.Equal(expectedDTO.HerramientasAOfertar.Count, actualDTO.HerramientasAOfertar.Count);

            foreach (var esperado in expectedDTO.HerramientasAOfertar)
            {
                var match = actualDTO.HerramientasAOfertar.FirstOrDefault(a =>
                    string.Equals(a.nombre, esperado.nombre, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(a.material, esperado.material, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(a.fabricante, esperado.fabricante, StringComparison.OrdinalIgnoreCase) &&
                    Math.Abs(a.precio - esperado.precio) < 0.01f &&
                    Math.Abs(a.precioOferta - esperado.precioOferta) < 0.01f
                );

                Assert.NotNull(match);
            }
        }
    }
}

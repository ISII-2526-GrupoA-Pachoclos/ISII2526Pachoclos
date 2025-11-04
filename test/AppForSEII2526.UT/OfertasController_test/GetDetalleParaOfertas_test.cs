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
                FechaInicio = DateTime.UtcNow.AddDays(30),
                FechaFin = DateTime.UtcNow.AddDays(60),
                FechaOferta = DateTime.UtcNow,
                MetodoPago = tiposMetodoPago.Efectivo,
                ParaSocio = tiposDirigidaOferta.Clientes,
                OfertaItems = ofertaItems
            };

            // Añadimos oferta (con sus items) y guardamos
            _context.Add(oferta);
            _context.SaveChanges();
        }
        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetDetalleParaOferta_NotFound()
        {
            //Arrange
            var mock = new Mock<ILogger<OfertasController>>();
            ILogger<OfertasController> logger = mock.Object;

            var controller = new OfertasController(_context, logger);

            //Act
            var result = await controller.GetDetalles_Oferta(0); // ID de oferta que no existe (999)

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetDetalleParaOferta_Found_test()
        {
            //Arrange
            var mock = new Mock<ILogger<OfertasController>>();
            ILogger<OfertasController> logger = mock.Object;
            var controller = new OfertasController(_context, logger);

            var expectedOferta = new OfertaDetalleDTO(
                DateTime.UtcNow.AddDays(30),
                DateTime.UtcNow.AddDays(60),
                DateTime.UtcNow,
                tiposDirigidaOferta.Clientes,
                tiposMetodoPago.Efectivo,
                new List<OfertaItemDTO>()
            );
            expectedOferta.HerramientasAOfertar.Add(new OfertaItemDTO
            {
                Nombre = "Martillo",
                Material = "Acero",
                Fabricante = "Pepe",
                Precio = 15.9f,
                PrecioOferta = 11.93f
            });

            //Act
            var result = await controller.GetDetalles_Oferta(1);

            //Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            var ofertaDTOActual = Assert.IsType<OfertaDetalleDTO>(okResult.Value);
            var eq = expectedOferta.Equals(ofertaDTOActual);

            Assert.Equal(expectedOferta, ofertaDTOActual);
        }
    }
}

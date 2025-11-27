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
        private readonly int _ofertaId;
        private readonly DateTime _fechaInicio;
        private readonly DateTime _fechaFin;
        private readonly DateTime _fechaOferta;

        public GetDetalleParaOferta_test()
        {
            // Usar fechas explícitas sin hora para evitar problemas con SQLite
            _fechaInicio = new DateTime(2024, 1, 1);
            _fechaFin = new DateTime(2024, 12, 31);
            _fechaOferta = new DateTime(2024, 1, 1);

            var fabricantes = new List<fabricante>()
            {
                new fabricante { id = 1, nombre = "Pepe" },
                new fabricante { id = 2, nombre = "Ana" },
                new fabricante { id = 3, nombre = "Luis" },
            };

            var herramientas = new List<Herramienta>()
            {
                new Herramienta { id = 1, nombre = "Martillo", material = "Acero", precio = 15.9f, tiempoReparacion = "1 día", fabricante = fabricantes[0] },
                new Herramienta { id = 2, nombre = "Destornillador", material = "Acero", precio = 5.5f, tiempoReparacion = "5 horas", fabricante = fabricantes[1] },
                new Herramienta { id = 3, nombre = "Brocas", material = "Metal", precio = 8.0f, tiempoReparacion = "30 minutos", fabricante = fabricantes[2] }
            };

            var usuario = new ApplicationUser
            {
                Id = "2",
                nombre = "Juan",
                apellido = "Pérez",
                correoElectronico = "juan@gmail.com",
                numTelefono = "123456789"
            };

            var oferta = new Oferta
            {
                Id = 1,
                fechaInicio = _fechaInicio,
                fechaFin = _fechaFin,
                fechaOferta = _fechaOferta,
                metodoPago = tiposMetodoPago.Efectivo,
                paraSocio = tiposDirigidaOferta.Clientes,
                ofertaItems = new List<OfertaItem>(),
                ApplicationUser = usuario
            };

            var ofertaItem = new OfertaItem
            {
                ofertaId = 1,
                herramientaid = 1,
                porcentaje = 25,
                precioFinal = 15.9f * 0.75f, // = 11.925f
                oferta = oferta,
                herramienta = herramientas[0]
            };

            oferta.ofertaItems.Add(ofertaItem);
            _ofertaId = oferta.Id;

            // Guardar en orden correcto para mantener relaciones
            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.ApplicationUser.Add(usuario);
            _context.Add(oferta);
            _context.Add(ofertaItem);
            _context.SaveChanges();
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetDetalleParaOferta_NotFound()
        {
            // Arrange
            var mock = new Mock<ILogger<OfertasController>>();
            var controller = new OfertasController(_context, mock.Object);

            // Act
            var result = await controller.GetDetallesOferta(999); // ID que no existe

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetDetalleParaOferta_Found_test()
        {
            // Arrange
            var mock = new Mock<ILogger<OfertasController>>();
            var controller = new OfertasController(_context, mock.Object);

            // Construir el DTO esperado
            var expectedOferta = new OfertaDetalleDTO(
                 _fechaInicio,
                 _fechaFin,
                 _fechaOferta,
                 "Juan",
                 tiposMetodoPago.Efectivo,
                 tiposDirigidaOferta.Clientes,
                 new List<OfertaItemDTO>
                 {
                    new OfertaItemDTO("Martillo", "Acero", "Pepe", 15.9f, 11.925f, 50) // ← Valor correcto
                 }
            );

            // Act
            var result = await controller.GetDetallesOferta(_ofertaId);

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            var ofertaDTOActual = Assert.IsType<OfertaDetalleDTO>(okResult.Value);

            // Comparar fechas (usar Date para evitar problemas de hora)
            Assert.Equal(expectedOferta.fechaInicio.Date, ofertaDTOActual.fechaInicio.Date);
            Assert.Equal(expectedOferta.fechaFin.Date, ofertaDTOActual.fechaFin.Date);
            Assert.Equal(expectedOferta.fechaOferta.Date, ofertaDTOActual.fechaOferta.Date);

            Assert.Equal(expectedOferta.nombreUsuario, ofertaDTOActual.nombreUsuario);
            Assert.Equal(expectedOferta.metodoPago, ofertaDTOActual.metodoPago);
            Assert.Equal(expectedOferta.tiposDirigidaOferta, ofertaDTOActual.tiposDirigidaOferta);

            // Comparar items
            Assert.Single(ofertaDTOActual.HerramientasAOfertar);

            var expectedItem = expectedOferta.HerramientasAOfertar[0];
            var actualItem = ofertaDTOActual.HerramientasAOfertar[0];

            Assert.Equal(expectedItem.nombre, actualItem.nombre);
            Assert.Equal(expectedItem.material, actualItem.material);
            Assert.Equal(expectedItem.fabricante, actualItem.fabricante);
            Assert.Equal(expectedItem.precio, actualItem.precio);
            Assert.Equal(expectedItem.precioOferta, actualItem.precioOferta);
        }
    }
}
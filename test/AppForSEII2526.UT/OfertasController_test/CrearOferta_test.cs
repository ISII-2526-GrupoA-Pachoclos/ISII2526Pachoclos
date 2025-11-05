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
    public class CrearOferta_test : AppForSEII25264SqliteUT
    {
        private readonly int _herramienta1Id;
        private readonly int _herramienta2Id;

        public CrearOferta_test()
        {
            // Setup inicial con herramientas y usuario
            var fabricantes = new List<fabricante>()
            {
                new fabricante { id = 1, nombre = "Pepe" },
                new fabricante { id = 2, nombre = "Ana" }
            };

            var herramientas = new List<Herramienta>()
            {
                new Herramienta { id = 1, nombre = "Martillo", material = "Acero", precio = 15.9f, fabricante = fabricantes[0] },
                new Herramienta { id = 2, nombre = "Destornillador", material = "Acero", precio = 5.5f, fabricante = fabricantes[1] },
                new Herramienta { id = 3, nombre = "Brocas", material = "Metal", precio = 8.0f, fabricante = fabricantes[0] }
            };

            var usuario = new ApplicationUser
            {
                Id = "1",
                nombre = "Admin",
                apellido = "Sistema",
                correoElectronico = "admin@test.com",
                numTelefono = "123456789"
            };

            _herramienta1Id = herramientas[0].id;
            _herramienta2Id = herramientas[1].id;

            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.ApplicationUser.Add(usuario);
            _context.SaveChanges();
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CrearOferta_ValidData_ReturnsCreated()
        {
            // Arrange
            var mock = new Mock<ILogger<OfertasController>>();
            var controller = new OfertasController(_context, mock.Object);

            var fechaInicio = DateTime.Today.AddDays(1);
            var fechaFin = DateTime.Today.AddDays(30);

            var crearOfertaDTO = new CrearOfertaDTO
            {
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                TiposMetodoPago = tiposMetodoPago.Tarjeta,
                TiposDirigidaOferta = tiposDirigidaOferta.Socios,
                CrearOfertaItem = new List<CrearOfertaItemDTO>
                {
                    new CrearOfertaItemDTO { herramientaid = _herramienta1Id, porcentaje = 25 },
                    new CrearOfertaItemDTO { herramientaid = _herramienta2Id, porcentaje = 10 }
                }
            };

            // Act
            var result = await controller.CrearOferta(crearOfertaDTO);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(OfertasController.GetDetalles_Oferta), createdResult.ActionName);

            var ofertaDetalle = Assert.IsType<OfertaDetalleDTO>(createdResult.Value);

            // Verificar fechas
            Assert.Equal(fechaInicio.Date, ofertaDetalle.fechaInicio.Date);
            Assert.Equal(fechaFin.Date, ofertaDetalle.fechaFin.Date);
            Assert.Equal(DateTime.Today, ofertaDetalle.fechaOferta.Date);

            // Verificar datos generales
            Assert.Equal("Admin", ofertaDetalle.nombreUsuario);
            Assert.Equal(tiposMetodoPago.Tarjeta, ofertaDetalle.metodoPago);
            Assert.Equal(tiposDirigidaOferta.Socios, ofertaDetalle.tiposDirigidaOferta);

            // Verificar herramientas
            Assert.Equal(2, ofertaDetalle.HerramientasAOfertar.Count);

            // Verificar primera herramienta
            var item1 = ofertaDetalle.HerramientasAOfertar.First(i => i.nombre == "Martillo");
            Assert.Equal("Acero", item1.material);
            Assert.Equal("Pepe", item1.fabricante);
            Assert.Equal(15.9f, item1.precio);
            Assert.Equal(15.9f * 0.75f, item1.precioOferta, 0.001f); // 25% descuento

            // Verificar segunda herramienta
            var item2 = ofertaDetalle.HerramientasAOfertar.First(i => i.nombre == "Destornillador");
            Assert.Equal("Acero", item2.material);
            Assert.Equal("Ana", item2.fabricante);
            Assert.Equal(5.5f, item2.precio);
            Assert.Equal(5.5f * 0.90f, item2.precioOferta, 0.001f); // 10% descuento

            // Verificar que se guardó en la base de datos
            var ofertaEnDb = _context.Oferta.FirstOrDefault();
            Assert.NotNull(ofertaEnDb);
            Assert.Equal(2, ofertaEnDb.ofertaItems.Count);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CrearOferta_InvalidStartDate_ReturnsBadRequest()
        {
            // Arrange
            var mock = new Mock<ILogger<OfertasController>>();
            var controller = new OfertasController(_context, mock.Object);

            var crearOfertaDTO = new CrearOfertaDTO
            {
                FechaInicio = DateTime.Today.AddDays(-1), // Fecha en pasado
                FechaFin = DateTime.Today.AddDays(30),
                TiposMetodoPago = tiposMetodoPago.Tarjeta,
                CrearOfertaItem = new List<CrearOfertaItemDTO>
                {
                    new CrearOfertaItemDTO { herramientaid = _herramienta1Id, porcentaje = 25 }
                }
            };

            // Act
            var result = await controller.CrearOferta(crearOfertaDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);
            Assert.Contains("FechaInicio", problemDetails.Errors.Keys);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CrearOferta_EndDateBeforeStartDate_ReturnsBadRequest()
        {
            // Arrange
            var mock = new Mock<ILogger<OfertasController>>();
            var controller = new OfertasController(_context, mock.Object);

            var crearOfertaDTO = new CrearOfertaDTO
            {
                FechaInicio = DateTime.Today.AddDays(10),
                FechaFin = DateTime.Today.AddDays(5), // Fin antes del inicio
                TiposMetodoPago = tiposMetodoPago.Tarjeta,
                CrearOfertaItem = new List<CrearOfertaItemDTO>
                {
                    new CrearOfertaItemDTO { herramientaid = _herramienta1Id, porcentaje = 25 }
                }
            };

            // Act
            var result = await controller.CrearOferta(crearOfertaDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);
            Assert.Contains("FechaFin", problemDetails.Errors.Keys);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CrearOferta_EmptyItems_ReturnsBadRequest()
        {
            // Arrange
            var mock = new Mock<ILogger<OfertasController>>();
            var controller = new OfertasController(_context, mock.Object);

            var crearOfertaDTO = new CrearOfertaDTO
            {
                FechaInicio = DateTime.Today.AddDays(1),
                FechaFin = DateTime.Today.AddDays(30),
                TiposMetodoPago = tiposMetodoPago.Tarjeta,
                CrearOfertaItem = new List<CrearOfertaItemDTO>() // Lista vacía
            };

            // Act
            var result = await controller.CrearOferta(crearOfertaDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);
            Assert.Contains("CrearOfertaItem", problemDetails.Errors.Keys);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CrearOferta_InvalidPercentage_ReturnsBadRequest()
        {
            // Arrange
            var mock = new Mock<ILogger<OfertasController>>();
            var controller = new OfertasController(_context, mock.Object);

            var crearOfertaDTO = new CrearOfertaDTO
            {
                FechaInicio = DateTime.Today.AddDays(1),
                FechaFin = DateTime.Today.AddDays(30),
                TiposMetodoPago = tiposMetodoPago.Tarjeta,
                CrearOfertaItem = new List<CrearOfertaItemDTO>
                {
                    new CrearOfertaItemDTO { herramientaid = _herramienta1Id, porcentaje = 0 }, // Porcentaje inválido
                    new CrearOfertaItemDTO { herramientaid = _herramienta2Id, porcentaje = 101 } // Porcentaje inválido
                }
            };

            // Act
            var result = await controller.CrearOferta(crearOfertaDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);
            Assert.Contains("CrearOfertaItem", problemDetails.Errors.Keys);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CrearOferta_NonExistentTool_ReturnsBadRequest()
        {
            // Arrange
            var mock = new Mock<ILogger<OfertasController>>();
            var controller = new OfertasController(_context, mock.Object);

            var crearOfertaDTO = new CrearOfertaDTO
            {
                FechaInicio = DateTime.Today.AddDays(1),
                FechaFin = DateTime.Today.AddDays(30),
                TiposMetodoPago = tiposMetodoPago.Tarjeta,
                CrearOfertaItem = new List<CrearOfertaItemDTO>
                {
                    new CrearOfertaItemDTO { herramientaid = 999, porcentaje = 25 } // Herramienta que no existe
                }
            };

            // Act
            var result = await controller.CrearOferta(crearOfertaDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);
            Assert.Contains("CrearOfertaItem", problemDetails.Errors.Keys);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CrearOferta_SingleItem_ReturnsCreated()
        {
            // Arrange
            var mock = new Mock<ILogger<OfertasController>>();
            var controller = new OfertasController(_context, mock.Object);

            var crearOfertaDTO = new CrearOfertaDTO
            {
                FechaInicio = DateTime.Today.AddDays(1),
                FechaFin = DateTime.Today.AddDays(15),
                TiposMetodoPago = tiposMetodoPago.PayPal,
                TiposDirigidaOferta = tiposDirigidaOferta.Clientes,
                CrearOfertaItem = new List<CrearOfertaItemDTO>
                {
                    new CrearOfertaItemDTO { herramientaid = _herramienta1Id, porcentaje = 50 } // 50% de descuento
                }
            };

            // Act
            var result = await controller.CrearOferta(crearOfertaDTO);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var ofertaDetalle = Assert.IsType<OfertaDetalleDTO>(createdResult.Value);

            Assert.Single(ofertaDetalle.HerramientasAOfertar);

            var item = ofertaDetalle.HerramientasAOfertar[0];
            Assert.Equal("Martillo", item.nombre);
            Assert.Equal(15.9f * 0.5f, item.precioOferta, 0.001f); // 50% descuento
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CrearOferta_MaximumPercentage_ReturnsCreated()
        {
            // Arrange
            var mock = new Mock<ILogger<OfertasController>>();
            var controller = new OfertasController(_context, mock.Object);

            var crearOfertaDTO = new CrearOfertaDTO
            {
                FechaInicio = DateTime.Today.AddDays(1),
                FechaFin = DateTime.Today.AddDays(30),
                TiposMetodoPago = tiposMetodoPago.PayPal,
                TiposDirigidaOferta = tiposDirigidaOferta.Socios,
                CrearOfertaItem = new List<CrearOfertaItemDTO>
                {
                    new CrearOfertaItemDTO { herramientaid = _herramienta1Id, porcentaje = 100 } // 100% de descuento
                }
            };

            // Act
            var result = await controller.CrearOferta(crearOfertaDTO);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var ofertaDetalle = Assert.IsType<OfertaDetalleDTO>(createdResult.Value);

            var item = ofertaDetalle.HerramientasAOfertar[0];
            Assert.Equal(0f, item.precioOferta, 0.001f); // Precio final 0 con 100% descuento
        }
    }
}
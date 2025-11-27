using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.OfertasController_test
{
    public class PostOfertas_test : AppForSEII25264SqliteUT
    {
        public PostOfertas_test()
        {
            var fabricantes = new List<fabricante>
            {
                new fabricante { id = 1, nombre = "Fabricante A" },
                new fabricante { id = 2, nombre = "Fabricante B" },
                new fabricante { id = 3, nombre = "Fabricante C" }
            };

            var herramientas = new List<Herramienta>
            {
                new Herramienta { id = 1, nombre = "Martillo", fabricante = fabricantes[0], material = "Acero", precio = 15.9f, tiempoReparacion = "2 dias" },
                new Herramienta { id = 2, nombre = "Destornillador", fabricante = fabricantes[1], material = "Acero", precio = 5.5f, tiempoReparacion = "1 dia" },
                new Herramienta { id = 3, nombre = "Brocas", fabricante = fabricantes[2], material = "Metal", precio = 8.0f, tiempoReparacion = "3 dias" }
            };

            var administradores = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1", nombre = "Admin", apellido = "Sistema", correoElectronico = "admin@test.com", numTelefono = "123456789" }
            };

            var ofertaItems = new List<OfertaItem>
            {
                new OfertaItem { ofertaId = 1, herramientaid = 1, porcentaje = 25, precioFinal = 11.93f, herramienta = herramientas[0] }
            };

            var oferta = new Oferta
            {
                Id = 1,
                ApplicationUser = administradores[0],
                ofertaItems = ofertaItems,
                fechaInicio = new DateTime(2024, 01, 01),
                fechaFin = new DateTime(2024, 12, 31),
                fechaOferta = new DateTime(2024, 01, 01),
                metodoPago = tiposMetodoPago.Tarjeta,
                paraSocio = tiposDirigidaOferta.Socios
            };

            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.AddRange(administradores);
            _context.AddRange(oferta);
            _context.AddRange(ofertaItems);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_CrearOferta()
        {
            var OfertaSinHerramientas = new CrearOfertaDTO
            {
                FechaInicio = DateTime.Today.AddDays(1),
                FechaFin = DateTime.Today.AddDays(30),
                TiposMetodoPago = tiposMetodoPago.Tarjeta,
                TiposDirigidaOferta = tiposDirigidaOferta.Socios,
                OfertaItem = new List<OfertaItemDTO>()
            };

            var OfertaFechaInicioPasada = new CrearOfertaDTO
            {
                FechaInicio = DateTime.Today.AddDays(-1),
                FechaFin = DateTime.Today.AddDays(30),
                TiposMetodoPago = tiposMetodoPago.Tarjeta,
                TiposDirigidaOferta = tiposDirigidaOferta.Socios,
                OfertaItem = new List<OfertaItemDTO>()
            };
            var OfertaItemValido = new OfertaItemDTO { herramientaId = 1, Porcentaje = 25 };
            OfertaFechaInicioPasada.OfertaItem.Add(OfertaItemValido);

            var OfertaFechaFinAnterior = new CrearOfertaDTO
            {
                FechaInicio = DateTime.Today.AddDays(10),
                FechaFin = DateTime.Today.AddDays(5),
                TiposMetodoPago = tiposMetodoPago.Tarjeta,
                TiposDirigidaOferta = tiposDirigidaOferta.Socios,
                OfertaItem = new List<OfertaItemDTO>()
            };
            OfertaFechaFinAnterior.OfertaItem.Add(OfertaItemValido);

            var OfertaPorcentajeCero = new CrearOfertaDTO
            {
                FechaInicio = DateTime.Today.AddDays(1),
                FechaFin = DateTime.Today.AddDays(30),
                TiposMetodoPago = tiposMetodoPago.Tarjeta,
                TiposDirigidaOferta = tiposDirigidaOferta.Socios,
                OfertaItem = new List<OfertaItemDTO>()
            };
            var OfertaItemPorcentajeCero = new OfertaItemDTO { herramientaId = 1, Porcentaje = 0 };
            OfertaPorcentajeCero.OfertaItem.Add(OfertaItemPorcentajeCero);

            var OfertaPorcentajeExcesivo = new CrearOfertaDTO
            {
                FechaInicio = DateTime.Today.AddDays(1),
                FechaFin = DateTime.Today.AddDays(30),
                TiposMetodoPago = tiposMetodoPago.Tarjeta,
                TiposDirigidaOferta = tiposDirigidaOferta.Socios,
                OfertaItem = new List<OfertaItemDTO>()
            };
            var OfertaItemPorcentajeExcesivo = new OfertaItemDTO { herramientaId = 1, Porcentaje = 101 };
            OfertaPorcentajeExcesivo.OfertaItem.Add(OfertaItemPorcentajeExcesivo);

            var OfertaHerramientaNoExistente = new CrearOfertaDTO
            {
                FechaInicio = DateTime.Today.AddDays(1),
                FechaFin = DateTime.Today.AddDays(30),
                TiposMetodoPago = tiposMetodoPago.Tarjeta,
                TiposDirigidaOferta = tiposDirigidaOferta.Socios,
                OfertaItem = new List<OfertaItemDTO>()
            };
            var OfertaItemHerramientaNE = new OfertaItemDTO { herramientaId = 999, Porcentaje = 25 };
            OfertaHerramientaNoExistente.OfertaItem.Add(OfertaItemHerramientaNE);

            var OfertaSinUsuario = new CrearOfertaDTO
            {
                FechaInicio = DateTime.Today.AddDays(1),
                FechaFin = DateTime.Today.AddDays(30),
                TiposMetodoPago = tiposMetodoPago.Tarjeta,
                TiposDirigidaOferta = tiposDirigidaOferta.Socios,
                OfertaItem = new List<OfertaItemDTO>()
            };
            OfertaSinUsuario.OfertaItem.Add(OfertaItemValido);

            //Modificación Examen Sprint 2
            var DuracionOferta = new CrearOfertaDTO
            {
                FechaInicio = DateTime.Today.AddDays(1),
                FechaFin = DateTime.Today.AddDays(5),
                TiposMetodoPago = tiposMetodoPago.Tarjeta,
                TiposDirigidaOferta = tiposDirigidaOferta.Socios,
                OfertaItem = new List<OfertaItemDTO>()
            };

            var allTest = new List<object[]>
            {
                new object[] { OfertaSinHerramientas, "Debe agregar al menos una herramienta a la oferta" },
                new object[] { OfertaFechaInicioPasada, "La fecha de inicio debe ser posterior o igual a la fecha actual" },
                new object[] { OfertaFechaFinAnterior, "La fecha de fin debe ser posterior a la fecha de inicio" },
                new object[] { OfertaPorcentajeCero, "El porcentaje 0% de 'Martillo' debe estar entre 1 y 100" },
                new object[] { OfertaPorcentajeExcesivo, "El porcentaje 101% de 'Martillo' debe estar entre 1 y 100" },
                new object[] { OfertaHerramientaNoExistente, "La herramienta con id 999 no existe" },
                new object[] { OfertaSinUsuario, "No se encontró un usuario válido" },
                new object[] {DuracionOferta, "¡Error, la oferta debe durar al menos 1 semana!" }
            };

            return allTest;
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesFor_CrearOferta))]
        public async Task CrearOferta_error_test(CrearOfertaDTO crearOfertaDTO, string errorExpected)
        {
            // Arrange
            var mock = new Mock<ILogger<OfertasController>>();
            ILogger<OfertasController> logger = mock.Object;

            var controller = new OfertasController(_context, logger);

            if (errorExpected == "No se encontró un usuario válido")
            {
                _context.ApplicationUser.RemoveRange(_context.ApplicationUser);
                await _context.SaveChangesAsync();
            }

            // Act
            var result = await controller.CrearOferta(crearOfertaDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];

            Assert.Contains(errorExpected, errorActual);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CrearOferta_Success_test()
        {
            // Arrange
            var mock = new Mock<ILogger<OfertasController>>();
            ILogger<OfertasController> logger = mock.Object;

            var controller = new OfertasController(_context, logger);

            var ofertaItems = new List<OfertaItemDTO>
            {
                new OfertaItemDTO { herramientaId = 1, Porcentaje = 25 },
                new OfertaItemDTO { herramientaId = 2, Porcentaje = 10 }
            };

            var ofertaDto = new CrearOfertaDTO
            {
                FechaInicio = DateTime.Today.AddDays(1),
                FechaFin = DateTime.Today.AddDays(30),
                TiposMetodoPago = tiposMetodoPago.Tarjeta,
                TiposDirigidaOferta = tiposDirigidaOferta.Socios,
                OfertaItem = ofertaItems
            };

            // Act
            var result = await controller.CrearOferta(ofertaDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualOfertaDetalleDTO = Assert.IsType<OfertaDetalleDTO>(createdResult.Value);

            // Verificar propiedades básicas
            Assert.Equal(ofertaDto.FechaInicio.Date, actualOfertaDetalleDTO.fechaInicio.Date);
            Assert.Equal(ofertaDto.FechaFin.Date, actualOfertaDetalleDTO.fechaFin.Date);
            Assert.Equal(DateTime.Today, actualOfertaDetalleDTO.fechaOferta.Date);
            Assert.Equal("Admin", actualOfertaDetalleDTO.nombreUsuario);
            Assert.Equal(tiposMetodoPago.Tarjeta, actualOfertaDetalleDTO.metodoPago);
            Assert.Equal(tiposDirigidaOferta.Socios, actualOfertaDetalleDTO.tiposDirigidaOferta);

            // Verificar herramientas
            Assert.Equal(2, actualOfertaDetalleDTO.HerramientasAOfertar.Count);

            var herramienta1 = actualOfertaDetalleDTO.HerramientasAOfertar.First(h => h.nombre == "Martillo");
            Assert.Equal("Acero", herramienta1.material);
            Assert.Equal("Fabricante A", herramienta1.fabricante);
            Assert.Equal(15.9f, herramienta1.precio);
            Assert.Equal(15.9f * 0.75f, herramienta1.precioOferta, 0.001f);

            var herramienta2 = actualOfertaDetalleDTO.HerramientasAOfertar.First(h => h.nombre == "Destornillador");
            Assert.Equal("Acero", herramienta2.material);
            Assert.Equal("Fabricante B", herramienta2.fabricante);
            Assert.Equal(5.5f, herramienta2.precio);
            Assert.Equal(5.5f * 0.90f, herramienta2.precioOferta, 0.001f);
        }
    }
}
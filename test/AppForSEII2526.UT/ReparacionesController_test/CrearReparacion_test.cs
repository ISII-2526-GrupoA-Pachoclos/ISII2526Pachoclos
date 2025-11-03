using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AppForSEII2526.UT.ReparacionesController_test
{
    // Pruebas unitarias del POST CrearReparacion
    public class CrearReparacion_test : AppForSEII25264SqliteUT
    {
        private const string _nombreCliente = "pibi";
        private const string _apellidosCliente = "ronaldo";
        private const string _numTelefono = "9876543210";

        private const string _herramienta1Nombre = "Llave Inglesa";
        private const string _herramienta2Nombre = "Destornillador";

        public CrearReparacion_test()
        {
            var fabricantes = new List<fabricante>()
            {
                new fabricante { id = 1, nombre = "Paco" },
                new fabricante { id = 2, nombre = "Ana" }
            };

            var herramientas = new List<Herramienta>()
            {
                new Herramienta
                {
                    id = 4,
                    nombre = _herramienta1Nombre,
                    material = "Hierro",
                    precio = 10.0f,
                    tiempoReparacion = "10 dias",
                    fabricante = fabricantes[0]
                },
                new Herramienta
                {
                    id = 5,
                    nombre = _herramienta2Nombre,
                    material = "Acero",
                    precio = 7.0f,
                    tiempoReparacion = "7 dias",
                    fabricante = fabricantes[1]
                }
            };

            var usuario = new ApplicationUser
            {
                nombre = _nombreCliente,
                apellido = _apellidosCliente,
                correoElectronico = "si",
                numTelefono = _numTelefono,
            };

            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.Add(usuario);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_CrearReparacion_Error()
        {
            // Caso 1: Sin herramientas
            var reparacionSinHerramientas = new ReparacionParaCrearDTO(
                _nombreCliente, _apellidosCliente, _numTelefono, metodoPago.PayPal,
                DateTime.Today.AddDays(1), new List<ReparacionItemDTO>());

            // Caso 2: Fecha de entrega anterior a hoy
            var herramientasFechaAnterior = new List<ReparacionItemDTO>
            {
                new ReparacionItemDTO(4, 10.0f, _herramienta1Nombre, "Mango roto", 3)
            };
            var reparacionFechaAnterior = new ReparacionParaCrearDTO(
                _nombreCliente, _apellidosCliente, _numTelefono, metodoPago.PayPal,
                DateTime.Today.AddDays(-1), herramientasFechaAnterior);

            // Caso 3: Cliente no registrado
            var herramientasClienteNoRegistrado = new List<ReparacionItemDTO>
            {
                new ReparacionItemDTO(4, 10.0f, _herramienta1Nombre, "Mango roto", 3)
            };
            var reparacionClienteNoRegistrado = new ReparacionParaCrearDTO(
                "Usuario", "Falso", "1234567890", metodoPago.Efectivo,
                DateTime.Today.AddDays(1), herramientasClienteNoRegistrado);

            // Caso 4: Herramienta no existe
            var herramientasNoExiste = new List<ReparacionItemDTO>
            {
                new ReparacionItemDTO(999, 10.0f, "Herramienta Inexistente", "Descripción", 1)
            };
            var reparacionHerramientaNoExiste = new ReparacionParaCrearDTO(
                _nombreCliente, _apellidosCliente, _numTelefono, metodoPago.PayPal,
                DateTime.Today.AddDays(1), herramientasNoExiste);

            // Caso 5: Cantidad <= 0
            var herramientasCantidadCero = new List<ReparacionItemDTO>
            {
                new ReparacionItemDTO(4, 10.0f, _herramienta1Nombre, "Mango roto", 0)
            };
            var reparacionCantidadCero = new ReparacionParaCrearDTO(
                _nombreCliente, _apellidosCliente, _numTelefono, metodoPago.PayPal,
                DateTime.Today.AddDays(1), herramientasCantidadCero);

            // Caso 6: Nombre de herramienta no coincide con ID
            var herramientasNombreIncorrecto = new List<ReparacionItemDTO>
            {
                new ReparacionItemDTO(4, 10.0f, "Nombre Incorrecto", "Mango roto", 1)
            };
            var reparacionNombreIncorrecto = new ReparacionParaCrearDTO(
                _nombreCliente, _apellidosCliente, _numTelefono, metodoPago.PayPal,
                DateTime.Today.AddDays(1), herramientasNombreIncorrecto);

            var allTests = new List<object[]>
            {
                new object[] { reparacionSinHerramientas, "Debe incluir al menos una herramienta para reparar." },
                new object[] { reparacionFechaAnterior, "La fecha de entrega debe ser igual o posterior a hoy." },
                new object[] { reparacionClienteNoRegistrado, "El cliente no está registrado en el sistema." },
                new object[] { reparacionHerramientaNoExiste, "La herramienta con ID 999 no existe." },
                new object[] { reparacionCantidadCero, $"La cantidad de la herramienta '{_herramienta1Nombre}' debe ser mayor que 0." },
                new object[] { reparacionNombreIncorrecto, $"El nombre de la herramienta 'Nombre Incorrecto' no coincide con el ID 4." },
            };

            return allTests;
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesFor_CrearReparacion_Error))]
        public async Task CrearReparacion_Error_test(ReparacionParaCrearDTO reparacionDTO, string errorExpected)
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ReparacionesController>>();
            var logger = mockLogger.Object;
            var controller = new ReparacionesController(_context, logger);

            // Act
            var result = await controller.CrearReparacion(reparacionDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];

            // Verificar que el mensaje de error contiene el texto esperado
            Assert.Contains(errorExpected, errorActual);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CrearReparacion_Success_test()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ReparacionesController>>();
            var logger = mockLogger.Object;
            var controller = new ReparacionesController(_context, logger);

            // Crear DTO para la reparación
            var herramientas = new List<ReparacionItemDTO>
            {
                new ReparacionItemDTO(4, 10.0f, _herramienta1Nombre, "Mango roto y oxidada", 3),
                new ReparacionItemDTO(5, 7.0f, _herramienta2Nombre, "Punta desgastada", 2)
            };

            var fechaEntrega = DateTime.Today.AddDays(1);
            var reparacionDTO = new ReparacionParaCrearDTO(
                _nombreCliente, _apellidosCliente, _numTelefono, metodoPago.PayPal,
                fechaEntrega, herramientas);

            // Calcular fecha de recogida esperada (máximo tiempo de reparación: 10 días)
            var fechaRecogidaEsperada = fechaEntrega.AddDays(10);
            var precioTotalEsperado = (herramientas[0].cantidad * 10.0f) + (herramientas[1].cantidad * 7.0f); // 30 + 14 = 44

            // Act
            var result = await controller.CrearReparacion(reparacionDTO);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var reparacionDetalle = Assert.IsType<ReparacionDetalleDTO>(createdResult.Value);

            // Verificar propiedades básicas
            // Assert.True(reparacionDetalle.id > 0); // ID generado automáticamente
            Assert.Equal(_nombreCliente, reparacionDetalle.nombre);
            Assert.Equal(_apellidosCliente, reparacionDetalle.apellido);
            Assert.Equal(fechaEntrega.Date, reparacionDetalle.fechaEntrega);
            Assert.Equal(fechaRecogidaEsperada.Date, reparacionDetalle.fechaRecogida);
            Assert.Equal(metodoPago.PayPal, reparacionDetalle.metodoPago);
            Assert.Equal(precioTotalEsperado, reparacionDetalle.precioTotal);

            // Verificar herramientas
            Assert.Equal(2, reparacionDetalle.HerramientasAReparar.Count);

            // Verificar primera herramienta
            var herramienta1 = reparacionDetalle.HerramientasAReparar[0];
            Assert.Equal(4, herramienta1.HerramientaId);
            Assert.Equal(_herramienta1Nombre, herramienta1.nombreHerramienta);
            Assert.Equal(10.0f, herramienta1.precio);
            Assert.Equal("Mango roto y oxidada", herramienta1.descripcion);
            Assert.Equal(3, herramienta1.cantidad);

            // Verificar segunda herramienta
            var herramienta2 = reparacionDetalle.HerramientasAReparar[1];
            Assert.Equal(5, herramienta2.HerramientaId);
            Assert.Equal(_herramienta2Nombre, herramienta2.nombreHerramienta);
            Assert.Equal(7.0f, herramienta2.precio);
            Assert.Equal("Punta desgastada", herramienta2.descripcion);
            Assert.Equal(2, herramienta2.cantidad);

            // Verificar que se guardó en la base de datos
            var reparacionEnBD = await _context.Reparacion
                .Include(r => r.ReparacionItems)
                .FirstOrDefaultAsync(r => r.id == reparacionDetalle.id);

            Assert.NotNull(reparacionEnBD);
            Assert.Equal(2, reparacionEnBD.ReparacionItems.Count);
            Assert.Equal(precioTotalEsperado, reparacionEnBD.precioTotal);
        }

        /*
        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CrearReparacion_SinTelefono_Success_test()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ReparacionesController>>();
            var logger = mockLogger.Object;
            var controller = new ReparacionesController(_context, logger);

            // Crear DTO para la reparación sin teléfono
            var herramientas = new List<ReparacionItemDTO>
            {
                new ReparacionItemDTO(4, 10.0f, _herramienta1Nombre, "Mango roto", 1)
            };

            var fechaEntrega = DateTime.Today.AddDays(1);
            var reparacionDTO = new ReparacionParaCrearDTO(
                _nombreCliente, _apellidosCliente, null, metodoPago.TarjetaCredito,
                fechaEntrega, herramientas);

            // Act
            var result = await controller.CrearReparacion(reparacionDTO);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var reparacionDetalle = Assert.IsType<ReparacionDetalleDTO>(createdResult.Value);

            // Verificar que se creó correctamente sin teléfono
            Assert.True(reparacionDetalle.id > 0);
            Assert.Equal(_nombreCliente, reparacionDetalle.nombre);
            Assert.Equal(_apellidosCliente, reparacionDetalle.apellido);
            Assert.Equal(metodoPago.TarjetaCredito, reparacionDetalle.metodoPago);
            Assert.Single(reparacionDetalle.HerramientasAReparar);
        }
        */
    }
}
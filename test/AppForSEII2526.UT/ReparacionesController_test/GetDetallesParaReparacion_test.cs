using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AppForSEII2526.UT.ReparacionesController_test
{
    public class GetDetalles_Reparacion_test : AppForSEII25264SqliteUT
    {
        public GetDetalles_Reparacion_test()
        {
            var fabricantes = new List<fabricante>()
            {
                new fabricante { id = 1, nombre = "Pepe" },
                new fabricante { id = 2, nombre = "Juan" }
            };

            var herramientas = new List<Herramienta>()
            {
                new Herramienta
                {
                    id = 1,
                    nombre = "Taladro",
                    material = "Metal",
                    precio = 50.0f,
                    tiempoReparacion = "5 dias",
                    fabricante = fabricantes[0]
                },
                new Herramienta
                {
                    id = 2,
                    nombre = "Sierra",
                    material = "Acero",
                    precio = 35.0f,
                    tiempoReparacion = "3 dias",
                    fabricante = fabricantes[1]
                }
            };

            var usuario = new ApplicationUser
            {
                nombre = "Juan",
                apellido = "Pérez",
                correoElectronico = "juan@gmail.com",
                numTelefono = "123456789"
            };

            var reparacion = new Reparacion
            {
                id = 1,
                fechaEntrega = new DateTime(2024, 1, 15), // Formato: AÑO, MES, DÍA
                fechaRecogida = new DateTime(2024, 1, 25),
                metodoPago = metodoPago.TarjetaCredito,
                precioTotal = 120.0f,
                ApplicationUser = usuario
            };

            // Crear items de reparación
            var reparacionItems = new List<ReparacionItem>
            {
                new ReparacionItem
                {
                    Herramientaid = 1,
                    Reparacionid = 1,
                    cantidad = 2,
                    descripcion = "Motor quemado",
                    precio = 50.0f,
                    Herramienta = herramientas[0],
                    Reparacion = reparacion
                },
                new ReparacionItem
                {
                    Herramientaid = 2,
                    Reparacionid = 1,
                    cantidad = 1,
                    descripcion = "Filo desgastado",
                    precio = 35.0f,
                    Herramienta = herramientas[1],
                    Reparacion = reparacion
                }
            };

            reparacion.ReparacionItems = reparacionItems;

            // guardar BD
            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.Add(usuario);
            _context.Add(reparacion);
            _context.AddRange(reparacionItems);
            _context.SaveChanges();
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetDetalles_Reparacion_NotFound_test() // ID que no existe
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ReparacionesController>>();
            var logger = mockLogger.Object;
            var controller = new ReparacionesController(_context, logger);

            // Act - ID que no existe
            var result = await controller.GetDetalles_Reparacion(999999999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetDetalles_Reparacion_Found_test() // ID que existe
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ReparacionesController>>();
            var logger = mockLogger.Object;
            var controller = new ReparacionesController(_context, logger);

            // DTO esperado
            var herramientasEsperadas = new List<ReparacionItemDTO>
            {
                new ReparacionItemDTO(
                    herramientaId: 1,
                    precio: 50.0f,
                    nombreHerramienta: "Taladro",
                    descripcion: "Motor quemado",
                    cantidad: 2,
                    tiempoReparacion: "5 dias"
                ),
                new ReparacionItemDTO(
                    herramientaId: 2,
                    precio: 35.0f,
                    nombreHerramienta: "Sierra",
                    descripcion: "Filo desgastado",
                    cantidad: 1,
                    tiempoReparacion: "3 dias"
                )
            };

            var reparacionEsperada = new ReparacionDetalleDTO(
                id: 1,
                nombre: "Juan",
                apellido: "Pérez",
                fechaEntrega: new DateTime(2024, 1, 15),
                fechaRecogida: new DateTime(2024, 1, 25),
                precioTotal: 1f, // Se calculará en la verificación
                herramientasAReparar: herramientasEsperadas
            );

            // Act
            var result = await controller.GetDetalles_Reparacion(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var reparacionActual = Assert.IsType<ReparacionDetalleDTO>(okResult.Value);

            // assert atributos basicos
            Assert.Equal(reparacionEsperada.id, reparacionActual.id);
            Assert.Equal(reparacionEsperada.nombre, reparacionActual.nombre);
            Assert.Equal(reparacionEsperada.apellido, reparacionActual.apellido);
            Assert.Equal(reparacionEsperada.fechaEntrega, reparacionActual.fechaEntrega);
            Assert.Equal(reparacionEsperada.fechaRecogida, reparacionActual.fechaRecogida);

            // assert items
            Assert.Equal(reparacionEsperada.HerramientasAReparar.Count, reparacionActual.HerramientasAReparar.Count);

            for (int i = 0; i < reparacionEsperada.HerramientasAReparar.Count; i++)
            {
                var esperado = reparacionEsperada.HerramientasAReparar[i]; // item esperado
                var actual = reparacionActual.HerramientasAReparar[i]; // item actual

                // assert propiedades de cada item
                Assert.Equal(esperado.HerramientaId, actual.HerramientaId);
                Assert.Equal(esperado.nombreHerramienta, actual.nombreHerramienta);
                Assert.Equal(esperado.precio, actual.precio);
                Assert.Equal(esperado.descripcion, actual.descripcion);
                Assert.Equal(esperado.cantidad, actual.cantidad);
            }

            // assert precio total
            var precioTotalCalculado = reparacionActual.HerramientasAReparar.Sum(h => h.precio * h.cantidad);
            Assert.Equal(precioTotalCalculado, reparacionActual.precioTotal);
        }
    }
}

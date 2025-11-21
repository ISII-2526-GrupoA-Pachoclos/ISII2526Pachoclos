using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppForSEII2526.UT.ComprasController_test
{
    public class PostCompras_test : AppForSEII25264SqliteUT
    {
        public PostCompras_test()
        {

            var fabricantes = new List<fabricante>
            {
                new fabricante{id=1, nombre= "Fabricante A" },
                new fabricante{id=2, nombre="Fabricante B" },
                new fabricante{id=3, nombre="Fabricante C"}
            };
            var herramientas = new List<Herramienta>
            {
                new Herramienta{id=7, nombre="Martillo", fabricante=fabricantes[0], material="Madera", precio=50, tiempoReparacion="2 dias" },
                new Herramienta{id=8, nombre="Barrena", fabricante=fabricantes[1], material="Madera", precio=70, tiempoReparacion= "2 dias" } ,
                new Herramienta{id=9, nombre="Alicate", fabricante=fabricantes[2], material="metal", precio=80, tiempoReparacion="2 dias" }
            };

            var clientes = new List<ApplicationUser>
            {
                new ApplicationUser{Id="1", nombre= "Juan", apellido= "Valdes", correoElectronico="juan@gmail", numTelefono="657464646"}

            };

            var compraItems = new List<ComprarItem>
            {
                new ComprarItem{compraId=1, cantidad=1, herramientaid=7, herramienta=herramientas[0], precio=50, descripcion="descripcion" }
            };

            var compra = new Compra { Id = 1, ApplicationUser = clientes[0], CompraItems = compraItems, direccionEnvio = "calle", fechaCompra = new DateTime(2015, 01, 01), metodopago = formaPago.Efectivo, precioTotal = 50 };



            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.AddRange(clientes);
            _context.AddRange(compra);
            _context.AddRange(compraItems);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_CrearCompra() { 
        
            var CompraSinHerramientas = new CrearCompraDTO { Nombre = "Juan", Apellido = "Valdes", direccionEnvio = "calle", metodoPago = formaPago.Efectivo, HerramientasCompradas = new List<CompraItemDTO>() };

            var CompraSinCantidad = new CrearCompraDTO { Nombre = "Juan", Apellido = "Valdes", direccionEnvio = "calle", metodoPago = formaPago.Efectivo, HerramientasCompradas = new List<CompraItemDTO>() };
            var CompraItemSinCantidad = new CompraItemDTO { cantidad = 0, herramientaid = 7, nombre = "Martillo", precio = 50, descripcion = "descripcion" };
            CompraSinCantidad.HerramientasCompradas.Add(CompraItemSinCantidad);

            var CompraSinUsuario = new CrearCompraDTO { Nombre = " ", Apellido = "Valdes", direccionEnvio = "calle", metodoPago = formaPago.Efectivo, HerramientasCompradas = new List<CompraItemDTO>() };
            var CompraItem = new CompraItemDTO { cantidad = 1, herramientaid = 7, nombre = "Martillo", precio = 50, descripcion = "descripcion" };
            CompraSinUsuario.HerramientasCompradas.Add(CompraItem);

            var CompraConHerramientaNoExistente = new CrearCompraDTO { Nombre = "Juan", Apellido = "Valdes", direccionEnvio = "calle", metodoPago = formaPago.Efectivo, HerramientasCompradas = new List<CompraItemDTO>() };
            var CompraItemConHerramientaNE = new CompraItemDTO { cantidad = 1, herramientaid = 0, nombre = "Martillo", precio = 50, descripcion = "descripcion" };
            CompraConHerramientaNoExistente.HerramientasCompradas.Add(CompraItemConHerramientaNE);

            var CompraConCantidad = new CrearCompraDTO { Nombre = "Juan", Apellido = "Valdes", direccionEnvio = "calle", metodoPago = formaPago.Efectivo, HerramientasCompradas = new List<CompraItemDTO>() };
            var CompraItemConCantidad = new CompraItemDTO { cantidad = 3, herramientaid = 7, nombre = "Martillo", precio = 50, descripcion="" };
            CompraConCantidad.HerramientasCompradas.Add(CompraItemConCantidad);


            var allTest = new List<object[]>
            {
                new object[] { CompraSinHerramientas,  "Error! Debes incluir al menos una herramienta " },
                new object[] { CompraSinCantidad, "Error! La cantidad debe ser mayor que 0" },
                new object[] { CompraSinUsuario, "Error! Usuario no registrado" },
                new object[] { CompraConHerramientaNoExistente, "Error! La herramienta con ese id no existe" },
                new object[] { CompraConCantidad, "Error! Estas comprando demasiadas herramientas sin descripcion" }
            };

            return allTest;
            

        }


        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesFor_CrearCompra))]
        public async Task CrearCompra_error_test(CrearCompraDTO crearcompra, string errorExpected) {

            //Arrange
            var mock = new Mock<ILogger<ComprasController>>();
            ILogger<ComprasController> logger = mock.Object;

            var controller = new ComprasController(_context, logger);


            //Act
            var result = await controller.CrearCompra(crearcompra);

            //Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];

            Assert.StartsWith(errorExpected, errorActual);
        
        }



        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CrearCompra_Success_test() {

            // Arrange
            var mock = new Mock<ILogger<ComprasController>>();
            ILogger<ComprasController> logger = mock.Object;

            var controller = new ComprasController(_context, logger);

            var compraItems = new List<CompraItemDTO>
            {
                new CompraItemDTO{ cantidad=1, herramientaid=7, nombre="Martillo", precio=50 , descripcion="descripcion"}
            };

            var CompraDto = new CrearCompraDTO {Nombre="Juan", Apellido="Valdes", direccionEnvio="calle", metodoPago=formaPago.Efectivo, HerramientasCompradas=compraItems};

            var expectedCompradetalleDTO = new CompraDetalleDTO { NombreCliente="Juan", ApellidosCliente="Valdes", direccionEnvio="calle", fechaCompra=DateTime.Today, precioTotal=50 , HerramientasCompradas=compraItems};


            
            //Act
            var result = await controller.CrearCompra(CompraDto);

            //Assert

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualCompraDetalleDTO = Assert.IsType<CompraDetalleDTO>(createdResult.Value);

            Assert.Equal(expectedCompradetalleDTO, actualCompraDetalleDTO);



        }


    }
}

using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

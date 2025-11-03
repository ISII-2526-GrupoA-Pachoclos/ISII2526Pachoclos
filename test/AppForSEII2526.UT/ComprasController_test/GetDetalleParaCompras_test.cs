using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ComprasController_test
{
    public class GetDetalleParaCompras_test : AppForSEII25264SqliteUT
    {
        
        public GetDetalleParaCompras_test()
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

        public static IEnumerable<object?[]> TestCasesFor_GetDetalles_Compra()
        {

            var compraItemDTO = new List<CompraItemDTO> {

                new CompraItemDTO{ cantidad=1, herramientaid=7, nombre="Martillo", precio=50 , descripcion="descripcion"}


            };

            var compraDetalleDTO = new CompraDetalleDTO { Id = 1, direccionEnvio = "calle", precioTotal = 50, fechaCompra = new DateTime(2015, 01, 01), HerramientasCompradas = compraItemDTO };

            

            var Test = new List<object?[]>
            {
                new object?[] {1, compraDetalleDTO }
            };
            return Test;


        }

        [Theory]
        [MemberData(nameof(TestCasesFor_GetDetalles_Compra))]
        [Trait("Database", "WithoutFixure")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetDetalles_Compra_Test(int id, CompraDetalleDTO expectedCompra)
        {
            //Arrange
            var controller = new ComprasController(_context, null);

            //Act
            var result = await controller.GetDetalles_Compra(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var comprasDTOsActual = Assert.IsAssignableFrom<CompraDetalleDTO>(okResult.Value);
            Assert.Equal(expectedCompra, comprasDTOsActual);



        }
    }
}

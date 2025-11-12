using AppForSEII2526.UT;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.HerramientasController_test
{
    public class GetHerramientasParaComprar_test : AppForSEII25264SqliteUT
    {

        public GetHerramientasParaComprar_test()
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

            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.SaveChanges();

        }

        public static IEnumerable<object?[]> TestCasesFor_GetHerramientasParaComprar_conTodosLosDatos_DTO()
        {
            var HerramientasDTOs = new List<HerramientasParaComprarDTO>
            {
                new HerramientasParaComprarDTO(7, "Martillo","Madera", "Fabricante A",50 ),
                new HerramientasParaComprarDTO(8, "Barrena","Madera", "Fabricante B",70 ),
                new HerramientasParaComprarDTO(9, "Alicate","metal", "Fabricante C",80 )
            };
            var HerramientasDTOsTC1 = new List<HerramientasParaComprarDTO>() { HerramientasDTOs[0], HerramientasDTOs[1], HerramientasDTOs[2] }
                .OrderBy(h => h.nombre)
                .ToList();
            var HerramientasDTOsTC2 = new List<HerramientasParaComprarDTO>() { HerramientasDTOs[0] }
                .ToList();

            var HerramientasDTOsTC3 = new List<HerramientasParaComprarDTO>() { HerramientasDTOs[0], HerramientasDTOs[1] }
                .OrderBy(h => h.nombre)
                .ToList();

            var allTest = new List<object[]>
            {
                new object[] { null, null, HerramientasDTOsTC1 },
                new object[] { 60f, null, HerramientasDTOsTC2 },
                new object[] { null, "Madera", HerramientasDTOsTC3 }
            };
            return allTest;



        }


        [Theory]
        [MemberData(nameof(TestCasesFor_GetHerramientasParaComprar_conTodosLosDatos_DTO))]
        [Trait("Database", "WithoutFixure")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetHerramientasParaComprar_conTodosLosDatos_DTO_Test(float? filtroPrecio, string? filtroMaterial, IList<HerramientasParaComprarDTO> expectedHerramientas)
        {


            //Arrange
            var controller = new HerramientasController(_context, null);

            //Act
            var result = await controller.GetHerramientasParaComprarconTodosLosDatosDTO(filtroPrecio, filtroMaterial);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var herramientaDTOsActual = Assert.IsAssignableFrom<IList<HerramientasParaComprarDTO>>(okResult.Value);
            Assert.Equal(expectedHerramientas, herramientaDTOsActual);
        }
    }
}

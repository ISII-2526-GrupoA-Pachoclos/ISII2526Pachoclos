using AppForSEII2526;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.HerramientasController_test
{
    public class GetHerramientasParaReparar_test : AppForSEII25264SqliteUT
    {
        public GetHerramientasParaReparar_test()
        {
            var fabricantes = new List<fabricante>()
            {
                new fabricante { nombre = "Pepe" },
                new fabricante { nombre = "Ana" },
                new fabricante { nombre = "Luis" },
            };

            var herramientas = new List<Herramienta>()
            {
                new Herramienta {id = 1, nombre = "Martillo", fabricante = fabricantes[0], material = "Madera", precio = 15, tiempoReparacion = "2 dias" },
                new Herramienta {id = 2, nombre = "Destornillador",  fabricante = fabricantes[1], material = "Metal", precio = 10, tiempoReparacion = "1 dia" },
                new Herramienta {id = 3, nombre = "Llave inglesa", fabricante = fabricantes[2], material = "Metal", precio = 20, tiempoReparacion = "3 dias" },
            };


            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.SaveChanges();
        }


        public static IEnumerable<object?[]> TestCasesFor_GetHerramientasParaReparar_conTodosLosDatos_DTO()
        {
            var herramientasDTOs = new List<HerramientasParaRepararDTO>()
            {
                new HerramientasParaRepararDTO(1, "Madera", "Martillo", 15,  "2 dias", "Pepe"),
                new HerramientasParaRepararDTO(2, "Metal", "Destornillador", 10, "1 dia", "Ana"),
                new HerramientasParaRepararDTO(3, "Metal", "Llave inglesa", 20, "3 dias", "Luis"),
            };

            // Caso sin filtros: ahora esperamos todos los elementos (ordenados por nombre)
            var herramientasDTOsTC1 = new List<HerramientasParaRepararDTO>() { 
                herramientasDTOs[0], // Pepe (Martillo)
                herramientasDTOs[1], // Ana (Destornillador)
                herramientasDTOs[2] // Luis (Llave inglesa) 
            }
                .OrderBy(h => h.nombre).ToList();

            var herramientasDTOsTC2 = new List<HerramientasParaRepararDTO>() { herramientasDTOs[1] };
            var herramientasDTOsTC3 = new List<HerramientasParaRepararDTO>() { herramientasDTOs[2] };

            var allTest = new List<object[]>
            {
                new object[] { null, null, herramientasDTOsTC1 }, // TC1: Sin filtros -> todos ordenados por nombre herramienta
                new object[] { "Destornillador", null, herramientasDTOsTC2 }, // TC2: Filtro por nombre herramienta
                new object[] { null, "3 dias", herramientasDTOsTC3 }, // TC3: Filtro por tiempo de reparacion
            };

            return allTest;
        }


        [Theory]
        [MemberData(nameof(TestCasesFor_GetHerramientasParaReparar_conTodosLosDatos_DTO))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetHerramientasParaReparar_conTodosLosDatos_DTO_Test(string? filtroNombre, 
            string? filtroTiempoReparacion,
            IList<HerramientasParaRepararDTO> herramientasEsperadas)
        {
            // Arrange
            var controller = new HerramientasController(_context, null);

            // Act
            var result = await controller.GetHerramientasParaReparar_conTodosLosDatos_DTO(filtroNombre, filtroTiempoReparacion);

            // Assert
            // Verificar que el resultado es Ok
            var okResult = Assert.IsType<OkObjectResult>(result);
            var herramientasDTOsActual = Assert.IsAssignableFrom<IList<HerramientasParaRepararDTO>>(okResult.Value);

            Assert.Equal(herramientasEsperadas, herramientasDTOsActual);
        }
    }
}

using AppForMovies.UT;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.HerramientasController_test
{
    public class GetHerramientasParaOfertas_test : AppForMovies4SqliteUT
    {

        public GetHerramientasParaOfertas_test()
        {
            var fabricantes = new List<fabricante>()
                    {
                        new fabricante { nombre = "Pepe" },
                        new fabricante { nombre = "Ana" },
                        new fabricante { nombre = "Luis" },
                    };

            var herramientas = new List<Herramienta>()
                    {
                        new Herramienta { nombre = "Martillo", fabricante = fabricantes[0], material = "", tiempoReparacion = "0", precio = 20f },
                        new Herramienta { nombre = "Destornillador",  fabricante = fabricantes[1], material = "", tiempoReparacion = "0", precio = 30f },
                        new Herramienta { nombre = "Llave inglesa", fabricante = fabricantes[2] , material = "", tiempoReparacion = "0", precio = 40f },
                    };

            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.SaveChanges();
        }



        public static IEnumerable<object[]> GetHerramientasParaOferta_conTodosLosDatos_DTO()
        {
            var herramientaDTOs = new List<HerramientasParaOfertasDTO>()
            {
                new HerramientasParaOfertasDTO { id = 1, material = "", nombre = "Martillo", precio = 20, fabricante = "Pepe" },
                new HerramientasParaOfertasDTO { id = 2, material = "", nombre = "Destornillador", precio = 30, fabricante = "Ana" },
                new HerramientasParaOfertasDTO { id = 3, material = "", nombre = "Llave inglesa", precio = 40, fabricante = "Luis" },
            };

            // Se espera que la consulta devuelva los elementos en el orden por fabricante (Ana, Luis, Pepe)
            var herramientaDTOsOrderedByFabricante = new List<HerramientasParaOfertasDTO>() { herramientaDTOs[1], herramientaDTOs[2], herramientaDTOs[0] };

            var herramientaDTOsTC1 = new List<HerramientasParaOfertasDTO>() { herramientaDTOs[1], herramientaDTOs[2] }
            var herramientaDTOsTC2 = new List<HerramientasParaOfertasDTO>() { herramientaDTOs[1]};
            var herramientaDTOsTC3 = new List<HerramientasParaOfertasDTO>() { herramientaDTOs[2]};
            var herramientaDTOsTC4 = herramientaDTOsOrderedByFabricante;

            var allTests = new List<object[]>
            {             
                // filtroPrecio, filtroFabricante, expected
                // Caso por defecto: no filtros -> todos los elementos (ordenados por fabricante)
                new object[] { (float?)null, (string?)null, herramientaDTOsTC4 },
                // Filtrar por fabricante "Ana" (destornillador)
                new object[] { (float?)null, (string?)"Ana", herramientaDTOsTC2 },
                // Filtrar por precio = 40 -> Llave inglesa
                new object[] { (float?)40f, (string?)null,  herramientaDTOsTC3 },
                // Repetición explícita del caso todos sin filtros
                new object[] { (float?)null, (string?)null, herramientaDTOsTC4 },
            };

            return allTests;
        }




        [Theory]
        [MemberData(nameof(GetHerramientasParaOferta_conTodosLosDatos_DTO))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetHerramientasParaOferta_conTodosLosDatos_DTO_test(float? filtroPrecio, string? filtroFabricante, IList<HerramientasParaOfertasDTO> expectedHerramientasOfertadas)
        {
            //Arrange
            var controller = new HerramientasController(_context, null!);

            //Act
            var result = await controller.GetHerramientasParaOferta_conTodosLosDatos_DTO(filtroPrecio, filtroFabricante);

            //Assert
            //Comprobamos que el resultado es Ok
            var okResult = Assert.IsType<OkObjectResult> (result);
            //y obtenemos la lista de herramientas ofertadas
            var herramientasDTOsActual = Assert.IsAssignableFrom<IList<HerramientasParaOfertasDTO>>(okResult.Value);
            Assert.Equal(expectedHerramientasOfertadas, herramientasDTOsActual);
        }
    }
}

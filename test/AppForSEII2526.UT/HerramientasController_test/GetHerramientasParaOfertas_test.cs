using AppForSEII2526.UT;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;

namespace AppForSEII2526.UT.HerramientasController_test
{
    public class GetHerramientasParaOfertas_test : AppForSEII25264SqliteUT
    {

        public GetHerramientasParaOfertas_test()
        {
            var fabricante = new List<fabricante>
            {
                new fabricante { nombre = "Pepe" },
                new fabricante { nombre = "Ana" },
                new fabricante { nombre = "Luis" },
            };

            var herramienta = new List<Herramienta>
            {
                new Herramienta { nombre = "Martillo", material = "Acero", precio = 15.9f, fabricante = fabricante[0], tiempoReparacion = "" },
                new Herramienta { nombre = "Destornillador", material = "Acero", precio = 5.5f, fabricante = fabricante[1], tiempoReparacion = "" },
                new Herramienta { nombre = "Brocas", material = "Metal", precio = 8.0f, fabricante = fabricante[2], tiempoReparacion = "" },
            };

            _context.AddRange(fabricante);
            _context.AddRange(herramienta);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> GetHerramientasParaOferta_conTodosLosDatos_DTO()
        {
            var herramientaDTOs = new List<HerramientasParaOfertasDTO>()
            {
                new HerramientasParaOfertasDTO { nombre = "Martillo", material = "Acero", fabricante = "Pepe", precio = 15.9f },
                new HerramientasParaOfertasDTO { nombre = "Destornillador", material = "Acero", fabricante = "Ana", precio = 5.5f },
                new HerramientasParaOfertasDTO { nombre = "Brocas", material = "Metal", fabricante = "Luis", precio = 8.0f },
            };

            var herramientaDTOsTC1 = new List<HerramientasParaOfertasDTO>() { herramientaDTOs[0], herramientaDTOs[1], herramientaDTOs[2] };
            var herramientaDTOsTC2 = new List<HerramientasParaOfertasDTO>() { herramientaDTOs[1] };
            var herramientaDTOsTC3 = new List<HerramientasParaOfertasDTO>() { herramientaDTOs[1], herramientaDTOs[2] };

            var allTests = new List<object[]>
            {
                new object[] { null, null, herramientaDTOsTC1 },
                new object[] { null, "Ana", herramientaDTOsTC2 },
                new object[] { 8.0f, null, herramientaDTOsTC3 }
            };

            return allTests;
        }

        [Theory]
        [MemberData(nameof(GetHerramientasParaOferta_conTodosLosDatos_DTO))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetHerramientasParaOferta_conTodosLosDatos_DTO_test(float? filtroPrecio, string? filtroFabricante, IList<HerramientasParaOfertasDTO> expectedHerramientasOfertadas)
        {
            // Arrange
            var controller = new HerramientasController(_context, null);

            // Act
            var result = await controller.GetHerramientasParaOferta_conTodosLosDatos_DTO(filtroPrecio, filtroFabricante);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var herramientasDTOsActual = Assert.IsAssignableFrom<IList<HerramientasParaOfertasDTO>>(okResult.Value);

            Assert.Equal(expectedHerramientasOfertadas.Count, herramientasDTOsActual.Count);

            foreach (var esperado in expectedHerramientasOfertadas)
            {
                var match = herramientasDTOsActual.FirstOrDefault(a =>
                    string.Equals(a.nombre, esperado.nombre, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(a.material, esperado.material, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(a.fabricante, esperado.fabricante, StringComparison.OrdinalIgnoreCase) &&
                    Math.Abs(a.precio - esperado.precio) < 0.0001f
                );

                Assert.NotNull(match);
            }
        }
    }
}

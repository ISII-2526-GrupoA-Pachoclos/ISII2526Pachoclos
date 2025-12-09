using AppForMovies.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_CrearOfertas
{
    public class CUCrearOfertas_UIT : UC_UIT
    {
        private SelectHerramientasParaOfertasPO _selectHerramientasParaOfertasPO;
        private const float filtroPrecio1 = 4;
        private const string filtroFabricante1 = "Ana";
        private const int idHerramienta1 = 2;
        private const string nombreHerramienta1 = "Destornillador";
        private const string materialHerramienta1 = "Acero";

        public CUCrearOfertas_UIT(ITestOutputHelper output) : base(output)
        {
            _selectHerramientasParaOfertasPO = new SelectHerramientasParaOfertasPO(_driver, _output);

            /*
            private void Precondition_perform_login() {
            Perform_login("elena@uclm.es", "Password1234%");
            }
            */

        }

        private void InitialStepsForOfertarHerramientas()
        {
            Initial_step_opening_the_web_page();

            //Precondition_perform_login();

            By id = By.Id("CreateOfertas");
            // We wait for the option of the menu to be visible
            _selectHerramientasParaOfertasPO.WaitForBeingVisible(id);

            //Esperamos para que Blazor termine de renderizar la página
            Thread.Sleep(500);

            // we click on the menu
            _driver.FindElement(id).Click();
        }

        /*
        ======================================================
             PRUEBAS DEL SELECT (PASOS 2,3; FLUJO 0)
        ======================================================
        */

        [Theory]
        [InlineData("6", "Jose", "3", "Martillo", "Madera", "Jose", "6")] // Filtro solo por precio y fabricante
        [InlineData("6", "", "3", "Martillo", "Madera", "Jose", "6")] // Filtro solo por precio
        [InlineData("", "Ana", "2", "Destornillador", "Acero", "Ana", "7")] // Filtro solo por fabricante
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_2_3_AF0_filteringbyPrecioandFabricante(
            string filtroPrecio,
            string filtroFabricante,
            string expectedId,
            string expectedNombre,
            string expectedMaterial,
            string expectedFabricante,
            string expectedPrecio)
        { 

            //Arrange
            InitialStepsForOfertarHerramientas();
            var expectedHerramientas = new List<string[]> 
            { 
                new string[] { expectedId, expectedNombre, expectedMaterial, expectedFabricante, expectedPrecio }
            };

            //Act
            _selectHerramientasParaOfertasPO.BuscarHerramientas(filtroPrecio, filtroFabricante);

            Thread.Sleep(500); //Esperamos ligeramente a las filas

            //Assert
            Assert.True(_selectHerramientasParaOfertasPO.CheckListOfHerramientas(expectedHerramientas));
        }



    }
}

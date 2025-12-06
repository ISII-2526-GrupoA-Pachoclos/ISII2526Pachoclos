using AppForMovies.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace AppForSEII2526.UIT.CU_Reparar
{
    public class CU_RepararHerramientas_UIT : UC_UIT
    {
        private SelectHerramientasParaRepararPO _selectHerramientasParaRepararPO;
        //private const string nombreH1 = "Destornillador";
        //private const string material1 = "Acero";
        //private const string fabricante1 = "Ana";
        //private const int precio1 = 7;
        //private const string tiempoReparacion1 = "7 dias";


        public CU_RepararHerramientas_UIT(ITestOutputHelper output) : base(output)
        {
            _selectHerramientasParaRepararPO = new SelectHerramientasParaRepararPO(_driver, _output);
        }

        /*
        private void Precondition_perform_login() {
            Perform_login("elena@uclm.es", "Password1234%");
        }
        */

        private void InitialStepsForRepararHerramientas()
        {
            Initial_step_opening_the_web_page();

            // Precondition_perform_login();

            By id = By.Id("CrearReparacion");
            //we wait for the option of the menu to be visible
            _selectHerramientasParaRepararPO.WaitForBeingVisible(id);

            // Esperar un momento adicional para asegurar que Blazor termine de renderizar
            Thread.Sleep(500);

            //we click on the menu
            _driver.FindElement(id).Click();
        }

        /*
        ======================================================
             PRUEBAS DEL SELECT (PASOS 2,3; FLUJO 0)
        ======================================================
        */

        [Theory]
        [InlineData("Destornillador", "7 dias", "Destornillador", "Acero", "Ana", "7 €", "7 dias")] // Filtro por nombre y tiempo de reparación
        [InlineData("Destornillador", "", "Destornillador", "Acero", "Ana", "7 €", "7 dias")] // Filtro solo por nombre
        [InlineData("", "7 dias", "Destornillador", "Acero", "Ana", "7 €", "7 dias")] // Filtro solo por tiempo de reparación
        //[InlineData("", "", "Destornillador", "Acero", "Ana", "7 €", "7 dias")] // Sin filtros (error: ya que esperamos una sola herramienta)
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_2_3_AF0_filteringbyNombreandTiempoReparacion(
            string filtroNombre,
            string filtroTiempoRepracion,
            string expectedNombre,
            string expectedMaterial,
            string expectedFabricante,
            string expectedPrecio,
            string expectedTiempoReparacion)
        {
            //Arrange
            InitialStepsForRepararHerramientas();
            var expectedHerramientas = new List<string[]>
            {
                new string[] { expectedNombre, expectedMaterial, expectedFabricante, expectedPrecio, expectedTiempoReparacion }
            };

            //Act
            _selectHerramientasParaRepararPO.BuscarHerramientas(filtroNombre, filtroTiempoRepracion);

            Thread.Sleep(500); // Esperar ligeramente a las filas

            //Assert
            Assert.True(_selectHerramientasParaRepararPO.CheckListOfHerramientas(expectedHerramientas));
        }

        
    }
}

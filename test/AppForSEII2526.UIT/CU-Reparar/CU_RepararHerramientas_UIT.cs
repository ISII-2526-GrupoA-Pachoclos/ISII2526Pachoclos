using AppForMovies.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_Reparar
{
    public class CU_RepararHerramientas_UIT : UC_UIT
    {
        private SelectHerramientasParaRepararPO _selectHerramientasParaRepararPO;
        private const string nombreH1 = "Destornillador";
        private const string material1 = "Acero";
        private const string fabricante1 = "Ana";
        private const int precio1 = 6;
        private const string tiempoReparacion1 = "2 dias";


        public CU_RepararHerramientas_UIT(ITestOutputHelper output) : base(output)
        {
            _selectHerramientasParaRepararPO = new SelectHerramientasParaRepararPO(_driver, _output);
        }

        private void InitialStepsForRepararHerramientas()
        {
            //Initial_step_opening_the_web_page();

            By id = By.Id("CrearReparacion");
            //we wait for the option of the menu to be visible
            _selectHerramientasParaRepararPO.WaitForBeingVisible(id);
            //we click on the menu
            _driver.FindElement(id).Click();
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_AF1_UC2_4_5_6_filtering()
        {
            //Arrange
            InitialStepsForRepararHerramientas();
            var expectedHerramientas = new List<string[]> { new string[] { nombreH1, material1, fabricante1, precio1.ToString(), tiempoReparacion1 }, };

            //Act
            _selectHerramientasParaRepararPO.BuscarHerramientas("Destornillador", "2 dias");

            //Assert
            Assert.True(_selectHerramientasParaRepararPO.CheckListOfHerramientas(expectedHerramientas));

        }
    }
}

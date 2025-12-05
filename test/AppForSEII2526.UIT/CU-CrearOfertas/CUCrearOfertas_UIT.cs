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
        private SelectHerramientasParaOfertasPO selectHerramientasParaOfertasPO;
        private const string herramientaPrecio = "4";
        private const string herramientaFabricante = "Ana";

        public CUCrearOfertas_UIT(ITestOutputHelper output) : base(output)
        {
            selectHerramientasParaOfertasPO = new SelectHerramientasParaOfertasPO(_driver, _output);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        private void InitialStepsForOfertarHerramientas()
        {
            
            // Esperar a que el menú de navegación se cargue
            selectHerramientasParaOfertasPO.WaitForBeingVisible(By.Id("CreateOfertas"));

            // Hacer clic en el menú
            _driver.FindElement(By.Id("CreateOfertas")).Click();
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_AF1_UC2_4_5_6_filtering()
        {
            //Arrange
            InitialStepsForOfertarHerramientas();
            var expectedHerramientas = new List<string[]> { new string[] { herramientaPrecio, herramientaFabricante }, };

            //Act
            selectHerramientasParaOfertasPO.BuscarHerramientas("2", "");

            //Assert
            Assert.True(selectHerramientasParaOfertasPO.CheckListOfHerramientas(expectedHerramientas));
        }
    }
}

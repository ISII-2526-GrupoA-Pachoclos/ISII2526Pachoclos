using AppForMovies.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_Compras
{
    public class CU_ComprarHerramientas_UIT : UC_UIT
    {

        private SelectHerramientasForCompraPO selectHerramientasForCompraPO;

        private const string NombreHerramienta = "Martillo";
        private const string FabricanteHerramienta = "Jose";
        private const string MaterialHerramienta = "Madera";
        private const int PrecioHerramienta = 6;

        public CU_ComprarHerramientas_UIT(ITestOutputHelper output):base(output)
        {
            selectHerramientasForCompraPO = new SelectHerramientasForCompraPO(_driver, _output);



        }

        private void InitialStepsForCompra() {

            selectHerramientasForCompraPO.WaitForBeingVisible(By.Id("CrearCompra"));

            _driver.FindElement(By.Id("CrearCompra")).Click();
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU1_4_5_FA1_filtrarbyMaterial() { 

            InitialStepsForCompra();
            var expectedHerramientas = new List<string[]>
            {
                new string[] { NombreHerramienta, FabricanteHerramienta, MaterialHerramienta, PrecioHerramienta.ToString() }
            };

            selectHerramientasForCompraPO.BuscarHerramientas("Made", 0);

            Assert.True(selectHerramientasForCompraPO.CheckListaHerramientas(expectedHerramientas));
        }




    }
}
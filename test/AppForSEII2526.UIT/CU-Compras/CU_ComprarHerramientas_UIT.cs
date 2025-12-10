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
        //private const string FabricanteHerramienta = "Jose";
        //private const string MaterialHerramienta = "Madera";
        //private const int PrecioHerramienta = 6;

        public CU_ComprarHerramientas_UIT(ITestOutputHelper output):base(output)
        {
            selectHerramientasForCompraPO = new SelectHerramientasForCompraPO(_driver, _output);



        }

        private void InitialStepsForCompra() {

            Initial_step_opening_the_web_page();

            selectHerramientasForCompraPO.WaitForBeingVisible(By.Id("CrearCompra"));

            Thread.Sleep(500);

            _driver.FindElement(By.Id("CrearCompra")).Click();
        }
        
        [Theory]
        [InlineData("Made", 0, "Martillo", "Jose", "Madera", 6)] //solo filtro por material
        [InlineData("", 6, "Martillo", "Jose", "Madera", 6)] //solo filtro por precio
        [InlineData("Made", 6, "Martillo", "Jose", "Madera", 6)] //filtro por ambos
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU1_4_5_FA1_filtrar(
            string filtroMaterial,
            int filtroPrecio,
            string NombreHerramienta,
            string FabricanteHerramienta,
            string MaterialHerramienta,
            int PrecioHerramienta
            ) { 

            InitialStepsForCompra();
            var expectedHerramientas = new List<string[]>
            {
                new string[] { NombreHerramienta, FabricanteHerramienta, MaterialHerramienta, PrecioHerramienta.ToString() }
            };

            selectHerramientasForCompraPO.BuscarHerramientas(filtroMaterial, filtroPrecio);

            Thread.Sleep(500);

            Assert.True(selectHerramientasForCompraPO.CheckListaHerramientas(expectedHerramientas));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU1_3_FA2_ModificarCarrito() { 

            //Arrange
            InitialStepsForCompra();
            selectHerramientasForCompraPO.BuscarHerramientas("", 0);
            Thread.Sleep(500);

            //Act
            selectHerramientasForCompraPO.AñadirHerramienta(NombreHerramienta);
            Thread.Sleep(500);
            selectHerramientasForCompraPO.QuitarHerramienta(NombreHerramienta);
            Thread.Sleep(500);

            //Assert
            Assert.True(selectHerramientasForCompraPO.ComprarHerramientaNotAvailable());


        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU1_3_FA3_noHerramientas()
        {

            //Arrange
            InitialStepsForCompra();
            selectHerramientasForCompraPO.BuscarHerramientas("", 0);
            Thread.Sleep(500);

            //Act
            //No se añade ninguna herramienta al carrito
            Thread.Sleep(500);

            //Assert
            Assert.True(selectHerramientasForCompraPO.ComprarHerramientaNotAvailable());


        }




    }
}
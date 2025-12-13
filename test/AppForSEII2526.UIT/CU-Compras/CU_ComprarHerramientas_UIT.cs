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
        private CrearCompraPO crearCompraPO;
        private DetallesCompraPO detallesCompraPO;

        private const string NombreHerramienta = "Martillo";
        private const string NombreHerramienta2 = "Destornillador";
        //private const string FabricanteHerramienta = "Jose";
        private const string MaterialHerramienta = "Madera";
        private const int PrecioHerramienta = 6;

        public CU_ComprarHerramientas_UIT(ITestOutputHelper output):base(output)
        {
            selectHerramientasForCompraPO = new SelectHerramientasForCompraPO(_driver, _output);
            crearCompraPO = new CrearCompraPO(_driver, _output);
            detallesCompraPO = new DetallesCompraPO(_driver, _output);


        }

        private void InitialStepsForCompra() {

            Initial_step_opening_the_web_page();

            selectHerramientasForCompraPO.WaitForBeingVisible(By.Id("CrearCompra"));

            Thread.Sleep(500);

            _driver.FindElement(By.Id("CrearCompra")).Click();
        }

        //Flujo Alternativo 1 a los paso 2
        
        [Theory]
        [InlineData("Made", 0, "Martillo", "Jose", "Madera", 6)] //solo filtro por material
        [InlineData("", 6, "Martillo", "Jose", "Madera", 6)] //solo filtro por precio
        [InlineData("Made", 6, "Martillo", "Jose", "Madera", 6)] //filtro por ambos
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU1_2_FA1_filtrar(
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

      

        //Flujo Alternativo 2 al paso 5

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU1_5_FA2_ModificarCarrito() { 

            //Arrange
            InitialStepsForCompra();
            selectHerramientasForCompraPO.BuscarHerramientas("", 0);
            Thread.Sleep(500);

            //Act
            selectHerramientasForCompraPO.AñadirHerramienta(NombreHerramienta);
            Thread.Sleep(500);
            selectHerramientasForCompraPO.AñadirHerramienta(NombreHerramienta2);
            Thread.Sleep(500);
            selectHerramientasForCompraPO.Comprar();
            Thread.Sleep(500);
            crearCompraPO.ModificarCompra();
            Thread.Sleep(500);
            selectHerramientasForCompraPO.QuitarHerramienta(NombreHerramienta2);
            Thread.Sleep(500);
            selectHerramientasForCompraPO.Comprar();
            Thread.Sleep(500);
            var expectedHerramientas = new List<string[]>
            {
                new string[] { NombreHerramienta, MaterialHerramienta, PrecioHerramienta.ToString() }
         
            };
            Thread.Sleep(500);



            //Assert
            Assert.True(crearCompraPO.CheckListaHerramientas(expectedHerramientas));


        }

        //Flujo Alternativo 3 al paso 4
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU1_4_FA3_noHerramientas()
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

        //Flujo Alternativo 4 al paso 6
        [Theory]
        [InlineData("", "Valdes", "calle de juan", "The Nombre field is required.")]
        [InlineData("Juan", "", "calle de juan", "The Apellido field is required.")]
        [InlineData("Juan", "Valdes", "", "The DireccionEnvio field is required.")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU1_6_FA5_camposInvalidos(string nombre, string apellido, string direccion, string error) 
        {

            //Arrange
            InitialStepsForCompra();
            selectHerramientasForCompraPO.BuscarHerramientas("", 0);
            Thread.Sleep(500);

            //Act
            selectHerramientasForCompraPO.AñadirHerramienta(NombreHerramienta);
            Thread.Sleep(500);
            selectHerramientasForCompraPO.Comprar();
            Thread.Sleep(500);
            crearCompraPO.RellenarFormularioCompra(nombre, apellido, direccion);
            Thread.Sleep(500);
            crearCompraPO.SubmitCompra();
            Thread.Sleep(500);

            //Assert
            Assert.True(crearCompraPO.CheckError(error));

        }


        //Flujo Alternativo 5 al paso 6
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU1_6_FA5_noCantidad() {

            //Arrange
            InitialStepsForCompra();
            selectHerramientasForCompraPO.BuscarHerramientas("", 0);
            Thread.Sleep(500);

            //Act
            selectHerramientasForCompraPO.AñadirHerramienta(NombreHerramienta);
            Thread.Sleep(500);
            selectHerramientasForCompraPO.Comprar();
            Thread.Sleep(500);
            crearCompraPO.RellenarFormularioCompra("Juan", "Valdes", "calle de juan");
            Thread.Sleep(500);
            crearCompraPO.rellenarCantidad(3, 0);
            Thread.Sleep(500);
            crearCompraPO.SubmitCompra();
            Thread.Sleep(500);
            crearCompraPO.ConfirmCompra();
            Thread.Sleep(500);

            //Assert
            Assert.True(crearCompraPO.CheckError("La cantidad debe ser un número positivo mayor que 0."));



        }

        //Flujo Básico
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU1_FB_CompraHerramienta() {

            //Arrange
            InitialStepsForCompra();
            selectHerramientasForCompraPO.BuscarHerramientas("", 0);
            Thread.Sleep(500);
            var expectedHerramientas = new List<string[]>
            {
                new string[] { NombreHerramienta, 1.ToString(), MaterialHerramienta, PrecioHerramienta.ToString(), "" }

            };

            //Act
            selectHerramientasForCompraPO.AñadirHerramienta(NombreHerramienta);
            Thread.Sleep(500);
            selectHerramientasForCompraPO.Comprar();
            Thread.Sleep(500);
            crearCompraPO.RellenarFormularioCompra("Juan", "Valdes", "calle de juan");
            Thread.Sleep(500);
            crearCompraPO.SubmitCompra();
            Thread.Sleep(500);
            crearCompraPO.ConfirmCompra();
            Thread.Sleep(500);

            //Assert

            Assert.True(detallesCompraPO.CheckDetallesCompra("Juan", "Valdes", "calle de juan", DateTime.Today, PrecioHerramienta));

            Assert.True(detallesCompraPO.CheckListaHerramientas(expectedHerramientas));


        }






    }
}
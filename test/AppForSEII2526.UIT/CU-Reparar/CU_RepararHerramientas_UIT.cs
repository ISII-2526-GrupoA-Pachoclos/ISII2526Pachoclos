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
        private CrearReparacionPO _crearReparacionPO;
        private const string nombreH1 = "Destornillador";
        private const int idH1 = 2;
        //private const string material1 = "Acero";
        //private const string fabricante1 = "Ana";
        //private const int precio1 = 7;
        //private const string tiempoReparacion1 = "7 dias";


        public CU_RepararHerramientas_UIT(ITestOutputHelper output) : base(output)
        {
            _selectHerramientasParaRepararPO = new SelectHerramientasParaRepararPO(_driver, _output);
            _crearReparacionPO = new CrearReparacionPO(_driver, _output);
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
        ============================
             PRUEBAS DEL SELECT 
        ============================
        */

        // PASOS 2 y 3, FLUJO ALTERNATIVO 0
        [Theory]
        [InlineData("Destornillador", "7 dias", "Destornillador", "Acero", "Ana", "7 €", "7 dias")] // Filtro por nombre y tiempo de reparación
        [InlineData("Destornillador", "", "Destornillador", "Acero", "Ana", "7 €", "7 dias")] // Filtro solo por nombre
        [InlineData("", "7 dias", "Destornillador", "Acero", "Ana", "7 €", "7 dias")] // Filtro solo por tiempo de reparación
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

        // PASOS 3, FLUJO ALTERNATIVO 2
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_3_AF2_ModificarCarrito()
        {
            //Arrange
            InitialStepsForRepararHerramientas();
            _selectHerramientasParaRepararPO.BuscarHerramientas("", "");
            Thread.Sleep(500);

            //Act
            _selectHerramientasParaRepararPO.AddHerramientaToCart(nombreH1);
            Thread.Sleep(300);
            _selectHerramientasParaRepararPO.RemoveHerramientaFromCart(nombreH1);
            Thread.Sleep(300);

            //Assert
            Assert.True(_selectHerramientasParaRepararPO.RepararHerramientasNotAvailable());
        }

        // PASO 4, FLUJO ALTERNATIVO 3
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_4_AF3_CarritoVacioBotonInactivo()
        {
            //Arrange
            InitialStepsForRepararHerramientas();
            _selectHerramientasParaRepararPO.BuscarHerramientas("", "");
            Thread.Sleep(500);

            //Act (nada)
            // Sin añadir herramientas al carrito

            //Assert
            Assert.True(_selectHerramientasParaRepararPO.RepararHerramientasNotAvailable());
        }


        /*
        ===================================
             PRUEBAS DEL SELECT + POST
        ===================================
        */

        // PASO 6, FLUJO ALTERNATIVO 1 - Fecha de entrega anterior
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_6_AF1_FechaEntregaAnteriorAHoy()
        {
            // Arrange
            InitialStepsForRepararHerramientas();
            _selectHerramientasParaRepararPO.BuscarHerramientas("", "");
            Thread.Sleep(500);
            _selectHerramientasParaRepararPO.AddHerramientaToCart(nombreH1);
            _selectHerramientasParaRepararPO.ClickRepararHerramientas();

            DateTime fechaAnterior = DateTime.Today.AddDays(-1);

            // Act
            _crearReparacionPO.RellenarFormularioReparacion("pibi", "ronaldo", fechaAnterior);
            Thread.Sleep(500);
            _crearReparacionPO.RellenarDescripcionReparacion("Mago roto", idH1);
            Thread.Sleep(500);
            _crearReparacionPO.ClickSubmitButton();
            Thread.Sleep(500);
            _crearReparacionPO.ConfirmDialog();
            Thread.Sleep(500);

            // Assert
            Assert.True(_crearReparacionPO.CheckValidationError("La fecha de entrega debe ser igual o posterior a hoy"));
        }

        // PASO 6, FLUJO ALTERNATIVO 4 - Datos obligatorios no rellenados
        [Theory]
        [InlineData("", "ronaldo", 5, "The NombreC field is required.")]
        [InlineData("pibi", "", 5, "The Apellidos field is required.")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_6_AF4_DatosObligatoriosNoRellenados(
            string nombre,
            string apellidos,
            int diasDesdeHoy,
            string expectedError)
        {
            // Arrange
            var fechaEntrega = DateTime.Today.AddDays(diasDesdeHoy);

            InitialStepsForRepararHerramientas();
            _selectHerramientasParaRepararPO.BuscarHerramientas("", "");
            Thread.Sleep(500);
            _selectHerramientasParaRepararPO.AddHerramientaToCart(nombreH1);
            Thread.Sleep(500);
            _selectHerramientasParaRepararPO.ClickRepararHerramientas();
            
            // Act
            _crearReparacionPO.RellenarFormularioReparacion(nombre, apellidos, fechaEntrega);
            Thread.Sleep(500);
            _crearReparacionPO.RellenarDescripcionReparacion("Mago roto", idH1);
            Thread.Sleep(500);
            _crearReparacionPO.ClickSubmitButton();
            Thread.Sleep(500);

            // Assert
            Assert.True(_crearReparacionPO.CheckValidationError(expectedError), $"Expected error: {expectedError}");
        }

        
    }
}

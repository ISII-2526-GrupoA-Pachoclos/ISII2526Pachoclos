using AppForMovies.UIT.Shared;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace AppForSEII2526.UIT.CU_CrearOfertas
{
    public class CUCrearOfertas_UIT : UC_UIT
    {
        private SelectHerramientasParaOfertasPO _selectHerramientasParaOfertasPO;
        private CrearOfertaPO _crearOfertaPO;
        private DetalleOfertaPO _detalleOfertaPO;

        private const string nombreHerramienta1 = "Destornillador";
        private const string nombreHerramienta2 = "Martillo";
        private const int idH1 = 2;
        private const string materialH1 = "Acero";
        private const string fabricanteH1 = "Ana";
        private const float precioH1float = 7f;
        private const string precioH1String = "7 €";
        private const int porcentajeDescuento = 50;



        public CUCrearOfertas_UIT(ITestOutputHelper output) : base(output)
        {
            _selectHerramientasParaOfertasPO = new SelectHerramientasParaOfertasPO(_driver, _output);
            _crearOfertaPO = new CrearOfertaPO(_driver, _output);
            _detalleOfertaPO = new DetalleOfertaPO(_driver, _output);
        }

        /*
        private void Precondition_perform_login() {
            Perform_login("elena@uclm.es", "Password1234%");
        }
        */

        private void InitialStepsForOfertarHerramientas()
        {
            //We go to the home page
            Initial_step_opening_the_web_page();

            //Precondition_perform_login();


            // We wait for the option of the menu to be visible
            _selectHerramientasParaOfertasPO.WaitForBeingVisible(By.Id("CreateOfertas"));

            // Esperar un momento adicional para asegurar que Blazor termine de renderizar
            Thread.Sleep(500);

            // we click on the menu
            _driver.FindElement(By.Id("CreateOfertas")).Click();
        }

        /*
        ============================================================================
                                 PRUEBAS DEL SELECT 
        ============================================================================
        */

        // PASOS 2 y 3, FLUJO ALTERNATIVO 0 - FILTROS
        [Theory]
        [InlineData("6", "Jose", "3", "Martillo", "Madera", "Jose", "6")] // Filtro por precio y fabricante
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
            Thread.Sleep(500); // Esperar ligeramente a las filas

            //Assert
            Assert.True(_selectHerramientasParaOfertasPO.CheckListOfHerramientas(expectedHerramientas));
        }

        // PASO 4, FLUJO ALTERNATIVO 4 - CARRITO VACÍO
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_4_AF4_CarritoVacioBotonInactivo()
        {
            //Arrange
            InitialStepsForOfertarHerramientas();
            _selectHerramientasParaOfertasPO.BuscarHerramientas("", "");
            Thread.Sleep(500);

            //Act
            //No se añade ninguna herramienta al carrito

            //Assert
            Assert.True(_selectHerramientasParaOfertasPO.OfertarHerramientasNotAvailable());
        }

        /*
        ============================================================================
                              PRUEBAS DEL SELECT + POST
        ============================================================================
        */
        //PASO 6, FLUJO ALTERNATIVO 1 - VALIDACIONES DE FECHAS
        [Theory]
        [InlineData(-1, 7, "La fecha de inicio debe ser posterior o igual a la fecha actual.")] // Fecha inicio anterior a hoy
        [InlineData(9, 1, "La fecha de fin debe ser posterior a la fecha de inicio.")] // Fecha fin anterior a fecha inicio
        [InlineData(1, 6, "¡Error, la oferta debe durar al menos 1 semana!")] // Duración menor a 1 semana
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_6_AF1_ValidacionesFechas(
            int diasFechaInicio,
            int diasFechaFin,
            string expectedError)
        {
            //Arrange
            InitialStepsForOfertarHerramientas();
            _selectHerramientasParaOfertasPO.BuscarHerramientas("", "");
            Thread.Sleep(500);
            _selectHerramientasParaOfertasPO.AddHerramientaToCarrito(nombreHerramienta1);
            _selectHerramientasParaOfertasPO.ClickOfertarHerramientas();
            Thread.Sleep(500);

            DateTime fechaInicio = DateTime.Today.AddDays(diasFechaInicio);
            DateTime fechaFin = DateTime.Today.AddDays(diasFechaFin);

            //Act
            _crearOfertaPO.RellenarFormularioOferta(fechaInicio, fechaFin);
            Thread.Sleep(500);
            _crearOfertaPO.RellenarPorcentajeDescuentoOferta(idH1, porcentajeDescuento);
            Thread.Sleep(500);
            _crearOfertaPO.ClickSubmitButton();
            Thread.Sleep(500); // Dar tiempo para que aparezca el error de validación
            _crearOfertaPO.ConfirmDialog();
            Thread.Sleep(500);

            //Assert
            Assert.True(_crearOfertaPO.CheckValidationError(expectedError), $"Expected error: {expectedError}");

        }

        //PASO 6, FLUJO ALTERNATIVO 3 - PORCENTAJE INCORRECTO
        [Theory]
        [InlineData(-1, "The field Porcentaje must be between 0 and 100.")]
        [InlineData(101, "The field Porcentaje must be between 0 and 100.")]
        public void UC3_6_AF3_PorcentajeIncorrecto(
            int porcentaje,
            string expectedError)
        {
            //Arrange
            InitialStepsForOfertarHerramientas();
            _selectHerramientasParaOfertasPO.BuscarHerramientas("", "");
            Thread.Sleep(500);
            _selectHerramientasParaOfertasPO.AddHerramientaToCarrito(nombreHerramienta1);
            Thread.Sleep(500);
            _selectHerramientasParaOfertasPO.ClickOfertarHerramientas();

            //Act
            DateTime fechaInicio = DateTime.Today;
            DateTime fechaFin = DateTime.Today.AddDays(7);

            _crearOfertaPO.RellenarFormularioOferta(fechaInicio, fechaFin);
            Thread.Sleep(500);
            _crearOfertaPO.RellenarPorcentajeDescuentoOferta(idH1, porcentaje);
            Thread.Sleep(500);
            _crearOfertaPO.ClickSubmitButton();
            Thread.Sleep(500);

            //Assert
            Assert.True(_crearOfertaPO.CheckValidationError(expectedError), $"Expected error: {expectedError}");
        }


        //PASO 5, FLUJO ALTERNATIVO 2 - MODIFICAR CARRITO DESDE CREAR OFERTA
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_5_AF3_ModificarCarrito()
        {
            //Arrange
            InitialStepsForOfertarHerramientas();
            _selectHerramientasParaOfertasPO.BuscarHerramientas("", "");
            Thread.Sleep(500);

            //Act
            _selectHerramientasParaOfertasPO.AddHerramientaToCarrito(nombreHerramienta1);
            Thread.Sleep(500);
            _selectHerramientasParaOfertasPO.AddHerramientaToCarrito(nombreHerramienta2);
            Thread.Sleep(500);
            
            _selectHerramientasParaOfertasPO.ClickOfertarHerramientas();
            _crearOfertaPO.PressModifyHerramientasButton();
            Thread.Sleep(500);
            _selectHerramientasParaOfertasPO.RemoveHerramientaFromCarrito(nombreHerramienta2);
            Thread.Sleep(500);
            _selectHerramientasParaOfertasPO.ClickOfertarHerramientas();

            //Assert
            var expectedHerramientas = new List<string[]>
            {
                new string[] { nombreHerramienta1, materialH1, precioH1float.ToString() } //Columnas de la herramienta en el POST de Crear Ofertas 
            };

            Assert.True(_crearOfertaPO.CheckListOfHerramientasParaOfertar(expectedHerramientas));
        }

        //PASO 6, FLUJO ALTERNATIVO 5 - DATOS OBLIGATORIOS NO RELLENADOS
        [Theory]
        [InlineData(null, 25, 31, "The Porcentaje field must be a number.")]
        [InlineData(50, null, 31, "The FechaInicio field must be a date.")]
        [InlineData(50, 25, null, "The FechaFin field must be a date.")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_6_AF5_DatosObligatoriosNoRellenados(
            int? porcentaje,
            int? diasFechaInicio,
            int? diasFechaFin,
            string expectedError)
        {
            //Arrange
            InitialStepsForOfertarHerramientas();
            _selectHerramientasParaOfertasPO.BuscarHerramientas("", "");
            Thread.Sleep(500);
            _selectHerramientasParaOfertasPO.AddHerramientaToCarrito(nombreHerramienta1);
            Thread.Sleep(500);
            _selectHerramientasParaOfertasPO.ClickOfertarHerramientas();
            Thread.Sleep(500);

            //Act
            _crearOfertaPO.RellenarFechaOpcional(By.Id("FechaInicio"), diasFechaInicio);
            _crearOfertaPO.RellenarFechaOpcional(By.Id("FechaFin"), diasFechaFin);
            _crearOfertaPO.RellenarPorcentajeOpcional(idH1, porcentaje);
            Thread.Sleep(500);

            _crearOfertaPO.ClickSubmitButton();
            Thread.Sleep(2000);

            //Assert 
            Assert.True(_crearOfertaPO.CheckValidationError(expectedError));
        }

        /*
        ============================================================================
                              PRUEBAS DEL SELECT + POST + DETAIL
        ============================================================================
        */

        //PASOS 1-7, FLUJO BÁSICO COMPLETO - CREAR OFERTA CORRECTAMENTE
        [Theory]
        [InlineData(1, 8, 50)] // Oferta de 7 días con 50% descuento
        [InlineData(1, 10, 25)] // Oferta de 9 días con 25% descuento
        [InlineData(0, 7, 75)] // Oferta desde hoy con 75% descuento
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_1_2_3_4_5_6_7_FlujoBásico(
            int diasFechaInicio,
            int diasFechaFin,
            int porcentaje)
        {
            //Arrange
            InitialStepsForOfertarHerramientas();
            _selectHerramientasParaOfertasPO.BuscarHerramientas("", "");
            Thread.Sleep(500);
            _selectHerramientasParaOfertasPO.AddHerramientaToCarrito(nombreHerramienta1);
            Thread.Sleep(500);
            _selectHerramientasParaOfertasPO.ClickOfertarHerramientas();
            Thread.Sleep(500);

            DateTime fechaInicio = DateTime.Today.AddDays(diasFechaInicio);
            DateTime fechaFin = DateTime.Today.AddDays(diasFechaFin);
            DateTime fechaOferta = DateTime.Today;

            // Calcular el precio con descuento esperado
            float precioOfertaEsperado = precioH1float * (100 - porcentaje) / 100f;

            //Act
            _crearOfertaPO.RellenarFormularioOferta(fechaInicio, fechaFin);
            Thread.Sleep(500);
            _crearOfertaPO.RellenarPorcentajeDescuentoOferta(idH1, porcentaje);
            Thread.Sleep(500);
            _crearOfertaPO.ClickSubmitButton();
            Thread.Sleep(500);
            _crearOfertaPO.ConfirmDialog();
            Thread.Sleep(500); 

            //Assert
            Assert.True(_detalleOfertaPO.CheckOfertaDetail(
                fechaInicio,
                fechaFin,
                fechaOferta,
                "Tarjeta",
                "Socios",
                1,
                precioOfertaEsperado
            ), "Error: los detalles de la oferta no son los esperados");

            var expectedHerramientas = new List<string[]>
            {
                new string[] { 
                    idH1.ToString(), 
                    nombreHerramienta1, 
                    materialH1, 
                    fabricanteH1, 
                    precioH1float.ToString("F2") + " €", 
                    porcentaje.ToString() + " %", 
                    precioOfertaEsperado.ToString("F2") + " €"
                }
            };

            Assert.True(_detalleOfertaPO.CheckListOfHerramientasOfertadas(expectedHerramientas),
                "Error: la lista de herramientas ofertadas no es la esperada");
        }

    }
}

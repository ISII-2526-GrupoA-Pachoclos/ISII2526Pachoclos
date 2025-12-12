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
        private const string nombreHerramienta = "Destornillador";
        private const int herramientaId = 2;
        //private const string material1 = "Acero";
        //private const string fabricante1 = "Ana";
        //private const int precio1 = 7;

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
            Initial_step_opening_the_web_page();

            //Precondition_perform_login();

            By id = By.Id("CreateOfertas");
            // We wait for the option of the menu to be visible
            _selectHerramientasParaOfertasPO.WaitForBeingVisible(id);

            // Esperar un momento adicional para asegurar que Blazor termine de renderizar
            Thread.Sleep(500);

            // we click on the menu
            _driver.FindElement(id).Click();
        }

        private void AñadirHerramientaYProcederACrearOferta(string nombreHerramienta)
        {
            _selectHerramientasParaOfertasPO.BuscarHerramientas("", "");
            Thread.Sleep(500);
            _selectHerramientasParaOfertasPO.AddHerramientaToCarrito(nombreHerramienta);
            Thread.Sleep(500);

            // Click en el botón de Ofertar Herramientas
            By botonOfertar = By.Id("ofertarHerramientaButton");
            _selectHerramientasParaOfertasPO.WaitForBeingClickable(botonOfertar);
            _driver.FindElement(botonOfertar).Click();
            Thread.Sleep(500);
        }

        /*
        ============================================================================
                                 PRUEBAS DEL SELECT 
        ============================================================================
        */

        // PASOS 2 y 3, FLUJO ALTERNATIVO 0
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

        // PASO 3, FLUJO ALTERNATIVO 2
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_3_AF2_ModificarCarritoHerramientas()
        {
            //Arrange
            InitialStepsForOfertarHerramientas();
            _selectHerramientasParaOfertasPO.BuscarHerramientas("", "");
            Thread.Sleep(500);

            //Act
            _selectHerramientasParaOfertasPO.AddHerramientaToCarrito(nombreHerramienta);
            Thread.Sleep(500);
            _selectHerramientasParaOfertasPO.RemoveHerramientaFromCarrito(nombreHerramienta);
            Thread.Sleep(500);

            //Assert
            Assert.True(_selectHerramientasParaOfertasPO.OfertarHerramientasNotAvailable());
        }

        // PASO 4, FLUJO ALTERNATIVO 4
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
            Thread.Sleep(500);

            //Assert
            Assert.True(_selectHerramientasParaOfertasPO.OfertarHerramientasNotAvailable());
        }



        /*
        ============================================================================
                                 PRUEBAS DEL POST
        ============================================================================
        */
        // PASOS 5, 6, 7 - FLUJO BÁSICO
        [Theory]
        [InlineData("Martillo", 3, 2, 10, "PayPal", "Socios", 30)]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_5_6_7_FB_CrearOfertaExitosa(
            string nombreHerramienta,
            int herramientaId,
            int diasInicio,
            int diasDuracion,
            string metodoPago,
            string dirigidaA,
            int porcentaje)
        {
            //Arrange
            InitialStepsForOfertarHerramientas();
            AñadirHerramientaYProcederACrearOferta(nombreHerramienta);

            DateTime fechaInicio = DateTime.Today.AddDays(diasInicio);
            DateTime fechaFin = DateTime.Today.AddDays(diasInicio + diasDuracion);

            //Act
            _crearOfertaPO.IntroducirFechaInicio(fechaInicio);
            _crearOfertaPO.IntroducirFechaFin(fechaFin);
            _crearOfertaPO.SeleccionarMetodoPago(metodoPago);
            _crearOfertaPO.SeleccionarDirigidaA(dirigidaA);
            _crearOfertaPO.IntroducirPorcentajeDescuento(herramientaId, porcentaje);
            Thread.Sleep(500);

            _crearOfertaPO.ClickCrearOferta();
            Thread.Sleep(500);
            _crearOfertaPO.ConfirmarCrearOferta();
            Thread.Sleep(1000);

            //Assert
            // Verificar que estamos en la página de detalle de oferta
            Assert.Contains("/Ofertas/DetalleOfertas", _driver.Url);

            // Verificar los datos de la oferta
            Assert.Equal(fechaInicio.ToString("dd/MM/yyyy"), _detalleOfertaPO.GetFechaInicio().Split(' ')[0]);
            Assert.Equal(fechaFin.ToString("dd/MM/yyyy"), _detalleOfertaPO.GetFechaFin().Split(' ')[0]);
            Assert.Equal(metodoPago, _detalleOfertaPO.GetMetodoPago());
            Assert.Equal(dirigidaA, _detalleOfertaPO.GetTiposDirigidaOferta());
            Assert.Equal("1", _detalleOfertaPO.GetCantidadOfertaItems());
        }

        // PASO 5, FLUJO ALTERNATIVO 1 - Fecha anterior a hoy
        [Theory]
        [InlineData("Destornillador", 2, -1, 7, "Tarjeta", "Clientes", 50, "La fecha de inicio debe ser posterior o igual a la fecha actual")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_5_AF1_FechaInicioAnteriorAHoy(
            string nombreHerramienta,
            int herramientaId,
            int diasInicio,
            int diasDuracion,
            string metodoPago,
            string dirigidaA,
            int porcentaje,
            string mensajeErrorEsperado)
        {
            //Arrange
            InitialStepsForOfertarHerramientas();
            AñadirHerramientaYProcederACrearOferta(nombreHerramienta);

            DateTime fechaInicio = DateTime.Today.AddDays(diasInicio);
            DateTime fechaFin = DateTime.Today.AddDays(diasInicio + diasDuracion);

            //Act
            _crearOfertaPO.IntroducirFechaInicio(fechaInicio);
            _crearOfertaPO.IntroducirFechaFin(fechaFin);
            _crearOfertaPO.SeleccionarMetodoPago(metodoPago);
            _crearOfertaPO.SeleccionarDirigidaA(dirigidaA);
            _crearOfertaPO.IntroducirPorcentajeDescuento(herramientaId, porcentaje);
            Thread.Sleep(500);

            _crearOfertaPO.ClickCrearOferta();
            Thread.Sleep(500);
            _crearOfertaPO.ConfirmarCrearOferta();
            Thread.Sleep(1000);

            //Assert
            Assert.True(_crearOfertaPO.CheckErrorMessage(mensajeErrorEsperado));
        }

        // PASO 5, FLUJO ALTERNATIVO 1 - Fecha fin menor que fecha inicio
        [Theory]
        [InlineData("Destornillador", 2, 10, -5, "Tarjeta", "Clientes", 50, "La fecha de fin debe ser posterior a la fecha de inicio")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_5_AF1_FechaFinMenorQueFechaInicio(
            string nombreHerramienta,
            int herramientaId,
            int diasInicio,
            int diasDuracion,
            string metodoPago,
            string dirigidaA,
            int porcentaje,
            string mensajeErrorEsperado)
        {
            //Arrange
            InitialStepsForOfertarHerramientas();
            AñadirHerramientaYProcederACrearOferta(nombreHerramienta);

            DateTime fechaInicio = DateTime.Today.AddDays(diasInicio);
            DateTime fechaFin = DateTime.Today.AddDays(diasInicio + diasDuracion);

            //Act
            _crearOfertaPO.IntroducirFechaInicio(fechaInicio);
            _crearOfertaPO.IntroducirFechaFin(fechaFin);
            _crearOfertaPO.SeleccionarMetodoPago(metodoPago);
            _crearOfertaPO.SeleccionarDirigidaA(dirigidaA);
            _crearOfertaPO.IntroducirPorcentajeDescuento(herramientaId, porcentaje);
            Thread.Sleep(500);

            _crearOfertaPO.ClickCrearOferta();
            Thread.Sleep(500);
            _crearOfertaPO.ConfirmarCrearOferta();
            Thread.Sleep(1000);

            //Assert
            Assert.True(_crearOfertaPO.CheckErrorMessage(mensajeErrorEsperado));
        }

        // PASO 5, FLUJO ALTERNATIVO 1 - Oferta menor a 1 semana (requisito del examen)
        [Theory]
        [InlineData("Destornillador", 2, 1, 5, "Efectivo", "Clientes", 25, "la oferta debe durar al menos 1 semana")]
        [InlineData("Martillo", 3, 2, 3, "Tarjeta", "Socios", 40, "la oferta debe durar al menos 1 semana")]
        [InlineData("Llave Inglesa", 1, 1, 6, "PayPal", "Clientes", 15, "la oferta debe durar al menos 1 semana")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_5_AF1_OfertaMenorAUnaSemana(
            string nombreHerramienta,
            int herramientaId,
            int diasInicio,
            int diasDuracion,
            string metodoPago,
            string dirigidaA,
            int porcentaje,
            string mensajeErrorEsperado)
        {
            //Arrange
            InitialStepsForOfertarHerramientas();
            AñadirHerramientaYProcederACrearOferta(nombreHerramienta);

            DateTime fechaInicio = DateTime.Today.AddDays(diasInicio);
            DateTime fechaFin = DateTime.Today.AddDays(diasInicio + diasDuracion);

            //Act
            _crearOfertaPO.IntroducirFechaInicio(fechaInicio);
            _crearOfertaPO.IntroducirFechaFin(fechaFin);
            _crearOfertaPO.SeleccionarMetodoPago(metodoPago);
            _crearOfertaPO.SeleccionarDirigidaA(dirigidaA);
            _crearOfertaPO.IntroducirPorcentajeDescuento(herramientaId, porcentaje);
            Thread.Sleep(500);

            _crearOfertaPO.ClickCrearOferta();
            Thread.Sleep(500);
            _crearOfertaPO.ConfirmarCrearOferta();
            Thread.Sleep(1000);

            //Assert
            Assert.True(_crearOfertaPO.CheckErrorMessage(mensajeErrorEsperado));
        }

        // PASO 5, FLUJO ALTERNATIVO 2 - Modificar carrito de ofertas
        [Theory]
        [InlineData("Destornillador")]
        [InlineData("Martillo")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_5_AF2_ModificarCarritoDeOfertas(string nombreHerramienta)
        {
            //Arrange
            InitialStepsForOfertarHerramientas();
            AñadirHerramientaYProcederACrearOferta(nombreHerramienta);

            //Act
            _crearOfertaPO.ClickModificarHerramientas();
            Thread.Sleep(500);

            //Assert
            // Verificamos que volvemos a la página de selección
            Assert.Contains("/Ofertas/SelectHerramientasForOfertas", _driver.Url);
        }

        // PASO 5, FLUJO ALTERNATIVO 3 - Porcentaje inválido
        [Theory]
        [InlineData("Destornillador", 2, 1, 8, "Tarjeta", "Clientes", 101, "El porcentaje debe estar entre 1 y 100")]
        [InlineData("Martillo", 3, 2, 10, "PayPal", "Socios", 0, "El porcentaje debe estar entre 1 y 100")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_5_AF3_PorcentajeInvalido(
            string nombreHerramienta,
            int herramientaId,
            int diasInicio,
            int diasDuracion,
            string metodoPago,
            string dirigidaA,
            int porcentajeInvalido,
            string mensajeErrorEsperado)
        {
            //Arrange
            InitialStepsForOfertarHerramientas();
            AñadirHerramientaYProcederACrearOferta(nombreHerramienta);

            DateTime fechaInicio = DateTime.Today.AddDays(diasInicio);
            DateTime fechaFin = DateTime.Today.AddDays(diasInicio + diasDuracion);

            //Act
            _crearOfertaPO.IntroducirFechaInicio(fechaInicio);
            _crearOfertaPO.IntroducirFechaFin(fechaFin);
            _crearOfertaPO.SeleccionarMetodoPago(metodoPago);
            _crearOfertaPO.SeleccionarDirigidaA(dirigidaA);
            _crearOfertaPO.IntroducirPorcentajeDescuento(herramientaId, porcentajeInvalido);
            Thread.Sleep(500);

            _crearOfertaPO.ClickCrearOferta();
            Thread.Sleep(500);
            _crearOfertaPO.ConfirmarCrearOferta();
            Thread.Sleep(1000);

            //Assert
            Assert.True(_crearOfertaPO.CheckErrorMessage(mensajeErrorEsperado));
        }


        // PASO 7, FLUJO ALTERNATIVO 5 - Datos obligatorios no rellenados
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_7_AF5_DatosObligatoriosNoRellenados()
        {
            //Arrange
            InitialStepsForOfertarHerramientas();
            AñadirHerramientaYProcederACrearOferta("Destornillador");

            //Act
            // No introducimos fechas ni porcentaje (campos obligatorios)
            _crearOfertaPO.SeleccionarMetodoPago("Tarjeta");
            _crearOfertaPO.SeleccionarDirigidaA("Clientes");
            Thread.Sleep(500);

            _crearOfertaPO.ClickCrearOferta();
            Thread.Sleep(500);

            //Assert
            // El diálogo no debería aparecer o debería haber errores de validación
            // debido a campos obligatorios no completados
            Assert.True(_crearOfertaPO.CheckErrorMessage("") || !_crearOfertaPO.IsSubmitButtonEnabled());
        }
    }
}

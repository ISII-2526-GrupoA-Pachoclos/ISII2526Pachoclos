using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_CrearOfertas
{
    public class CrearOfertaPO : PageObject
    {
        private By buttonFechaInicio = By.Id("FechaInicio");
        private By buttonFechaFin = By.Id("FechaFin");
        private By buttonMetodoPago = By.Id("TiposMetodoPago");
        private By buttonDirigidaA = By.Id("TiposDirigidaOferta");
        private By buttonCrearOferta = By.Id("Submit");
        private By buttonModifyHerramientas = By.Id("ModifyHerramientas");
        private By tableOfertaItems = By.Id("TablaDeOfertaItems");
        private By errorsShown = By.Id("ErrorsShown");
        private By modalDialogOk = By.Id("Button_DialogOK");
        private By modalDialogCancel = By.Id("Button_DialogCancel");


        public CrearOfertaPO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public void IntroducirFechaInicio(DateTime fecha)
        {
            WaitForBeingClickable(buttonFechaInicio);
            InputDateInDatePicker(buttonFechaInicio, fecha);
        }

        public void IntroducirFechaFin(DateTime fecha)
        {
            WaitForBeingClickable(buttonFechaFin);
            InputDateInDatePicker(buttonFechaFin, fecha);
        }

        public void SeleccionarMetodoPago(string metodoPago)
        {
            WaitForBeingClickable(buttonMetodoPago);
            SelectElement selectElement = new SelectElement(_driver.FindElement(buttonMetodoPago));
            selectElement.SelectByText(metodoPago);
        }

        public void SeleccionarDirigidaA(string dirigidaA)
        {
            WaitForBeingClickable(buttonDirigidaA);
            SelectElement selectElement = new SelectElement(_driver.FindElement(buttonDirigidaA));
            selectElement.SelectByText(dirigidaA);
        }

        public void IntroducirPorcentajeDescuento(int herramientaId, int porcentaje)
        {
            By inputPorcentaje = By.Id($"Porcentaje_{herramientaId}");
            WaitForBeingClickable(inputPorcentaje);
            var inputElement = _driver.FindElement(inputPorcentaje);
            inputElement.Clear();
            inputElement.SendKeys(porcentaje.ToString());
        }

        public void ClickCrearOferta()
        {
            WaitForBeingClickable(buttonCrearOferta);
            _driver.FindElement(buttonCrearOferta).Click();
        }

        public void ClickModificarHerramientas()
        {
            WaitForBeingClickable(buttonModifyHerramientas);
            _driver.FindElement(buttonModifyHerramientas).Click();
        }

        public void ConfirmarCrearOferta()
        {
            WaitForBeingClickable(modalDialogOk);
            _driver.FindElement(modalDialogOk).Click();
        }

        public void CancelarCrearOferta()
        {
            WaitForBeingClickable(modalDialogCancel);
            _driver.FindElement(modalDialogCancel).Click();
        }

        public bool CheckOfertaItemsInTable(List<string[]> expectedItems)
        {
            return CheckBodyTable(expectedItems, tableOfertaItems);
        }

        public bool CheckErrorMessage(string expectedError)
        {
            try
            {
                WaitForBeingVisible(errorsShown);
                var errorText = _driver.FindElement(errorsShown).Text;
                return errorText.Contains(expectedError);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsSubmitButtonEnabled()
        {
            try
            {
                return _driver.FindElement(buttonCrearOferta).Enabled;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetPrecioTotalOferta()
        {
            // Buscar el elemento que contiene "Precio Total Oferta"
            var priceElement = _driver.FindElements(By.TagName("p"))
                .FirstOrDefault(p => p.Text.Contains("Precio Total Oferta"));

            if (priceElement != null)
            {
                // Extraer el precio del texto
                var text = priceElement.Text;
                var startIndex = text.IndexOf("Precio Total Oferta:") + "Precio Total Oferta:".Length;
                var endIndex = text.IndexOf("€", startIndex);
                return text.Substring(startIndex, endIndex - startIndex).Trim();
            }
            return string.Empty;
        }
    }
}

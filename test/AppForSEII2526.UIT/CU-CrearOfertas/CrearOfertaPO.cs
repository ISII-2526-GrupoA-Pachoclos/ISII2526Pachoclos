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
        private By buttonOfertarHerramientas = By.Id("Submit");
        private By dialogOkButton = By.Id("Button_DialogOK");
        private By modificarHerramientasButton = By.Id("ModifyHerramientas");
        private By tableOfOfertasItemsBy = By.Id("TablaDeOfertaItems");

        public CrearOfertaPO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {

        }

        public void RellenarFormularioOferta(DateTime fechaInicio, DateTime fechaFin)
        {
            InputDateInDatePicker(buttonFechaInicio, fechaInicio);
            InputDateInDatePicker(buttonFechaFin, fechaFin);
        }

        public void RellenarPorcentajeDescuentoOferta(int hID, int porcentajeDescuento)
        {
            WaitForBeingClickable(By.Id($"Porcentaje_{hID}"));
            _driver.FindElement(By.Id($"Porcentaje_{hID}")).Clear();
            _driver.FindElement(By.Id($"Porcentaje_{hID}")).SendKeys(porcentajeDescuento.ToString());
        }

        public bool CheckValidationError(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);
        }

        public void ClickSubmitButton()
        {
            WaitForBeingClickable(buttonOfertarHerramientas);
            _driver.FindElement(buttonOfertarHerramientas).Click();
        }

        public void ConfirmDialog()
        {
            WaitForBeingClickable(dialogOkButton);
            _driver.FindElement(dialogOkButton).Click();
        }

        public void PressModifyHerramientasButton()
        {
            WaitForBeingClickable(modificarHerramientasButton);
            _driver.FindElement(modificarHerramientasButton).Click();
        }

        public bool CheckListOfHerramientasParaOfertar(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, tableOfOfertasItemsBy);
        }

        public void RellenarFechaOpcional(By campo, int? dias)
        {
            WaitForBeingVisible(campo);
            if (dias.HasValue && dias > 0)
                InputDateInDatePicker(campo, DateTime.Today.AddDays(dias.Value));
            else
                _driver.FindElement(campo).Clear();
        }

        public void RellenarPorcentajeOpcional(int hID, int? porcentaje)
        {
            WaitForBeingClickable(By.Id($"Porcentaje_{hID}"));
            var inputElement = _driver.FindElement(By.Id($"Porcentaje_{hID}"));
            inputElement.Clear();
            if (porcentaje.HasValue)
                inputElement.SendKeys(porcentaje.Value.ToString());
        }
    }
}

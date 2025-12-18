using AppForMovies.UIT.Shared;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppForSEII2526.UIT.CU_Reparar
{
    public class CrearReparacionPO : PageObject
    {
        private By inputNombre = By.Id("Name");
        private By inputApellidos = By.Id("Surname");
        private By inputFechaEntrega = By.Id("FechaFin");
        private By repararHerramientas = By.Id("Submit");
        private By dialogOkButton = By.Id("Button_DialogOK");
        private By modificarHerramientasButton = By.Id("ModifyMovies");
        private By tableOfRentalItemsBy = By.Id("TableOfRentalItems");

        public CrearReparacionPO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {

        }

        public void RellenarFormularioReparacion(string nombreC, string apellidosC, DateTime fechaEntrega)
        {
            //wait for the webelement to be clickable
            WaitForBeingClickable(inputNombre);
            _driver.FindElement(inputNombre).SendKeys(nombreC);

            WaitForBeingClickable(inputApellidos);
            _driver.FindElement(inputApellidos).SendKeys(apellidosC);

            InputDateInDatePicker(inputFechaEntrega, fechaEntrega);
        }

        public void RellenarDescripcionReparacion(string descripcion, int hID)
        {
            _driver.FindElement(By.Id($"Descripcion_{hID}")).SendKeys(descripcion);
        }

        public bool CheckValidationError(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);
        }

        public void ClickSubmitButton()
        {
            WaitForBeingClickable(repararHerramientas);
            _driver.FindElement(repararHerramientas).Click();
        }

        public void ConfirmDialog()
        {
            WaitForBeingClickable(dialogOkButton);
            _driver.FindElement(dialogOkButton).Click();
        }

        public void RellenarCantidadReparar(int hID, int cantidad)
        {
            WaitForBeingClickable(By.Id($"Cantidad_{hID}"));
            _driver.FindElement(By.Id($"Cantidad_{hID}")).Clear();
            _driver.FindElement(By.Id($"Cantidad_{hID}")).SendKeys(cantidad.ToString());
        }

        public bool CheckGuardarReparacionDisabled()
        {
            WaitForBeingVisible(repararHerramientas);
            
            try
            {
                var button = _driver.FindElement(repararHerramientas);
                var disabledAttr = button.GetAttribute("disabled");

                // El botón está deshabilitado si tiene el atributo "disabled"
                // o si Enabled es false
                return disabledAttr != null || !button.Enabled;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void PressModifyHerramientasButton()
        {
            WaitForBeingClickable(modificarHerramientasButton);
            _driver.FindElement(modificarHerramientasButton).Click();
        }

        public bool CheckListOfHerramientasParaReparar(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, tableOfRentalItemsBy);
        }
    }
}

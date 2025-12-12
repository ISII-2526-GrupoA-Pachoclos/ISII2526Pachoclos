using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit.Abstractions;
using OpenQA.Selenium.Support.UI;

namespace AppForSEII2526.UIT.CU_Reparar
{
    public class SelectHerramientasParaRepararPO : PageObject
    {
        private By inputNombreHerramienta = By.Id("inputNombreHerramienta");
        private By inputfiltroTiempoReparacion = By.Id("inputfiltroTiempoReparacion");
        private By botonBuscarHerramientas = By.Id("BuscarHerramientas");
        private By tableOfHerramientasBy = By.Id("TableOfHerramientas");
        private By buttonRepararHerramientas = By.Id("purchaseHerramientaButton");
        
        public SelectHerramientasParaRepararPO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {

        }

        public void BuscarHerramientas(string nombreHerramienta, string tiempoReparacion)
        {
            //wait for the webelement to be clickable
            WaitForBeingClickable(inputNombreHerramienta);
            _driver.FindElement(inputNombreHerramienta).SendKeys(nombreHerramienta);

            WaitForBeingClickable(inputfiltroTiempoReparacion);
            _driver.FindElement(inputfiltroTiempoReparacion).SendKeys(tiempoReparacion);


            _driver.FindElement(botonBuscarHerramientas).Click();

        }

        public bool CheckListOfHerramientas(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, tableOfHerramientasBy);
        }

        public void AddHerramientaToCart(string nombreHerramienta)
        {
            By addButton = By.Id("herramientaForReparar_" + nombreHerramienta);
            WaitForBeingClickable(addButton);
            _driver.FindElement(addButton).Click();
        }

        public void RemoveHerramientaFromCart(string nombreHerramienta)
        {
            By removeButton = By.Id("removeHerramienta_" + nombreHerramienta);
            WaitForBeingClickable(removeButton);
            _driver.FindElement(removeButton).Click();
        }

        public bool RepararHerramientasNotAvailable()
        {
            //the button is not Displayed=hidden
            try
            {
                return _driver.FindElement(buttonRepararHerramientas).Displayed == false;
            }
            catch (Exception ex)
            {
                return true; // Si no existe, está oculto
            }
        }

        public void ClickRepararHerramientas()
        {
            WaitForBeingClickable(buttonRepararHerramientas);
            _driver.FindElement(buttonRepararHerramientas).Click();
        }
    }
}


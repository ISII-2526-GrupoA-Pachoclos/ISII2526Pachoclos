using AppForSEII2526.UIT.Shared;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Xunit.Abstractions;

namespace AppForSEII2526.UIT.CU_CrearOfertas
{
    public class SelectHerramientasParaOfertasPO : PageObject
    {
        private By inputPrecio = By.Id("inputTitle");
        private By inputFabricante = By.Id("fabricanteSelected");
        private By buttonBuscarHerramientas = By.Id("searchHerramientas");
        private By tableOfHerramientasBy = By.Id("TableOfHerramientas");
        private By botonOfertar = By.Id("ofertarHerramientaButton");

        public SelectHerramientasParaOfertasPO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {

        }

        public void BuscarHerramientas(string precio, string fabricante)
        {
            WaitForBeingClickable(inputPrecio);
            _driver.FindElement(inputPrecio).SendKeys(precio);

            if (fabricante == "") fabricante = "All";
            SelectElement selectElement = new SelectElement(_driver.FindElement(inputFabricante));
            selectElement.SelectByText(fabricante);

            _driver.FindElement(buttonBuscarHerramientas).Click();
        }

        public bool CheckListOfHerramientas(List<string[]> expectedHerramientas)
        {

            return CheckBodyTable(expectedHerramientas, tableOfHerramientasBy);
        }

        public void AddHerramientaToCarrito(string nombreHerramienta)
        {
            By addButton = By.Id("herramientaToOfertar_" + nombreHerramienta);
            WaitForBeingClickable(addButton);
            _driver.FindElement(addButton).Click();

            // Hacer scroll hacia el botón de ofertar usando Actions
            WaitForBeingVisible(botonOfertar);
            IWebElement botonOfertarElement = _driver.FindElement(botonOfertar);
            Actions actions = new Actions(_driver);
            actions.MoveToElement(botonOfertarElement).Perform();
        }

        public void RemoveHerramientaFromCarrito(string nombreHerramienta)
        {
            By removeButton = By.Id("removeHerramienta_" + nombreHerramienta);
            WaitForBeingClickable(removeButton);
            
            // Hacer scroll hacia el botón de eliminar primero
            IWebElement removeButtonElement = _driver.FindElement(removeButton);
            Actions actions = new Actions(_driver);
            actions.MoveToElement(removeButtonElement).Perform();
            Thread.Sleep(300); // Pequeña pausa para que termine el scroll
            
            try
            {
                // Intentar click normal
                removeButtonElement.Click();
            }
            catch (ElementClickInterceptedException)
            {
                // Si falla, usar JavaScript para hacer click
                IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                js.ExecuteScript("arguments[0].click();", removeButtonElement);
            }

            // Hacer scroll hacia el botón de ofertar después de eliminar
            WaitForBeingVisible(botonOfertar);
            IWebElement botonOfertarElement = _driver.FindElement(botonOfertar);
            actions.MoveToElement(botonOfertarElement).Perform();
        }

        public bool OfertarHerramientasNotAvailable()
        {
            // The button is not Displayed == hidden
            try
            {
                return _driver.FindElement(botonOfertar).Displayed == false;
            }
            catch (Exception)
            {
                return true; // Si no existe, está oculto
            }
        }

        public void ClickOfertarHerramientas()
        {
            WaitForBeingClickable(botonOfertar);
            _driver.FindElement(botonOfertar).Click();
        }
    }
}

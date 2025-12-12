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

        public bool CheckHerramientasInTable(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, tableOfHerramientasBy);
        }



        //------------------- CARRITO ---------------------------
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
            _driver.FindElement(removeButton).Click();
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

        public bool CheckListOfHerramientas(List<string[]> expectedHerramientas)
        {

            return CheckBodyTable(expectedHerramientas, tableOfHerramientasBy);
        }
    }
}

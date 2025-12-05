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

        public SelectHerramientasParaRepararPO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public void BuscarHerramientas(string nombreHerramienta, string tiempoReparacion)
        {
            //wait for the webelement to be clickable
            WaitForBeingVisible(inputNombreHerramienta);
            _driver.FindElement(inputNombreHerramienta).SendKeys(nombreHerramienta);

            WaitForBeingVisible(inputfiltroTiempoReparacion);
            _driver.FindElement(inputfiltroTiempoReparacion).SendKeys(tiempoReparacion);


            _driver.FindElement(botonBuscarHerramientas).Click();






        }

        public bool CheckListOfHerramientas(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, tableOfHerramientasBy);
        }

    }
}


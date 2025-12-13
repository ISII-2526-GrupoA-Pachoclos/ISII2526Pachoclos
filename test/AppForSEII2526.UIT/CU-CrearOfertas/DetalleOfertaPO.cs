using AppForSEII2526.UIT.Shared;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;

namespace AppForSEII2526.UIT.CU_CrearOfertas
{
    public class DetalleOfertaPO : PageObject
    {
        private By botonPrecioTotal = By.Id("PrecioTotal");

        public DetalleOfertaPO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {

        }

        public bool CheckOfertaDetail(DateTime fechaInicio, DateTime fechaFin, DateTime fechaOferta, string metodoPago, string dirigidaA, int ofertaItems, float precioTotal)
        {
            WaitForBeingVisible(botonPrecioTotal);
            bool result = true;

            result = result && _driver.FindElement(By.Id("FechaInicio")).Text.Contains(fechaInicio.ToString("dd/MM/yyyy"));
            result = result && _driver.FindElement(By.Id("FechaFin")).Text.Contains(fechaFin.ToString("dd/MM/yyyy"));
            result = result && _driver.FindElement(By.Id("FechaOferta")).Text.Contains(fechaOferta.ToString("dd/MM/yyyy"));
            result = result && _driver.FindElement(By.Id("MetodoPago")).Text.Contains(metodoPago);
            result = result && _driver.FindElement(By.Id("TiposDirigidaOferta")).Text.Contains(dirigidaA);
            result = result && _driver.FindElement(By.Id("OfertaItems")).Text.Contains(ofertaItems.ToString());
            result = result && _driver.FindElement(botonPrecioTotal).Text.Contains(precioTotal.ToString("F2"));

            return result;
        }

        public bool CheckListOfHerramientasOfertadas(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, By.Id("DetalleOferta"));
        }
    }
}

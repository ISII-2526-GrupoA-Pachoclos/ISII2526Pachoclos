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
        private By tableFechaInicio = By.Id("FechaInicio");
        private By tableFechaFin = By.Id("FechaFin");
        private By tableFechaOferta = By.Id("FechaOferta");
        private By tableMetodoPago = By.Id("MetodoPago");
        private By tableTiposDirigidaOferta = By.Id("TiposDirigidaOferta");
        private By tableOfertaItems = By.Id("OfertaItems");
        private By tableDetalleOferta = By.Id("DetalleOferta");
        private By tablePrecioTotal = By.Id("PrecioTotal");

        public DetalleOfertaPO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public string GetFechaInicio()
        {
            WaitForBeingVisible(tableFechaInicio);
            return _driver.FindElement(tableFechaInicio).Text;
        }

        public string GetFechaFin()
        {
            WaitForBeingVisible(tableFechaFin);
            return _driver.FindElement(tableFechaFin).Text;
        }

        public string GetFechaOferta()
        {
            WaitForBeingVisible(tableFechaOferta);
            return _driver.FindElement(tableFechaOferta).Text;
        }

        public string GetMetodoPago()
        {
            WaitForBeingVisible(tableMetodoPago);
            return _driver.FindElement(tableMetodoPago).Text;
        }

        public string GetTiposDirigidaOferta()
        {
            WaitForBeingVisible(tableTiposDirigidaOferta);
            return _driver.FindElement(tableTiposDirigidaOferta).Text;
        }

        public string GetCantidadOfertaItems()
        {
            WaitForBeingVisible(tableOfertaItems);
            return _driver.FindElement(tableOfertaItems).Text;
        }

        public string GetPrecioTotal()
        {
            WaitForBeingVisible(tablePrecioTotal);
            return _driver.FindElement(tablePrecioTotal).Text;
        }

        public bool CheckHerramientasOfertadasInTable(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, tableDetalleOferta);
        }
    }
}

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
    public class DetalleReparacionPO : PageObject
    {
        By botonPrecioTotal = By.Id("TotalPrice");

        public DetalleReparacionPO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public bool CheckReparacionDetail(string nombreC, string apellidosC, DateTime fechaEntrega,
            DateTime fechaRecogida, float precioTotal)
        {
            WaitForBeingVisible(botonPrecioTotal);
            bool result = true;
            var nombreApellidos = nombreC + " " + apellidosC;
            result = result && _driver.FindElement(By.Id("NameSurname")).Text.Contains(nombreApellidos);
            result = result && _driver.FindElement(By.Id("FechaEntrega")).Text.Contains(fechaEntrega.ToString("dd/MM/yyyy"));
            result = result && _driver.FindElement(By.Id("FechaRecogida")).Text.Contains(fechaRecogida.ToString("dd/MM/yyyy"));
            result = result && _driver.FindElement(By.Id("TotalPrice")).Text.Contains(precioTotal.ToString());

            return result;
        }

        public bool CheckListOfHerramientasReparadas(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, By.Id("HerramientasAReparar"));
        }
    }
}

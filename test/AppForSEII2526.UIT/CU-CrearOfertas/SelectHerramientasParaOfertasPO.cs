using OpenQA.Selenium.DevTools.V137.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_CrearOfertas
{
    public class SelectHerramientasParaOfertasPO : PageObject
    {
        private By inputPrecio = By.Id("inputTitle");
        private By inputFabricante = By.Id("fabricanteSelected");
        private By buttonBuscarHerramientas = By.Id("searchHerramientas");
        private By tableOfHerramientasBy = By.Id("TableOfHerramientas");

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
    }
}

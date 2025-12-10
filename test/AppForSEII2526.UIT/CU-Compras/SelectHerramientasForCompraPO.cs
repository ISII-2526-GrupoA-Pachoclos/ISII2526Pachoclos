using OpenQA.Selenium.DevTools.V137.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_Compras
{
    public class SelectHerramientasForCompraPO: PageObject
    {
        public SelectHerramientasForCompraPO( IWebDriver driver, ITestOutputHelper output): base(driver, output) 
        { 
            



        }
        By filtroPrecio = By.Id("filtroPrecio");
        By filtroMaterial = By.Id("filtroMaterial");
        By botonBuscar = By.Id("buscarHerramientas");
        By tablaHerramientasBy = By.Id("Tabla de Herramientas");

        public void BuscarHerramientas(string material, int percio) {
            WaitForBeingClickable(filtroMaterial);

            _driver.FindElement(filtroMaterial).SendKeys(material);

            WaitForBeingClickable(filtroPrecio);
            _driver.FindElement(filtroPrecio).SendKeys(percio.ToString());
            _driver.FindElement(botonBuscar).Click();


        }

        public bool CheckListaHerramientas(List<string[]> expectedHerramientas) { 

            return CheckBodyTable(expectedHerramientas, tablaHerramientasBy);
        }

        

    }
}

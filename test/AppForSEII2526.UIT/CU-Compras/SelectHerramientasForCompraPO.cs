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
        By botonComprar = By.Id("compraButton");

        public void BuscarHerramientas(string material, int percio) {
            WaitForBeingClickable(filtroMaterial);

            _driver.FindElement(filtroMaterial).SendKeys(material);

            WaitForBeingClickable(filtroPrecio);
            _driver.FindElement(filtroPrecio).SendKeys(percio.ToString());
            _driver.FindElement(botonBuscar).Click();


        }

        public void AñadirHerramienta(string nombreHerramienta) { 
            By addButton = By.Id("HerramientaParaComprar_" + nombreHerramienta);
            WaitForBeingClickable(addButton);
            _driver.FindElement(addButton).Click();
        }

        public void QuitarHerramienta(string nombreHerramienta)
        {
            By removeButton = By.Id("removeHerramienta_" + nombreHerramienta);
            WaitForBeingClickable(removeButton);
            _driver.FindElement(removeButton).Click();
        }

        public bool ComprarHerramientaNotAvailable()
        {
            try
            {
                return _driver.FindElement(botonComprar).Displayed == false;
            }
            catch (Exception ex) {
                return true; //si no existe está oculto
            }
        
        
        }

        public bool CheckListaHerramientas(List<string[]> expectedHerramientas) { 

            return CheckBodyTable(expectedHerramientas, tablaHerramientasBy);
        }

        

    }
}

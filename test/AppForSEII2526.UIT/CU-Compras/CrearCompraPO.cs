using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_Compras
{
    public class CrearCompraPO : PageObject
    {
        public CrearCompraPO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {

        }
        By nombreUsuario = By.Id("Nombre");
        By apellido = By.Id("Apellido");
        By direccion = By.Id("DireccionEnvio");
        By comprarHerramientas = By.Id("Submit");
        By dialogOk = By.Id("Button_DialogOK");

        public void RellenarFormularioCompra(string nombre, string apellidoUser, string direccionEnvio,  string descripcionText)
        {
            WaitForBeingClickable(nombreUsuario);
            _driver.FindElement(nombreUsuario).SendKeys(nombre);
            WaitForBeingClickable(apellido);
            _driver.FindElement(apellido).SendKeys(apellidoUser);
            WaitForBeingClickable(direccion);
            _driver.FindElement(direccion).SendKeys(direccionEnvio);
        }

        public void SubmitCompra()
        {
            WaitForBeingClickable(comprarHerramientas);
            _driver.FindElement(comprarHerramientas).Click();
        }

        public void ConfirmCompra() { 
            WaitForBeingClickable(dialogOk);
            _driver.FindElement(dialogOk).Click();


        }

        
        public bool CheckError(string expectedError) {
            
            return _driver.PageSource.Contains(expectedError);

        }
        

    }
}

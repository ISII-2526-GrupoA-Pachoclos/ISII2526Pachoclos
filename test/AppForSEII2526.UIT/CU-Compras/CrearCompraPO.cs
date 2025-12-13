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
        By modificarCompra = By.Id("modificarHerramientas");
        By tablaHerramientasBy = By.Id("TableOfRentalItems");


        public void RellenarFormularioCompra(string nombre, string apellidoUser, string direccionEnvio)
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

        public void ModificarCompra()
        {
            WaitForBeingClickable(modificarCompra);
            _driver.FindElement(modificarCompra).Click();
        }


        public bool CheckError(string expectedError) {
            
            return _driver.PageSource.Contains(expectedError);

        }

        public bool CheckListaHerramientas(List<string[]> expectedHerramientas)
        {

            return CheckBodyTable(expectedHerramientas, tablaHerramientasBy);
        }

        public void rellenarCantidad(int id, int cantidad) {
            By cantidadBy = By.Id("cantidad_" + id);
            WaitForBeingClickable(cantidadBy);
            _driver.FindElement(cantidadBy).Clear();
            _driver.FindElement(cantidadBy).SendKeys(cantidad.ToString());
        }


    }
}

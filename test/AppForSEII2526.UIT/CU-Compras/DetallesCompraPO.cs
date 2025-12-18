using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_Compras
{
    public class DetallesCompraPO: PageObject
    {
        public DetallesCompraPO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {

        }
        By tablaHerramientasBy = By.Id("RentedMovies");

        public bool CheckDetallesCompra(string nombre, string apellido, string direccion, DateTime Fecha, int precioTotal) {
            WaitForBeingVisible(tablaHerramientasBy);
            bool result = true;
            var nombreApellidos = nombre + " " + apellido;
            result = result && _driver.FindElement(By.Id("NombreApellido")).Text.Contains(nombreApellidos);
            result = result && _driver.FindElement(By.Id("DireccionEnvio")).Text.Contains(direccion);
            result = result && _driver.FindElement(By.Id("FechaCompra")).Text.Contains(Fecha.ToString("dd/MM/yyyy"));
            result = result && _driver.FindElement(By.Id("TotalPrice")).Text.Contains(precioTotal.ToString());
            return result;

        }


        public bool CheckListaHerramientas(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, tablaHerramientasBy);
        }
    }
}

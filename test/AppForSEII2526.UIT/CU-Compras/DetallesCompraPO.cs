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

        public bool CheckListaHerramientas(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, tablaHerramientasBy);
        }
    }
}

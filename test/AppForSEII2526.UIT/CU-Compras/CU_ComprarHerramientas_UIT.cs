using AppForMovies.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_Compras
{
    internal class CU_ComprarHerramientas_UIT : IDisposable
    {
        public CU_ComprarHerramientas_UIT(ITestOutputHelper output)
        {
            //it is needed to run the browser and
            //know the URI of your app
            //UC_UIT.SetUp_UIT(out _driver, out _URI);

            //it is initialized using the logger provided by xUnit
            this._output = output;
        }
        IWebDriver _driver;
        string _URI;
        private readonly ITestOutputHelper _output;

        void IDisposable.Dispose()
        {
            //To close and release all the resources allocated by the web driver
            _driver.Close();
            _driver.Dispose();
            GC.SuppressFinalize(this);

        }
    }
}
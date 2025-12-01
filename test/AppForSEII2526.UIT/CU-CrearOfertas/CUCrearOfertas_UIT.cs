using AppForMovies.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_CrearOfertas
{
    public class CUCrearOfertas_UIT :IDisposable
    {
        IWebDriver _driver;
        string _URI;
        private readonly ITestOutputHelper _output;

        public CUCrearOfertas_UIT(ITestOutputHelper output)
        {
            UC_UIT.SetUp_Chrome4UIT(out _driver, out _URI);
            this._output = output;
        }

        void IDisposable.Dispose()
        {
            _driver.Close();
            _driver.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

using AppForMovies.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_Reparar
{
    public class CU_RepararHerramientas_UIT : IDisposable
    {
        //Webdriver: A reference to the browser
        IWebDriver _driver;

        //A reference to the URI of the web page to test
        string _URI;

        //this may be used whenever some result should be printed in E
        private readonly ITestOutputHelper _output;

        public CU_RepararHerramientas_UIT(ITestOutputHelper output)
        {
            //it is needed to run the browser and
            //know the URI of your app
            UC_UIT ucuit = new UC_UIT(output);

            //it is initialized using the logger provided by xUnit
            this._output = output;
        }

        //The code for your test Methods goes here
        void IDisposable.Dispose()
        {
            //To close and release all the resources allocated by the web driver
            _driver.Close();
            _driver.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

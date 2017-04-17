using SPI_PCC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPI_PCC
{
    class Program
    {
        static void Main(string[] args)
        {
            SPIParserApp app = new SPIParserApp();
            app.initialize();
            app.runOnce();
            app.waitForShutdown();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPI_PCC.Model
{
    public enum ChartType
    {
        XBarChart = 0,
        RChart = 1
    }

    public enum Status
    {
        WithinSpec = 0,
        OutOfSpec1 = 1,
        OutOfSpec2 = 2,
        OutOfSpec3 = 4
    }
}

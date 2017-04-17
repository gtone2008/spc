using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPI_PCC.Model
{
    public class DashBoardInfo
    {
        public string Line { get; set; }
        public string Customer { get; set; }
        public string Model { get; set; }
        public List<ModelInfo> Models { get; set; }
    }
}

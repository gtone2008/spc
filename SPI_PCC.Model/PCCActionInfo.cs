using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SPI_PCC.Model
{
    public class PCCActionInfo
    {


        public string Application { get; set; }
        public string Bay { get; set; }
        public double WarningData { get; set; }
        public DateTime WarningFrom { get; set; }
        public string WarningOwner { get; set; }
        public string Proposal { get; set; }
        public string ActualAction { get; set; }
        public string WarningStatus { get; set; }
        public int ElapseMins { get; set; }
        public string WarningInfo { get; set; }
        public string ACTStatus { get; set; }
    }
}

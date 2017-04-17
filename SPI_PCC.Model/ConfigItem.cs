using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPI_PCC.Model
{
    public class ConfigItem
    {
        public string Line { get; set; }
        public string IP { get; set; }
        public string SrcFolder { get; set; }
        public string BackupFolder { get; set; }
        public string ErrorFolder { get; set; }
        public string LogFolder { get; set; }
        public string FileMask { get; set; }
    }
}

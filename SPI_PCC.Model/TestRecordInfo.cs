using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPI_PCC.Model
{
    public class TestRecordInfo
    {
        public int ID { get; set; }
        public string Customer { get; set; }
        public ModelInfo Model { get; set; }
        public string FileName { get; set; }
        public string Line { get; set; }
        public DateTime FileTime { get; set; }
        public double X { get; set; }
        public double R { get; set; }
        public Status XFlag { get; set; }
        public Status RFlag { get; set; }
    }
}

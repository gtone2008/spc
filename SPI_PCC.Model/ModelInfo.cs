using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPI_PCC.Model
{
    public class ModelInfo
    {
        public string ModelName { get; set; }
        public string BoardType { get; set; }
        public string Customer { get; set; }
        public string Line { get; set; }
        public double CL_X { get; set; }
        public double USL_X { get; set; }
        public double LSL_X { get; set; }
        public double CL_R { get; set; }
        public double UCL_X
        {
            get
            {
                return CL_X + 0.58 * CL_R;
            }
        }

        public double LCL_X
        {
            get
            {
                return CL_X - 0.58 * CL_R;
            }
        }

        public double UCL_R
        {
            get
            {
                return 2.11 * CL_R;
            }
        }

        public double LCL_R
        {
            get
            {
                return 0;
            }
        }

        public DateTime UpdateTime { get; set; }
        public bool IsNeedUpdate
        {
            get
            {
                if (UpdateTime.AddDays(30) > DateTime.Now)
                    return false;
                else
                    return true;
            }
        }

        public string[] MeasurementPoints { get; set; }
    }
}

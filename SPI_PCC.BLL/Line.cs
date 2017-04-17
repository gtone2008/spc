using SPI_PCC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPI_PCC.BLL
{
    public class Line
    {
        private string _lineName;
        private DAL.Line _dal = new DAL.Line();

        public Line(string lineName)
        {
            this._lineName = lineName;
        }

        public static List<string> List()
        {
            return DAL.Line.List();
        }
       
        public DashBoardInfo ListCurrentDataPoints()
        {
           return TestRecord.List(_lineName);
        }
    }
}

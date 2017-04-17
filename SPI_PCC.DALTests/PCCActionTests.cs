using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPI_PCC.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPI_PCC.Model;

namespace SPI_PCC.DAL.Tests
{
    [TestClass()]
    public class PCCActionTests
    {
        [TestMethod()]
        public void InsertTest()
        {
            SPI_PCC.DAL.PCCAction _pccDal = new DAL.PCCAction();

            PCCActionInfo pccActionInfo = new PCCActionInfo();
            pccActionInfo.Application = "SPI PCC";
            pccActionInfo.Bay = "Bay-08-1";
            pccActionInfo.WarningData = 0;
            pccActionInfo.WarningFrom = System.DateTime.Now;
            pccActionInfo.WarningOwner ="CX Zhou";
            pccActionInfo.WarningStatus = "Open";
            pccActionInfo.ElapseMins = 0;
            pccActionInfo.ActualAction = "Edit";
            pccActionInfo.ACTStatus = "";


            pccActionInfo.Proposal = "Check SPI X Flag";
            pccActionInfo.WarningInfo = "ROA1286240" + " / XFlag= 1000 out of spec1";
            _pccDal.Insert(pccActionInfo);

            Assert.IsTrue(_pccDal.Insert(pccActionInfo)>0);
        }
    }
}

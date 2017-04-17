using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestSPIFileParser
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
        [TestMethod]
        public void TestProcessFile()
        {


            //                    registerHandler(new SPIFileParser(item, new FileFolderAccess(item.SrcFolder)));

            SPI_PCC.Model.ConfigItem item = new SPI_PCC.Model.ConfigItem()
            {
                Line = "line77",
                IP = "123.123.123.123",
                SrcFolder = @"\\wuxfile10\it\040_TEMP\SPI",
                BackupFolder = @"\\wuxfile10\it\040_TEMP\SPI\BK",
                ErrorFolder = @"\\wuxfile10\it\040_TEMP\SPI\ERR",
                LogFolder = @"\\wuxfile10\it\040_TEMP\SPI\LOG",
                FileMask = "*.csv"
            };
            //JblFHT.Core.FileFolderAccess 

            SPI_PCC.BLL.SPIFileParser p = new SPI_PCC.BLL.SPIFileParser(item,
                new JblFHT.Core.FileFolderAccess(item.SrcFolder));

            JblFHT.Core.SimFileInfo sfi = new JblFHT.Core.SimFileInfo(
                @"\\wuxfile10\it\040_TEMP\SPI\IDNO_08700.csv",
                DateTime.Now
                );

            p.Test_processFile(sfi);
        }
    }
}

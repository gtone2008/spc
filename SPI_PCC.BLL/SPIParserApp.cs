using JblFHT.Core;
using SPI_PCC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SPI_PCC.BLL
{
    public class SPIParserApp:JblFHTApp
    {
        public override void initialize()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("config.xml");

            XmlElement root = doc.DocumentElement;
            XmlNodeList folderNodes = root.SelectNodes("/FileHandler/Source/Folder");

            foreach (XmlNode node in folderNodes)
            {
                try
                {
                    ConfigItem item = new ConfigItem
                    {
                        Line = node.Attributes["line"].Value,
                        IP = node.Attributes["IP"].Value,
                        SrcFolder = node.Attributes["src_folder"].Value,
                        BackupFolder = node.Attributes["backup_folder"].Value,
                        ErrorFolder = node.Attributes["error_folder"].Value,
                        LogFolder = node.Attributes["log_folder"].Value,
                        FileMask = node.Attributes["filemask"].Value
                    };

                    registerHandler(new SPIFileParser(item, new FileFolderAccess(item.SrcFolder)));
                }
                catch (Exception e)
                {
                }
            }
        }
    }
}

using JblFHT.Core;
using JblFHT.Core.Log;
using SPI_PCC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SPI_PCC.BLL
{
    public class SPIFileParser : FolderHandler
    {
        private ConfigItem _item;
        private TestRecordInfo _info;

        public SPIFileParser(ConfigItem item, IFolderAccess folderAccess)
            : base(item.Line, item.SrcFolder, item.FileMask, false, 0, 1)
        {
            this._item = item;
            _folder = folderAccess;
            _info = new TestRecordInfo();
            setLogger(new DailyFileLogger(item.LogFolder, "SPI", ".txt"));
        }

        public override void onProcessFolderStart()
        {
            _pendingFiles = _folder.List(this.FileMask).OrderBy(o => o.LastModifiedTime).ToList();
        }

        public void Test_processFile(SimFileInfo sfi)
        {
            processFile(sfi);
        }

        protected override void processFile(SimFileInfo sfi)
        {
            //todo:read file,get TestRecordInfo,insert db,backupfile,log
            //todo:refer template
            string str;
            int i = 0;
            int j = 0;
            string fileName;
            int pLength = 0;
            StringBuilder sb = new StringBuilder();

            fileName = sfi.Path.Substring(sfi.Path.LastIndexOf("\\") + 1);
            System.IO.StreamReader sr = new System.IO.StreamReader(sfi.Path);
            _info.Model = null;

            try
            {
                //skip the top 3 lines
                for (i = 0; i < 3; i++)
                {
                    sr.ReadLine();
                }

                //get the model information from the 4th line
                if ((str = sr.ReadLine()) == null)
                {
                    return;
                }

                string[] rawRow = str.Split(',');

                //BLL.Model m = new BLL.Model(path[path.Length - 2]);
                //modInfo = m.Get();

                double[] componentSum = null;
                int[] componentCount = null;
                double[] componentAvg = null;

                do
                {
                    rawRow = str.Split(',');

                    if (_info.Model == null)
                    {
                        string[] path = rawRow[0].Split('\\');
                        string str1 = path[path.Length - 1];
                        path = path[path.Length - 1].Split('_');

                        _info.Model = new ModelInfo();
                        _info.Customer = path[0];
                        _info.Model.BoardType = path[path.Length - 2];
                        _info.Model.ModelName = str1.Substring(_info.Customer.Length + 1, str1.Length - (_info.Customer.Length + 1) - (path[path.Length - 1].Length + 3));
                        _info.Model.Line = _item.Line;
                        _info.FileName = fileName;
                        _info.FileTime = sfi.LastModifiedTime;
                        _info.Line = _item.Line;

                        _info.Model = new Model(_info.Model).Get();
                        pLength = _info.Model.MeasurementPoints.Length;
                        componentSum = new double[pLength];
                        componentCount = new int[pLength];
                        componentAvg = new double[pLength];
                    }

                    j = 0;
                    foreach (string item in _info.Model.MeasurementPoints)
                    {
                        if (rawRow[1].ToUpper().EndsWith(item.ToUpper()))
                        {
                            break;
                        }
                        j++;
                    }

                    if (j < pLength)
                    {
                        componentSum[j] += Convert.ToDouble(rawRow[2]);
                        componentCount[j] += 1;
                    }

                } while ((!string.IsNullOrEmpty(str = sr.ReadLine())));

                sr.Close();

                //calculate the MEAN value, and R value
                double avgSum = 0;
                int invalidNum = 0;
                double maxValue = 0;
                double minValue = double.MaxValue;

                for (i = 0; i < pLength; i++)
                {
                    if (componentCount[i] > 0)
                    {
                        componentAvg[i] = componentSum[i] / componentCount[i];
                        avgSum += componentAvg[i];

                        if (maxValue < componentAvg[i])
                            maxValue = componentAvg[i];
                        if (minValue > componentAvg[i])
                            minValue = componentAvg[i];
                    }
                    else
                    {
                        invalidNum++;
                    }
                }


                if (pLength <= invalidNum)
                    throw new Exception("can not find points in file!");

                _info.X = avgSum / (pLength - invalidNum);
                _info.R = maxValue - minValue;

                _info.XFlag = SPI_PCC.Model.Status.WithinSpec;
                _info.RFlag = SPI_PCC.Model.Status.WithinSpec;

                Move2Folder(sfi.Path, Path.Combine(_item.BackupFolder, fileName));

                //todo
                TestRecord trBLL = new TestRecord(_info);
                int id = trBLL.Insert();
                log(fileName, fileName + " inserted into TestRecord,ID[" + id + "]");
                trBLL.Delete();

                trBLL.RulesCheck();
            }
            catch (Exception e)
            {
                if (sr != null)
                    sr.Close();

                Move2Folder(sfi.Path, Path.Combine(_item.ErrorFolder, fileName));
                sb.Append("exception ocurred: ");
                sb.Append(e.Message);
                log(fileName, fileName + "  " + sb.ToString());
            }
        }


        public override void onProcessFolderEnd()
        {
        }

        private void Move2Folder(string srcFile, string destFile)
        {
            try
            {
                File.Move(srcFile, destFile);
                return;
            }
            catch (Exception ex)
            {
                log(srcFile, "Move to " + destFile + " failed: " + ex.Message);
            }
            try
            {
                File.Delete(destFile);
                File.Move(srcFile, destFile);
                return;
            }
            catch (Exception e)
            {
                string fileName = srcFile.Substring(srcFile.LastIndexOf("\\") + 1);
                log(srcFile, fileName + "  " + e.Message);
            }
        }

    }
}












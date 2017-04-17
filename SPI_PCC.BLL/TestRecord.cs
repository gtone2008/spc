using SPI_PCC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPI_PCC.BLL
{
    public class TestRecord
    {
        private TestRecordInfo _info;
        private List<TestRecordInfo> _list;
        private bool _isReReCalculateCLXR;
        private SPI_PCC.DAL.TestRecord _dal = new SPI_PCC.DAL.TestRecord();
        private SPI_PCC.DAL.PCCAction _pccDal = new DAL.PCCAction();

        public TestRecord(TestRecordInfo info)
        {
            this._info = info;
            _dal.info = info;
            _list = _dal.ListPreviousPoints(29);

        }

        public int Insert()
        {
            _info.ID = _dal.Insert();

            return _info.ID;
        }

        public int Delete()
        {
            // delete the oldeest records, to keep only 50 latest records
            return _dal.Delete(50);
        }

        public void RulesCheck()
        {
            _list.Insert(0, this._info);

            bool hasReCalculatedCL = false;
            hasReCalculatedCL = ReCalculateCLXR(_list.Count);

            if (hasReCalculatedCL)
            {
                Model modelBLL = new Model(_info.Model);
                modelBLL.Update();
            }

            if (hasReCalculatedCL)
            {
                //reset the flag
                foreach (TestRecordInfo tr in _list)
                {
                    tr.XFlag = Status.WithinSpec;
                    tr.RFlag = Status.WithinSpec;
                }

                // recheck rule1 for previous N points
                foreach (TestRecordInfo tr in _list)
                {
                    if (tr.X > _info.Model.UCL_X || tr.X < _info.Model.LCL_X)
                        tr.XFlag |= Status.OutOfSpec1;

                    if (tr.R > _info.Model.UCL_R || tr.R < _info.Model.LCL_R)
                        tr.RFlag |= Status.OutOfSpec1;
                }

                // recheck rule2 for previous N points
                //Rule2_1Check();
            }
            else
            {
                //Rule1Check(_list);

                PCCActionInfo pccActionInfo = new PCCActionInfo();
                pccActionInfo.Application = System.Configuration.ConfigurationManager.AppSettings["Application"].ToString();
                pccActionInfo.Bay = _info.Line;
                pccActionInfo.WarningData = 0;
                pccActionInfo.WarningFrom = System.DateTime.Now;
                pccActionInfo.WarningOwner = System.Configuration.ConfigurationManager.AppSettings["ApplicationOwner"].ToString();
                pccActionInfo.WarningStatus = "Open";
                pccActionInfo.ElapseMins = 0;
                pccActionInfo.ActualAction = "Edit";
                pccActionInfo.ACTStatus = "";

                if (!Rule1CheckX())
                {
                    pccActionInfo.Proposal = "Check SPI X Flag";
                    pccActionInfo.WarningInfo = _info.Model.ModelName + " / XFlag=" + _info.X + " out of spec1";
                    _pccDal.Insert(pccActionInfo);
                }
                if (!Rule1CheckR())
                {
                    pccActionInfo.Proposal = "Check SPI Y Flag";
                    pccActionInfo.WarningInfo = _info.Model.ModelName + " / RFlag=" + _info.R + " out of spec1";
                    _pccDal.Insert(pccActionInfo);
                }

                if (_list.Count >= 7)
                {
                    //_list.RemoveRange(7, _list.Count - 7);

                    //Rule2Check(_list);


                    if (!Rule3CheckX(_list) && !Is8PointsRiseOrDeclineX(_list))
                    {
                        pccActionInfo.Proposal = "Check SPI X Flag";
                        pccActionInfo.WarningInfo = _info.Model.ModelName + " / X out of spec3";
                        _pccDal.Insert(pccActionInfo);
                    }

                    if (!Rule3CheckR(_list) && !Is8PointsRiseOrDeclineR(_list))
                    {
                        pccActionInfo.Proposal = "Check SPI R Flag";
                        pccActionInfo.WarningInfo = _info.Model.ModelName + " / R out of spec3";
                        _pccDal.Insert(pccActionInfo);
                    }
                }
            }

            foreach (TestRecordInfo info in _list)
            {
                _dal.info = info;
                _dal.Update();
            }
        }

        //任意连续7点在中心线一侧
        private void Rule2_1Check()
        {
            int pStart;
            int pEnd;
            int cnt;
            bool flag;
            bool flagCurrent;

            //check X
            pStart = 0;
            pEnd = 1;
            cnt = 1;

            flag = _list[pStart].X > _info.Model.CL_X;
            while (pEnd < _list.Count)
            {
                flagCurrent = _list[pEnd].X > _info.Model.CL_X;
                if (flagCurrent == flag)
                {
                    cnt++;
                    pEnd++;
                }
                else
                {
                    if (cnt >= 7)
                    {
                        for (int i = pStart; i < pEnd; i++)
                            _list[i].XFlag |= Status.OutOfSpec2;
                    }
                    pStart = pEnd;
                    flag = _list[pStart].X > _info.Model.CL_X;
                    cnt = 1;
                    pEnd++;
                }
            }
            if (cnt >= 7)
            {
                for (int i = pStart; i < pEnd; i++)
                    _list[i].XFlag |= Status.OutOfSpec2;
            }

            //check R
            pStart = 0;
            pEnd = 1;
            cnt = 1;

            flag = _list[pStart].R > _info.Model.CL_R;
            while (pEnd < _list.Count)
            {
                flagCurrent = _list[pEnd].R > _info.Model.CL_R;
                if (flagCurrent == flag)
                {
                    cnt++;
                    pEnd++;
                }
                else
                {
                    if (cnt >= 7)
                    {
                        for (int i = pStart; i < pEnd; i++)
                            _list[i].RFlag |= Status.OutOfSpec2;
                    }
                    pStart = pEnd;
                    flag = _list[pStart].R > _info.Model.CL_R;
                    cnt = 1;
                    pEnd++;
                }
            }
            if (cnt >= 7)
            {
                for (int i = pStart; i < pEnd; i++)
                    _list[i].RFlag |= Status.OutOfSpec2;
            }


        }

        public void Rule2_1Check(List<TestRecordInfo> list)
        {
            _list = list;
            Rule2_1Check();
        }

        //任意一点超出UCL或LCL
        //public void Rule1Check(List<TestRecordInfo> list)
        //{
        //    if (_info.X > _info.Model.UCL_X || _info.X < _info.Model.LCL_X)
        //    {
        //        _info.XFlag |= Status.OutOfSpec1;
        //    }

        //    if (_info.R > _info.Model.UCL_R || _info.R < _info.Model.LCL_R)
        //    {
        //        _info.RFlag |= Status.OutOfSpec1;
        //    }
        //}

        //任意一点超出UCL或LCL
        public bool Rule1CheckX()
        {
            if (_info.X > _info.Model.UCL_X || _info.X < _info.Model.LCL_X)
            {
                _info.XFlag |= Status.OutOfSpec1;
                return false;
            }
            return true;
        }
        //任意一点超出UCL或LCL
        public bool Rule1CheckR()
        {
            if (_info.R > _info.Model.UCL_R || _info.R < _info.Model.LCL_R)
            {
                _info.RFlag |= Status.OutOfSpec1;
                return false;
            }
            return true;
        }



        //连续7点在中心线一侧
        public void Rule2Check(List<TestRecordInfo> list)
        {
            double minX = list.Min(o => o.X);
            double maxX = list.Max(o => o.X);
            if (minX > _info.Model.CL_X || maxX < _info.Model.CL_X)
            {
                foreach (TestRecordInfo info in list)
                {
                    info.XFlag |= Status.OutOfSpec2;
                }
            }

            double minR = list.Min(o => o.R);
            double maxR = list.Max(o => o.R);
            if (minR > _info.Model.CL_R || maxR < _info.Model.CL_R)
            {
                foreach (TestRecordInfo info in list)
                {
                    info.RFlag |= Status.OutOfSpec2;
                }
            }
        }

        //连续7点上升或下降
        //list中所有点上升，或下降
        //public bool Rule3Check(List<TestRecordInfo> list)
        //{
        //    bool flg = true;
        //    bool flg2 = true;
        //    for (int i = 1; i < 7; i++)
        //    {
        //        if (list[i].X >= list[i - 1].X && list[i].X >= list[i + 1].X        // x[i-1]<=x[i]>=x[i+1]
        //            || list[i].X <= list[i - 1].X && list[i].X <= list[i + 1].X)     // x[i-1]>=x[i]<=x[i+1]
        //        {
        //            flg = false;
        //            break;
        //        }
        //    }

        //    if (flg)
        //    {
        //        foreach (TestRecordInfo info in list)
        //        {
        //            info.XFlag |= Status.OutOfSpec3;
        //        }
        //    }


        //    for (int i = 1; i < 7; i++)
        //    {
        //        if (list[i].R >= list[i - 1].R && list[i].R >= list[i + 1].R        // x[i-1]<=x[i]>=x[i+1]
        //            || list[i].R <= list[i - 1].R && list[i].R <= list[i + 1].R)     // x[i-1]>=x[i]<=x[i+1]
        //        {
        //            flg2 = false;
        //            break;
        //        }
        //    }

        //    if (flg2)
        //    {
        //        foreach (TestRecordInfo info in list)
        //        {
        //            info.RFlag |= Status.OutOfSpec3;
        //        }
        //    }

        //    return flg || flg2;

        //}


        public bool Rule3CheckX(List<TestRecordInfo> list)
        {
            bool flg = false;
            for (int i = 1; i < 7; i++)
            {
                if (list[i].X >= list[i - 1].X && list[i].X >= list[i + 1].X        // x[i-1]<=x[i]>=x[i+1]
                    || list[i].X <= list[i - 1].X && list[i].X <= list[i + 1].X)     // x[i-1]>=x[i]<=x[i+1]
                {
                    flg = true;
                    break;
                }
            }

            if (!flg)
            {
                foreach (TestRecordInfo info in list)
                {
                    info.XFlag |= Status.OutOfSpec3;
                }
            }

            return flg;
        }

        public bool Rule3CheckR(List<TestRecordInfo> list)
        {
            bool flg = false;
            
            for (int i = 1; i < 7; i++)
            {
                if (list[i].R >= list[i - 1].R && list[i].R >= list[i + 1].R        // x[i-1]<=x[i]>=x[i+1]
                    || list[i].R <= list[i - 1].R && list[i].R <= list[i + 1].R)     // x[i-1]>=x[i]<=x[i+1]
                {
                    flg = true;
                    break;
                }
            }

            if (!flg)
            {
                foreach (TestRecordInfo info in list)
                {
                    info.RFlag |= Status.OutOfSpec3;
                }
            }

            return flg;
        }
        //
        public bool Is8PointsRiseOrDeclineX(List<TestRecordInfo> list)
        {
            bool flg = true;
            
            for (int i = 1; i < 8; i++)
            {
                if (list[i].X >= list[i - 1].X && list[i].X >= list[i + 1].X        // x[i-1]<=x[i]>=x[i+1]
                    || list[i].X <= list[i - 1].X && list[i].X <= list[i + 1].X)     // x[i-1]>=x[i]<=x[i+1]
                {
                    flg = false;
                    return flg;
                }
            }

            return flg;
        }

        public bool Is8PointsRiseOrDeclineR(List<TestRecordInfo> list)
        {
            bool flg = true;
           
            for (int i = 1; i < 8; i++)
            {
                if (list[i].R >= list[i - 1].R && list[i].R >= list[i + 1].R        // x[i-1]<=x[i]>=x[i+1]
                    || list[i].R <= list[i - 1].R && list[i].R <= list[i + 1].R)     // x[i-1]>=x[i]<=x[i+1]
                {
                    flg = false;
                    return flg;
                }
            }

            return flg;
        }


        // 1st point, initiate the CL as its value.
        // 2nd..29th point, no change
        // 30th point, recalculate CL
        // 31st ... Nth, no change if with 30 days.
        private bool ReCalculateCLXR(int count)
        {
            _isReReCalculateCLXR = true;

            switch (count)
            {
                case 1:
                    _info.Model.CL_X = _info.X;
                    _info.Model.CL_R = _info.R;
                    _info.Model.UpdateTime = DateTime.Now.AddDays(-50);

                    _isReReCalculateCLXR = true;
                    break;

                default:
                    if (_info.Model.IsNeedUpdate == true && count >= 30)
                    {
                        _info.Model.CL_X = _list.Average(o => o.X);
                        _info.Model.CL_R = _list.Average(o => o.R);
                        _info.Model.UpdateTime = DateTime.Now;
                        _isReReCalculateCLXR = true;
                    }
                    else
                        _isReReCalculateCLXR = false;
                    break;
            }

            return _isReReCalculateCLXR;
        }

        public static DashBoardInfo List(string line)
        {
            return DAL.TestRecord.List(line);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestTestRecord
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void TestRule1Check1()
        {
            SPI_PCC.Model.TestRecordInfo trInfo = new SPI_PCC.Model.TestRecordInfo();
            trInfo.Model = new SPI_PCC.Model.ModelInfo();
            trInfo.Model.CL_X = 10.1;
            trInfo.X = 10.1 + 0.58 * 10.1;

            trInfo.Model.CL_R = 0.8;
            trInfo.R = 0.8 * 2.11 + 1;
            SPI_PCC.BLL.TestRecord tr = new SPI_PCC.BLL.TestRecord(trInfo);

            //tr.Rule1Check(null);
            //Assert.AreEqual(SPI_PCC.Model.Status.OutOfSpec1, trInfo.XFlag, "XFlag");
            //Assert.AreEqual(SPI_PCC.Model.Status.OutOfSpec1, trInfo.RFlag, "RFlag");

            /*
                 _info.XFlag=OutOfSpec1;
                 _info.RFlag=OutOfSpec1;                
            */

            trInfo.X=10;
            trInfo.R = 1;

           // tr.Rule1Check(null);

            /*
               _info.XFlag=OutOfSpec1;
               _info.RFlag=OutOfSpec1;                
          */

        }

        [TestMethod]
        public void TestRule2Check1()
        {
            SPI_PCC.Model.TestRecordInfo trInfo = new SPI_PCC.Model.TestRecordInfo();
            trInfo.Model = new SPI_PCC.Model.ModelInfo();
            trInfo.Model.CL_X = 10.1;
            trInfo.Model.CL_R = 0.8;
            SPI_PCC.BLL.TestRecord tr = new SPI_PCC.BLL.TestRecord(trInfo);

            List<SPI_PCC.Model.TestRecordInfo> l = new List<SPI_PCC.Model.TestRecordInfo>();
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 1, R = 1, XFlag = SPI_PCC.Model.Status.OutOfSpec1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 2.3, R = 1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 12.6, R = 0.8 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 1, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 7.1, R = 1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 1, R = 1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 1, R = 1 });

            tr.Rule2Check(l);

            /*
             * XFlag值不用做位运算，保持不变
             * RFlag值不用做位运算，保持不变
             * 
             * 
            */

            trInfo.Model.CL_X = 0.5;
            trInfo.Model.CL_R = 0.5;

            tr.Rule2Check(l);

            /*XFlag:
             * 3,OutOfSpec2,OutOfSpec2,OutOfSpec2,OutOfSpec2,OutOfSpec2,OutOfSpec2
             * RFlag:
             * OutOfSpec2,OutOfSpec2,OutOfSpec2,OutOfSpec2,OutOfSpec2,OutOfSpec2,OutOfSpec2
            */

            trInfo.Model.CL_X = 13;
            trInfo.Model.CL_R = 5;

            tr.Rule2Check(l);

            /*XFlag:
            * 3,OutOfSpec2,OutOfSpec2,OutOfSpec2,OutOfSpec2,OutOfSpec2,OutOfSpec2
            * RFlag:
            * OutOfSpec2,OutOfSpec2,OutOfSpec2,OutOfSpec2,OutOfSpec2,OutOfSpec2,OutOfSpec2
            */

        }

        [TestMethod]
        public void TestRule3Check1()
        {
            SPI_PCC.Model.TestRecordInfo trInfo = new SPI_PCC.Model.TestRecordInfo();
            trInfo.Model = new SPI_PCC.Model.ModelInfo();
            trInfo.Model.CL_X = 10.1;
            trInfo.Model.CL_R = 0.8;
            SPI_PCC.BLL.TestRecord tr = new SPI_PCC.BLL.TestRecord(trInfo);

            List<SPI_PCC.Model.TestRecordInfo> l = new List<SPI_PCC.Model.TestRecordInfo>();
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 1, R = 1, XFlag = SPI_PCC.Model.Status.OutOfSpec1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 2.3, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 3.6, R = 1.3 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 4, R = 1.31 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 5.1, R = 1.4 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 6, R = 1.41 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 6.1, R = 1.44 });

            //tr.Rule3Check(l);

            /*XFlag:
            * 5,OutOfSpec3,OutOfSpec3,OutOfSpec3,OutOfSpec3,OutOfSpec3,OutOfSpec3
            * RFlag:
            * OutOfSpec3,OutOfSpec3,OutOfSpec3,OutOfSpec3,OutOfSpec3,OutOfSpec3,OutOfSpec3
            */

            List<SPI_PCC.Model.TestRecordInfo> la = new List<SPI_PCC.Model.TestRecordInfo>();
            la.Add(new SPI_PCC.Model.TestRecordInfo { X = 1, R = 1, XFlag = SPI_PCC.Model.Status.OutOfSpec1 });
            la.Add(new SPI_PCC.Model.TestRecordInfo { X = 1, R = 1 });
            la.Add(new SPI_PCC.Model.TestRecordInfo { X = 1, R = 1 });
            la.Add(new SPI_PCC.Model.TestRecordInfo { X = 4, R = 1.31 });
            la.Add(new SPI_PCC.Model.TestRecordInfo { X = 5.1, R = 1.4 });
            la.Add(new SPI_PCC.Model.TestRecordInfo { X = 6, R = 1.41 });
            la.Add(new SPI_PCC.Model.TestRecordInfo { X = 6.1, R = 1.44 });

            //tr.Rule3Check(la);

            /*XFlag:
            * 保持不变。没有执行赋值操作
            * RFlag:
            * 保持不变。没有执行赋值操作
            */


        }


        [TestMethod]
        public void TestBllTestRecordReCalculateCLXR()
        {
            SPI_PCC.Model.TestRecordInfo trInfo = new SPI_PCC.Model.TestRecordInfo();
            trInfo.Model = new SPI_PCC.Model.ModelInfo();
            trInfo.X = 11.7;
            trInfo.R = 1.8;
            trInfo.Line = "line88";
            trInfo.FileTime = Convert.ToDateTime("2017-04-10 10:27");
            trInfo.Model.ModelName = "M123";
            trInfo.Model.BoardType = "T";

            trInfo.Model.UpdateTime = Convert.ToDateTime("2017-02-10 10:27");

            SPI_PCC.BLL.TestRecord tr = new SPI_PCC.BLL.TestRecord(trInfo);

            //tr.ReCalculateCLXR(1);

            /*返回值:
            * true
            * 
            */

            //tr.ReCalculateCLXR(30);

            /*返回值:
              * true
              * 
            */

            trInfo.Model.UpdateTime = Convert.ToDateTime("2017-02-10 10:27");

            tr = new SPI_PCC.BLL.TestRecord(trInfo);

            //tr.ReCalculateCLXR(20);

            /*返回值:
              * false
              * 
            */

        }

        [TestMethod]
        public void TestBllTestRecordRulesCheck()
        {
            SPI_PCC.Model.TestRecordInfo trInfo = new SPI_PCC.Model.TestRecordInfo();
            trInfo.Model = new SPI_PCC.Model.ModelInfo();
            trInfo.X = 11.7;
            trInfo.R = 9.8;
            trInfo.Line = "line88";
            trInfo.FileTime = Convert.ToDateTime("2017-04-10 10:27");
            trInfo.Model.ModelName = "M123";
            trInfo.Model.BoardType = "T";
            trInfo.Model.Line = "line88";

            trInfo.Model.CL_X = 2;
            trInfo.Model.CL_R = 1;

            trInfo.Model.UpdateTime = Convert.ToDateTime("2017-02-10 10:27");

            SPI_PCC.BLL.TestRecord tr = new SPI_PCC.BLL.TestRecord(trInfo);

            tr.RulesCheck();

            /*
             * 这个函数里面循环有点问题，请确认下！
             * 还有条记录，没有插入，进行修改是不对的！
             * 
             * 
             * 
            */

            trInfo.FileTime = Convert.ToDateTime("2017-04-10 10:21:00");
            tr = new SPI_PCC.BLL.TestRecord(trInfo);
            tr.RulesCheck();

            /*
            * 以上测试TestRecord数小于30
            * 
            * 
            * 
            * 
           */
        }
        
        [TestMethod]
        public void TestRule2_1Check1()
        {
            SPI_PCC.Model.TestRecordInfo trInfo = new SPI_PCC.Model.TestRecordInfo();
            trInfo.Model = new SPI_PCC.Model.ModelInfo();
            trInfo.Model.CL_X = 10.1;
            trInfo.Model.CL_R = 0.8;
            SPI_PCC.BLL.TestRecord tr = new SPI_PCC.BLL.TestRecord(trInfo);

            List<SPI_PCC.Model.TestRecordInfo> l = new List<SPI_PCC.Model.TestRecordInfo>();
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 9.6, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 8.1, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 7.4, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 7.3, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 10.1, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 9.5, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 7.8, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 7.7, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 11.5, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 7.5, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 11.2, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 7.8, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 7.9, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 10.9, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 7.6, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 9.3, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 11, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 11, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 10.5, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 10.3, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 12.3, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 10.2, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 10.3, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 8.5, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 7.8, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 8.4, R = 1.1 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 9.1, R = 1.1 });

            //新改测试用例：
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 9.9, R = 0.5 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 8.9, R = 0.6 });
            l.Add(new SPI_PCC.Model.TestRecordInfo { X = 8.3, R = 0.3 });

            tr.Rule2_1Check(l);
            foreach (SPI_PCC.Model.TestRecordInfo inf in l)
                System.Diagnostics.Debug.WriteLine(inf.XFlag);

        }

        [TestMethod]
        public void TestDALTestRecordDelete()
        {
            /*
             *  insert into TestRecord(CustomerCode,ModelName,BoardType,FileName,FileTime,Line,X,R,XFlag,RFlag)values ('NE','M123','T','ABC.Csv','2017-04-10 10:18','line88',10,3.5,0,0);
                insert into TestRecord(CustomerCode,ModelName,BoardType,FileName,FileTime,Line,X,R,XFlag,RFlag)values ('NE','M123','T','ABC.Csv','2017-04-10 10:19','line88',10,3.5,0,0);
                insert into TestRecord(CustomerCode,ModelName,BoardType,FileName,FileTime,Line,X,R,XFlag,RFlag)values ('NE','M123','T','ABC.Csv','2017-04-10 10:21','line88',10,3.5,0,0);
                insert into TestRecord(CustomerCode,ModelName,BoardType,FileName,FileTime,Line,X,R,XFlag,RFlag)values ('NE','M123','T','ABC.Csv','2017-04-10 10:22','line88',10,3.5,0,0);
                insert into TestRecord(CustomerCode,ModelName,BoardType,FileName,FileTime,Line,X,R,XFlag,RFlag)values ('NE','M123','T','ABC.Csv','2017-04-10 10:23','line88',10,3.5,0,0);
                insert into TestRecord(CustomerCode,ModelName,BoardType,FileName,FileTime,Line,X,R,XFlag,RFlag)values ('NE','M123','T','ABC.Csv','2017-04-10 10:24','line88',10,3.5,0,0);
                insert into TestRecord(CustomerCode,ModelName,BoardType,FileName,FileTime,Line,X,R,XFlag,RFlag)values ('NE','M123','T','ABC.Csv','2017-04-10 10:25','line88',10,3.5,0,0);
                insert into TestRecord(CustomerCode,ModelName,BoardType,FileName,FileTime,Line,X,R,XFlag,RFlag)values ('NE','M123','T','ABC.Csv','2017-04-10 10:26','line88',10,3.5,0,0);
                insert into TestRecord(CustomerCode,ModelName,BoardType,FileName,FileTime,Line,X,R,XFlag,RFlag)values ('NE','M123','T','ABC.Csv','2017-04-10 10:27','line88',10,3.5,0,0);
                insert into TestRecord(CustomerCode,ModelName,BoardType,FileName,FileTime,Line,X,R,XFlag,RFlag)values ('NE','M123','T','ABC.Csv','2017-04-10 10:28','line88',10,3.5,0,0);
                insert into TestRecord(CustomerCode,ModelName,BoardType,FileName,FileTime,Line,X,R,XFlag,RFlag)values ('NE','M123','T','ABC.Csv','2017-04-10 10:29','line88',10,3.5,0,0);
 
                mysql>  select * from TestRecord where Line = 'line88';
                +----+--------------+-----------+----------+---------------------+--------+------+------+-------+-------+-----------+
                | ID | CustomerCode | ModelName | FileName | FileTime            | Line   | X    | R    | XFlag | RFlag | BoardType |
                +----+--------------+-----------+----------+---------------------+--------+------+------+-------+-------+-----------+
                | 14 | NE           | M123      | ABC.Csv  | 2017-04-10 10:18:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
                | 15 | NE           | M123      | ABC.Csv  | 2017-04-10 10:19:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
                | 16 | NE           | M123      | ABC.Csv  | 2017-04-10 10:21:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
                | 17 | NE           | M123      | ABC.Csv  | 2017-04-10 10:22:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
                | 18 | NE           | M123      | ABC.Csv  | 2017-04-10 10:23:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
                | 19 | NE           | M123      | ABC.Csv  | 2017-04-10 10:24:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
                | 20 | NE           | M123      | ABC.Csv  | 2017-04-10 10:25:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
                | 21 | NE           | M123      | ABC.Csv  | 2017-04-10 10:26:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
                | 22 | NE           | M123      | ABC.Csv  | 2017-04-10 10:27:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
                | 23 | NE           | M123      | ABC.Csv  | 2017-04-10 10:28:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
                | 24 | NE           | M123      | ABC.Csv  | 2017-04-10 10:29:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
                +----+--------------+-----------+----------+---------------------+--------+------+------+-------+-------+-----------+
                11 rows in set (0.00 sec)
                 
             */

            SPI_PCC.Model.TestRecordInfo trInfo = new SPI_PCC.Model.TestRecordInfo();
            trInfo.Model = new SPI_PCC.Model.ModelInfo();
            trInfo.Line = "line88";


            SPI_PCC.DAL.TestRecord tr = new SPI_PCC.DAL.TestRecord();
            tr.info = trInfo;

            tr.Delete(20);
            tr.Delete(11);
            tr.Delete(10);  //!!!error
            tr.Delete(9);   //!!!error

            //            delete 20:
            //mysql>  select * from TestRecord where Line = 'line88';
            //+----+--------------+-----------+----------+---------------------+--------+------+------+-------+-------+-----------+
            //| ID | CustomerCode | ModelName | FileName | FileTime            | Line   | X    | R    | XFlag | RFlag | BoardType |
            //+----+--------------+-----------+----------+---------------------+--------+------+------+-------+-------+-----------+
            //| 14 | NE           | M123      | ABC.Csv  | 2017-04-10 10:18:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 15 | NE           | M123      | ABC.Csv  | 2017-04-10 10:19:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 16 | NE           | M123      | ABC.Csv  | 2017-04-10 10:21:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 17 | NE           | M123      | ABC.Csv  | 2017-04-10 10:22:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 18 | NE           | M123      | ABC.Csv  | 2017-04-10 10:23:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 19 | NE           | M123      | ABC.Csv  | 2017-04-10 10:24:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 20 | NE           | M123      | ABC.Csv  | 2017-04-10 10:25:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 21 | NE           | M123      | ABC.Csv  | 2017-04-10 10:26:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 22 | NE           | M123      | ABC.Csv  | 2017-04-10 10:27:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 23 | NE           | M123      | ABC.Csv  | 2017-04-10 10:28:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 24 | NE           | M123      | ABC.Csv  | 2017-04-10 10:29:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //+----+--------------+-----------+----------+---------------------+--------+------+------+-------+-------+-----------+
            //11 rows in set (0.01 sec)


            //delete 11:
            //mysql>  select * from TestRecord where Line = 'line88';
            //+----+--------------+-----------+----------+---------------------+--------+------+------+-------+-------+-----------+
            //| ID | CustomerCode | ModelName | FileName | FileTime            | Line   | X    | R    | XFlag | RFlag | BoardType |
            //+----+--------------+-----------+----------+---------------------+--------+------+------+-------+-------+-----------+
            //| 14 | NE           | M123      | ABC.Csv  | 2017-04-10 10:18:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 15 | NE           | M123      | ABC.Csv  | 2017-04-10 10:19:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 16 | NE           | M123      | ABC.Csv  | 2017-04-10 10:21:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 17 | NE           | M123      | ABC.Csv  | 2017-04-10 10:22:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 18 | NE           | M123      | ABC.Csv  | 2017-04-10 10:23:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 19 | NE           | M123      | ABC.Csv  | 2017-04-10 10:24:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 20 | NE           | M123      | ABC.Csv  | 2017-04-10 10:25:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 21 | NE           | M123      | ABC.Csv  | 2017-04-10 10:26:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 22 | NE           | M123      | ABC.Csv  | 2017-04-10 10:27:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 23 | NE           | M123      | ABC.Csv  | 2017-04-10 10:28:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 24 | NE           | M123      | ABC.Csv  | 2017-04-10 10:29:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //+----+--------------+-----------+----------+---------------------+--------+------+------+-------+-------+-----------+
            //11 rows in set (0.00 sec)

            //delete 10:   !!! 没有删除掉
            //mysql>  select * from TestRecord where Line = 'line88';
            //+----+--------------+-----------+----------+---------------------+--------+------+------+-------+-------+-----------+
            //| ID | CustomerCode | ModelName | FileName | FileTime            | Line   | X    | R    | XFlag | RFlag | BoardType |
            //+----+--------------+-----------+----------+---------------------+--------+------+------+-------+-------+-----------+
            //| 14 | NE           | M123      | ABC.Csv  | 2017-04-10 10:18:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 15 | NE           | M123      | ABC.Csv  | 2017-04-10 10:19:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 16 | NE           | M123      | ABC.Csv  | 2017-04-10 10:21:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 17 | NE           | M123      | ABC.Csv  | 2017-04-10 10:22:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 18 | NE           | M123      | ABC.Csv  | 2017-04-10 10:23:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 19 | NE           | M123      | ABC.Csv  | 2017-04-10 10:24:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 20 | NE           | M123      | ABC.Csv  | 2017-04-10 10:25:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 21 | NE           | M123      | ABC.Csv  | 2017-04-10 10:26:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 22 | NE           | M123      | ABC.Csv  | 2017-04-10 10:27:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 23 | NE           | M123      | ABC.Csv  | 2017-04-10 10:28:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 24 | NE           | M123      | ABC.Csv  | 2017-04-10 10:29:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //+----+--------------+-----------+----------+---------------------+--------+------+------+-------+-------+-----------+
            //11 rows in set (0.00 sec)

            //delete 9:   ！！！没有删除掉
            //mysql>  select * from TestRecord where Line = 'line88';
            //+----+--------------+-----------+----------+---------------------+--------+------+------+-------+-------+-----------+
            //| ID | CustomerCode | ModelName | FileName | FileTime            | Line   | X    | R    | XFlag | RFlag | BoardType |
            //+----+--------------+-----------+----------+---------------------+--------+------+------+-------+-------+-----------+
            //| 14 | NE           | M123      | ABC.Csv  | 2017-04-10 10:18:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 15 | NE           | M123      | ABC.Csv  | 2017-04-10 10:19:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 16 | NE           | M123      | ABC.Csv  | 2017-04-10 10:21:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 17 | NE           | M123      | ABC.Csv  | 2017-04-10 10:22:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 18 | NE           | M123      | ABC.Csv  | 2017-04-10 10:23:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 19 | NE           | M123      | ABC.Csv  | 2017-04-10 10:24:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 20 | NE           | M123      | ABC.Csv  | 2017-04-10 10:25:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 21 | NE           | M123      | ABC.Csv  | 2017-04-10 10:26:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 22 | NE           | M123      | ABC.Csv  | 2017-04-10 10:27:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 23 | NE           | M123      | ABC.Csv  | 2017-04-10 10:28:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //| 24 | NE           | M123      | ABC.Csv  | 2017-04-10 10:29:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
            //+----+--------------+-----------+----------+---------------------+--------+------+------+-------+-------+-----------+
            //11 rows in set (0.00 sec)



        }

        [TestMethod]
        public void TestDALTestRecordListPreviousPoints()
        {

            /*
             * 
             * mysql> select * from TestRecord where Line = 'line88';
                +----+--------------+-----------+----------+---------------------+--------+------+------+-------+-------+-----------+
                | ID | CustomerCode | ModelName | FileName | FileTime            | Line   | X    | R    | XFlag | RFlag | BoardType |
                +----+--------------+-----------+----------+---------------------+--------+------+------+-------+-------+-----------+
                | 14 | NE           | M123      | ABC.Csv  | 2017-04-10 10:18:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
                | 15 | NE           | M123      | ABC.Csv  | 2017-04-10 10:19:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
                | 16 | NE           | M123      | ABC.Csv  | 2017-04-10 10:21:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
                | 17 | NE           | M123      | ABC.Csv  | 2017-04-10 10:22:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
                | 18 | NE           | M123      | ABC.Csv  | 2017-04-10 10:23:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
                | 19 | NE           | M123      | ABC.Csv  | 2017-04-10 10:24:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
                | 20 | NE           | M123      | ABC.Csv  | 2017-04-10 10:25:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
                | 21 | NE           | M123      | ABC.Csv  | 2017-04-10 10:26:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
                | 22 | NE           | M123      | ABC.Csv  | 2017-04-10 10:27:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
                | 23 | NE           | M123      | ABC.Csv  | 2017-04-10 10:28:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
                | 24 | NE           | M123      | ABC.Csv  | 2017-04-10 10:29:00 | line88 |   10 |  3.5 |     0 |     0 | T         |
                +----+--------------+-----------+----------+---------------------+--------+------+------+-------+-------+-----------+
                11 rows in set (0.00 sec)
             * 
             * 
             * 
             * 
            */

            SPI_PCC.Model.TestRecordInfo trInfo = new SPI_PCC.Model.TestRecordInfo();
            trInfo.Model = new SPI_PCC.Model.ModelInfo();
            trInfo.Line = "line88";
            trInfo.FileTime = Convert.ToDateTime("2017-04-10 10:27");
            trInfo.Model.ModelName = "M123";
            trInfo.Model.BoardType = "T";

            SPI_PCC.DAL.TestRecord tr = new SPI_PCC.DAL.TestRecord();
            tr.info = trInfo;

            List<SPI_PCC.Model.TestRecordInfo> l = tr.ListPreviousPoints(5);

         

        }



    }
}



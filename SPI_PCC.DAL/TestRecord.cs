using MySql.Data.MySqlClient;
using SPI_PCC.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPI_PCC.DAL
{
    public class TestRecord
    {
        private static readonly string _connString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

        public TestRecordInfo info { get; set; }

        public int Insert()
        {
            string cmdText = @"insert into TestRecord(CustomerCode,ModelName,BoardType,FileName,FileTime,Line,X,R,XFlag,RFlag)
                                values (@CustomerCode,@ModelName,@BoardType,@FileName,@FileTime,@Line,
                                        @X,@R,@XFlag,@RFlag); select @@identity;";

            MySqlParameter[] paras = new MySqlParameter[]{
                new MySqlParameter("@CustomerCode",info.Customer),
                new MySqlParameter("@ModelName",info.Model.ModelName),
                new MySqlParameter("@BoardType",info.Model.BoardType),
                new MySqlParameter("@FileName",info.FileName),
                new MySqlParameter("@FileTime",info.FileTime),
                new MySqlParameter("@Line",info.Line),
                new MySqlParameter("@X",info.X),
                new MySqlParameter("@R",info.R),
                new MySqlParameter("@XFlag",(int)info.XFlag),
                new MySqlParameter("@RFlag",(int)info.RFlag)
            };

            using (MySqlConnection conn = new MySqlConnection(_connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                cmd.Parameters.AddRange(paras);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int Delete(int keepPoint)
        {
            //todo:delete point before last N point
           
            using (MySqlConnection conn = new MySqlConnection(_connString))
            {
                string cmdText;
                MySqlParameter[] paras;

                conn.Open();
                
                // Get the last N point's time.
                cmdText = @"select FileTime from TestRecord
                                where Line=@Line
                                order by FileTime desc limit @keepPoint,1";
                paras = new MySqlParameter[]{
                        new MySqlParameter("@keepPoint",keepPoint-1),
                        new MySqlParameter("@Line",info.Line)
                    };

                MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                cmd.Parameters.AddRange(paras);

                Object o = cmd.ExecuteScalar();
                if (o == null)
                    return 0;       // if the total points less than N, then return.

                DateTime lastNthTime = Convert.ToDateTime(o);

                cmdText = @"delete from TestRecord 
                                where FileTime<@FileTime
                                and Line=@Line";
                paras = new MySqlParameter[]{
                    new MySqlParameter("@FileTime",lastNthTime),
                    new MySqlParameter("@Line",info.Line)
                };
                cmd = new MySqlCommand(cmdText, conn);
                cmd.Parameters.AddRange(paras);
                return cmd.ExecuteNonQuery();

            }
        }

        public int Update()
        {
            //todo:update flag by rules`
            string cmdText = @"update TestRecord 
                                set XFlag=@XFlag,RFlag=@RFlag
                                where ID=@ID";

            MySqlParameter[] paras = new MySqlParameter[]{
                new MySqlParameter("@XFlag",(int)info.XFlag),
                new MySqlParameter("@RFlag",(int)info.RFlag),
                new MySqlParameter("@ID", info.ID)
            };

            using (MySqlConnection conn = new MySqlConnection(_connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                cmd.Parameters.AddRange(paras);
                return cmd.ExecuteNonQuery();
            }
        }

        public List<TestRecordInfo> ListPreviousPoints(int n)
        {
            List<TestRecordInfo> list = new List<TestRecordInfo>();
            string cmdText = @"select * from TestRecord 
                                where FileTime < @FileTime and Line = @Line 
                                and ModelName = @Model and BoardType = @BoardType
                                order by FileTime desc
                                limit @n";
            MySqlParameter[] paras = new MySqlParameter[]{
                new MySqlParameter("@FileTime",info.FileTime),
                new MySqlParameter("@Line",info.Line),
                new MySqlParameter("@Model", info.Model.ModelName),
                new MySqlParameter("@BoardType",info.Model.BoardType),
                new MySqlParameter("@n",n)
            };

            using (MySqlConnection conn = new MySqlConnection(_connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                cmd.Parameters.AddRange(paras);
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        TestRecordInfo tr = new TestRecordInfo
                        {
                            ID = Convert.ToInt32(dr["ID"]),
                            Customer = dr["CustomerCode"].ToString(),
                            Model = new ModelInfo
                            {
                                ModelName = dr["ModelName"].ToString(),
                                BoardType = dr["BoardType"].ToString()
                            },
                            FileName = dr["FileName"].ToString(),
                            FileTime = Convert.ToDateTime(dr["FileTime"]),
                            Line = dr["Line"].ToString(),
                            X = Convert.ToDouble(dr["X"]),
                            R = Convert.ToDouble(dr["R"]),
                            XFlag = (Status)dr["XFlag"],
                            RFlag = (Status)dr["RFlag"]
                        };

                        list.Add(tr);
                    }
                }
            }

            return list;
        }

        public static DashBoardInfo List(string line)
        {
            //todo:
            throw new NotImplementedException();
        }
    }
}

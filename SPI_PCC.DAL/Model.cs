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
    public class Model
    {
        private static readonly string _connString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

        public ModelInfo Info { get; set; }

        public ModelInfo Get()
        {
            ModelInfo model = null;
            string cmdText = @"select m.ModelName,m.BoardType,m1.CL_X,m.USL_X,m.LSL_X,m1.CL_R,m1.UpdateTime,m.Customer,
                                 m.MeasurementPoints as Point from model m 
                                 inner join modelmeasurement m1 on m.ModelName = m1.ModelName and m.BoardType=m1.BoardType
                                 where m.ModelName=@ModelName and m.BoardType=@BoardType and m1.Line=@Line";

            MySqlParameter[] paras = new MySqlParameter[]{
                new MySqlParameter("@ModelName",Info.ModelName),
                new MySqlParameter("@BoardType",Info.BoardType),
                new MySqlParameter("@Line",Info.Line)
            };

            using (MySqlConnection conn = new MySqlConnection(_connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                cmd.Parameters.AddRange(paras);
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        model = new ModelInfo();
                        model.ModelName = Info.ModelName;
                        model.BoardType = Info.BoardType;
                        model.Line = Info.Line;
                        model.Customer = dr["Customer"].ToString();
                        model.CL_X = dr["CL_X"] != DBNull.Value ? Convert.ToDouble(dr["CL_X"]) : 0;
                        model.USL_X = Convert.ToDouble(dr["USL_X"]);
                        model.LSL_X = Convert.ToDouble(dr["LSL_X"]);
                        model.CL_R = dr["CL_R"] != DBNull.Value ? Convert.ToDouble(dr["CL_R"]) : 0;
                        model.UpdateTime = dr["UpdateTime"] != DBNull.Value ? Convert.ToDateTime(dr["UpdateTime"]) : DateTime.Now.AddDays(-50);
                        model.MeasurementPoints = dr["Point"].ToString().Split(',');
                    }
                }
            }

            return model;
        }

        public int Update()
        {
            //todo:
            string cmdText = @"update ModelMeasurement 
                                set CL_X=@CL_X,CL_R=@CL_R,UpdateTime=@UpdateTime
                                where  ModelName=@ModelName and BoardType=@BoardType and Line=@Line ";

            MySqlParameter[] paras = new MySqlParameter[]{
                new MySqlParameter("@ModelName",Info.ModelName),
                new MySqlParameter("@BoardType",Info.BoardType),
                new MySqlParameter("@Line",Info.Line),
                new MySqlParameter("@CL_X",Info.CL_X),
                new MySqlParameter("@CL_R",Info.CL_R),
                new MySqlParameter("@UpdateTime",Info.UpdateTime),
            };

            using (MySqlConnection conn = new MySqlConnection(_connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                cmd.Parameters.AddRange(paras);
                return cmd.ExecuteNonQuery();
            }

        }

        public int Insert()
        {
            //todo:

            string cmdText = @"insert into ModelMeasurement(ModelName,BoardType,Line)
                                values (@ModelName,@BoardType,@Line)";

            MySqlParameter[] paras = new MySqlParameter[]{
                new MySqlParameter("@ModelName",Info.ModelName),
                new MySqlParameter("@BoardType",Info.BoardType),
                new MySqlParameter("@Line",Info.Line)
               
            };

            using (MySqlConnection conn = new MySqlConnection(_connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                cmd.Parameters.AddRange(paras);
                return cmd.ExecuteNonQuery();
            }

        }

        public bool IsExists()
        {
            string cmdText = @"select count(*) from model
                                where ModelName=@ModelName and BoardType=@BoardType";

            MySqlParameter[] paras = new MySqlParameter[]{
                new MySqlParameter("@ModelName",Info.ModelName),
                new MySqlParameter("@BoardType",Info.BoardType)
            };

            using (MySqlConnection conn = new MySqlConnection(_connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                cmd.Parameters.AddRange(paras);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0 ? true : false;
            }
        }
    }
}







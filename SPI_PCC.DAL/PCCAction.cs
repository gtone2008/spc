using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPI_PCC.Model;

namespace SPI_PCC.DAL
{
    public class PCCAction
    {
        private static readonly string _connString = ConfigurationManager.ConnectionStrings["MySQLDBConnectionString"].ConnectionString;
        //private static readonly string _connString = "server=wuxsg01;database=mesystem;user id=root;password=Jabil12345;CharacterSet=gb2312;";
        public int Insert(PCCActionInfo info)
        {
            string cmdText = @"insert into actiontrack(Application,Bay,WarningData,WarningFrom,WarningOwner,Proposal,ActualAction,WarningStatus,ElapseMins,WarningInfo,ACTStatus)
                                values (@Application,@Bay,@WarningData,@WarningFrom,@WarningOwner,@Proposal,@ActualAction,@WarningStatus,@ElapseMins,@WarningInfo,@ACTStatus); select @@identity;";

            MySqlParameter[] paras = new MySqlParameter[]{
                new MySqlParameter("@Application",info.Application),
                new MySqlParameter("@Bay",info.Bay),
                new MySqlParameter("@WarningData",info.WarningData),
                new MySqlParameter("@WarningFrom",info.WarningFrom),
                new MySqlParameter("@WarningOwner",info.WarningOwner),
                new MySqlParameter("@Proposal",info.Proposal),
                new MySqlParameter("@ActualAction",info.ActualAction),
                new MySqlParameter("@WarningStatus",info.WarningStatus),
                new MySqlParameter("@ElapseMins",info.ElapseMins),
                new MySqlParameter("@WarningInfo",info.WarningInfo),
                new MySqlParameter("@ACTStatus",info.ACTStatus)
            };

            using (MySqlConnection conn = new MySqlConnection(_connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                cmd.Parameters.AddRange(paras);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}

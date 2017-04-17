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
    public class Line
    {
        private static readonly string _connString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

        public static List<string> List()
        {
            //todo:
            List<string> list = new List<string>();
            string cmdText = @"select LineName from Line 
                             where Active=1
                             order by LineName desc";
            
            using (MySqlConnection conn = new MySqlConnection(_connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(dr["LineName"].ToString());
                    }
                }
            }

            return list;
        }
    }
}

<%@ WebHandler Language="C#" Class="GetDataHandler" %>

using System;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
public class GetDataHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string Conn = ConfigurationManager.AppSettings["MySQLconn"];
        string sqlStr1 = @" select t.ModelName,Customer,t.Line,truncate(X,3) as X,truncate(R,3) R,truncate(XFlag,3) as XFlag,
						truncate(RFlag,3) as RFlag,truncate(CL_X,3) as CL_X,truncate(CL_R,3)CL_R,truncate(USL_X,3)USL_X,
						truncate(LSL_X,3)LSL_X,(truncate(CL_X,3)+ 0.58 * truncate(CL_R,3))UCL_X,
						(truncate(CL_X,3) - 0.58 * truncate(CL_R,3)) as LCL_X,(2.11 * truncate(CL_R,3))UCL_R
						from testrecord t left join modelmeasurement m on t.Line=m.Line and t.ModelName=m.ModelName and t.BoardType=m.BoardType
						left join model on t.ModelName=model.ModelName and t.BoardType=model.BoardType 
						where t.FileTime>now()-interval 4 hour 
						and t.ModelName=(select ModelName from testrecord where Line=t.Line order by FileTime desc limit 1 )
						and t.BoardType=(select BoardType from testrecord where Line=t.Line order by FileTime desc limit 1 )
						order by t.FileTime;";
		string _jsonTable = Util.JsonHelper.ToJson(MySqlHelper.ExecuteDataset(Conn, sqlStr1).Tables[0]);
        context.Response.Write(_jsonTable);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
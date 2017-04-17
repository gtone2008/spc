<%@ WebHandler Language="C#" Class="ConfigHandler" %>

using System;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
public class ConfigHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string Conn = ConfigurationManager.AppSettings["MySQLconn"];
        string _Action = context.Request.QueryString["Action"];
        switch (_Action)
        {
            case "getModel":
                string sqlStr = "select * from model";
                string jsonTable = Util.JsonHelper.ToJson(MySqlHelper.ExecuteDataset(Conn, sqlStr).Tables[0]);
                context.Response.Write(jsonTable);
                break;

        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
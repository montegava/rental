using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace DAL
{
    public static class ConnectionManager
    {
        public static string ConnectionStringSQLite
        {
            get
            {
                return "server=" + ConfigurationManager.AppSettings["ServerAdress"] + ";User Id=root;password=1qazxsw2;Persist Security Info=True;database=rental";
            }
        }


        public static string ConnectionStringEntity
        {
            get
            {
                return "metadata=res://*/rental.csdl|res://*/rental.ssdl|res://*/rental.msl;provider=MySql.Data.MySqlClient;provider connection string=\"server=" + ConfigurationManager.AppSettings["ServerAdress"] + ";User Id=root;password=1qazxsw2;Persist Security Info=True;database=rental\"";
            }
        }
    }
}

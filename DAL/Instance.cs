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
                return "server=" + "mysql78.1gb.ru" + ";User Id=gb_x_amiravrn;password=0063c98esg;Persist Security Info=True;database=gb_x_amiravrn";
            }
        }


        public static string ConnectionStringEntity
        {
            get
            {

//                return "metadata=res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl;provider=MySql.Data.MySqlClient;provider connection string=\"server=mysql78.1gb.ru;user id=gb_x_amiravrn;password=0063c98esg;Persist Security Info=True;database=gb_x_amiravrn\"";
              return "metadata=res://*/rental.csdl|res://*/rental.ssdl|res://*/rental.msl;provider=MySql.Data.MySqlClient;provider connection string=\"server=localhost;user id=root;password=1qazxsw2;persist security info=True;database=rental\"";
            }
        }
    }
}

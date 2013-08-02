using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    public static class BlackListManager
    {

        public static DataTable GetAllBlackListsAsDataTable()
        {
            DataTable dt = null;
            using (MySqlConnection sqlConn = new MySqlConnection(ConnectionManager.ConnectionStringSQLite))
            {
                string query = "SELECT * FROM  black_list ORDER BY id";
                sqlConn.Open();
                using (MySqlCommand command = new MySqlCommand(query, sqlConn))
                {
                    DataSet ds = new DataSet();
                    MySqlDataAdapter ret = new MySqlDataAdapter();
                    ret.SelectCommand = command;
                    ret.Fill(ds);

                    if (ds != null && ds.Tables.Count > 0)
                        dt = ds.Tables[0];
                }
                sqlConn.Close();
            }
            return dt;
        }

        public static List<DAL.black_list> GetAllBlackLists()
        {
            var result = new List<DAL.black_list>();
            DataTable dt = GetAllBlackListsAsDataTable();
            if (dt != null && dt.Rows.Count > 0)
            {
                result = (from r in dt.AsEnumerable()
                          select new DAL.black_list()
                          {
                              ID = r.Field<int>("Id"),
                              TYPE_ID = r.Field<double>("type_id"),
                              STOP = r.Field<string>("stop")
                          }).ToList<DAL.black_list>();

            }
            return result;
        }


        public static DataTable GetBlackWordById(int blackId)
        {
            DataTable dt = null;
            using (MySqlConnection sqlConn = new MySqlConnection(ConnectionManager.ConnectionStringSQLite))
            {
                string query = String.Format(@"SELECT * FROM black_list WHERE id ={0}", blackId); ;
                sqlConn.Open();
                using (MySqlCommand command = new MySqlCommand(query, sqlConn))
                {
                    DataSet ds = new DataSet();
                    MySqlDataAdapter ret = new MySqlDataAdapter();
                    ret.SelectCommand = command;
                    ret.Fill(ds);

                    if (ds != null && ds.Tables.Count > 0)
                        dt = ds.Tables[0];
                }
                sqlConn.Close();
            }
            return dt;

        }

        public static bool DeleteBlackWord(int blackId, out string error)
        {
            error = String.Empty;
            try
            {
                using (MySqlConnection sqlConn = new MySqlConnection(ConnectionManager.ConnectionStringSQLite))
                {
                    string query = @"delete from where black_list Id=@blackId";
                    sqlConn.Open();
                    using (MySqlCommand command = new MySqlCommand(query, sqlConn))
                    {
                        command.Parameters.Add(new MySqlParameter() { ParameterName = "@blackId", DbType = DbType.Int32, Value = blackId });
                        command.ExecuteNonQuery();
                    }
                    sqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
            return true;
        }

        public static bool AddNewBlakItem(DAL.black_list item, out string error)
        {
            error = String.Empty;
            try
            {
                using (MySqlConnection sqlConn = new MySqlConnection(ConnectionManager.ConnectionStringSQLite))
                {
                    string query = String.Format(@"insert into black_list ( STOP  ,COMMENT  ,TYPE_ID) values('{0}', '{2}', {1})", item.STOP, item.TYPE_ID, item.COMMENT);
                    sqlConn.Open();
                    using (MySqlCommand command = new MySqlCommand(query, sqlConn))
                    {
                        command.ExecuteNonQuery();
                    }
                    sqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
            return true;

        }
    }
}

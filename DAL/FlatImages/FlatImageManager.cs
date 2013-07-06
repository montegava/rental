using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;


namespace DAL
{
    public static class FlatImageManager
    {
        public static List<DAL.images> GetFlatImagesByFlatId(int flatId)
        {

            using (MySqlConnection sqlConn = new MySqlConnection(ConnectionManager.ConnectionStringSQLite))
            {
                String query = "select * from IMAGES i where i.Flat_Id = @flatId";
                sqlConn.Open();
                using (MySqlCommand command = new MySqlCommand(query, sqlConn))
                {
                    command.Parameters.Add(new MySqlParameter() { ParameterName = "@flatId", DbType = DbType.Int32, Value = flatId });
                    DataSet ds = new DataSet();
                    MySqlDataAdapter ret = new MySqlDataAdapter();
                    ret.SelectCommand = command;
                    ret.Fill(ds);

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        return (from rows in ds.Tables[0].AsEnumerable()
                                select new DAL.images()
                                {
                                    ID = (int)rows["Id"],
                                    FLAT_ID = (int)rows["Flat_Id"],
                                    IMAGE_PATH = rows["Image_Path"].ToString()
                                }).ToList();
                    }
                }
                sqlConn.Close();
            }
            return new List<DAL.images>();

        }

        public static int AddFlatImage(Int32 flatId, string imagePath)
        {
            using (MySqlConnection sqlConn = new MySqlConnection(ConnectionManager.ConnectionStringSQLite))
            {
                string query = "insert into IMAGES(flat_id, image_path) values (@flatId, @imagePath)";
                sqlConn.Open();
                using (MySqlCommand command = new MySqlCommand(query, sqlConn))
                {
                    command.Parameters.Add(new MySqlParameter() { ParameterName = "@flatId", DbType = DbType.Int32, Value = flatId });
                    command.Parameters.Add(new MySqlParameter() { ParameterName = "@imagePath", DbType = DbType.AnsiString, Value = System.IO.Path.GetFileName(imagePath) });
                    return command.ExecuteNonQuery();
                }
				 sqlConn.Close();
            }
            return -1;
        }

        public static int DeleteFlatImage(int imageId)
        {
            using (MySqlConnection sqlConn = new MySqlConnection(ConnectionManager.ConnectionStringSQLite))
            {
                string query = @"delete from IMAGES where Id=@imageId";
                sqlConn.Open();
                using (MySqlCommand command = new MySqlCommand(query, sqlConn))
                {
                    command.Parameters.Add(new MySqlParameter() { ParameterName = "@imageId", DbType = DbType.Int32, Value = imageId });
                    return command.ExecuteNonQuery();
                }
				 sqlConn.Close();
            }
            return -1;
        }

        public static int DeleteAllByFlatId(int flatId)
        {
            if (flatId > 0)
                using (MySqlConnection sqlConn = new MySqlConnection(ConnectionManager.ConnectionStringSQLite))
                {
                    string query = @"delete from IMAGES where Flat_Id=@flatId";
                    sqlConn.Open();
                    using (MySqlCommand command = new MySqlCommand(query, sqlConn))
                    {
                        command.Parameters.Add(new MySqlParameter() { ParameterName = "@flatId", DbType = DbType.Int32, Value = flatId });
                        return command.ExecuteNonQuery();
                    }
					 sqlConn.Close();
                }
            return -1;
        }


      

    }
}

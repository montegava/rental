using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;


namespace DAL
{
    public static class FlatManager
    {
        public static List<flat_info> GetAllFlats()
        {
            using (MySqlConnection sqlConn = new MySqlConnection(ConnectionManager.ConnectionStringSQLite))
            {
                String query = "select * from FLAT_INFO";
                sqlConn.Open();
                using (MySqlCommand command = new MySqlCommand(query, sqlConn))
                {
                    DataSet ds = new DataSet();
                    MySqlDataAdapter ret = new MySqlDataAdapter();
                    ret.SelectCommand = command;
                    ret.Fill(ds);

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                        return (from rows in ds.Tables[0].AsEnumerable()
                                select new flat_info()
                                {
                                    ID = (int)rows["Id"],
                                }).ToList();
                }
                sqlConn.Close();
            }

            return new List<flat_info>();
        }

        /// <summary>
        /// Check if advert already added
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool CheckUrlIfExist(string url, out flat_info flat)
        {
            using (MySqlConnection sqlConn = new MySqlConnection(ConnectionManager.ConnectionStringSQLite))
            {
                String query = String.Format(@"select *
                                from flat_info f
                                where f.LINK = '{0}'", url);
                sqlConn.Open();
                using (MySqlCommand command = new MySqlCommand(query, sqlConn))
                {
                    //command.Parameters.Add(new MySqlParameter() { ParameterName = "@url", DbType = DbType.AnsiString, Value = url });
                    DataSet ds = new DataSet();
                    MySqlDataAdapter ret = new MySqlDataAdapter();
                    ret.SelectCommand = command;
                    ret.Fill(ds);

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {

                        flat = (from rows in ds.Tables[0].AsEnumerable()
                                select new flat_info()
                                {
                                    ID = (int)rows["id"],
                                    DATA = rows.Field<DateTime>("data"),
                                    ROOM_COUNT = rows["room_count"].ToString(),
                                    ADDRESS = rows["address"].ToString(),
                                    FLOOR = rows["floor"].ToString(),
                                    BATH_UNIT = rows["bath_unit"].ToString(),
                                    BUILD = rows["build"].ToString(),
                                    FURNITURE = rows["furniture"].ToString(),
                                    STATE = rows["state"].ToString(),
                                    MECHANIC = rows["mechanic"].ToString(),
                                    NAME = rows["name"].ToString(),
                                    PRICE = rows["price"].ToString(),
                                    PHONE = rows["phone"].ToString(),
                                    COMMENT = rows["comment"].ToString(),
                                    CONTENT = rows["content"].ToString(),
                                    LINK = rows["link"].ToString(),
                                    RENT_FROM = rows.Field<DateTime>("rent_from"),
                                    RENT_TO = rows.Field<DateTime>("rent_to"),
                                    TERM = rows["term"].ToString(),
                                    LESSOR = rows["lessor"].ToString(),

                                    FRIDGE = rows["fridge"] == DBNull.Value ? false : (bool)rows["fridge"],
                                    TV = rows["tv"] == DBNull.Value ? false : (bool)rows["tv"],
                                    WASHER = rows["washer"] == DBNull.Value ? false : (bool)rows["washer"],
                                    COOLER = rows["cooler"] == DBNull.Value ? false : (bool)rows["cooler"],

                                    REGION = rows["region"].ToString(),
                                }).FirstOrDefault<flat_info>();
                        return true;
                    }
                }
                sqlConn.Close();
            }
            flat = null;
            return false;
        }

        public static bool AddNewFlat(flat_info flat, out string error)
        {
            error = String.Empty;
            try
            {
                using (var context = new rentalEntities(ConnectionManager.ConnectionStringEntity))
                {
                    context.flat_info.AddObject(flat);
                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }

            return true;

        }

        public static flat_info GetFlatById(int flatId, out string error)
        {
            flat_info result = null;
            error = String.Empty;

            try
            {
                if (flatId > 0)
                    using (var context = new rentalEntities(ConnectionManager.ConnectionStringEntity))
                    {
                        result = context.flat_info.Where(f => f.ID == flatId).FirstOrDefault();
                    }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return result;
        }

        public static bool UpdateFlat(flat_info flat, out string error)
        {
            error = String.Empty;

            try
            {

                using (var context = new rentalEntities(ConnectionManager.ConnectionStringEntity))
                {
                    var findedFlat = context.flat_info.Where(f => f.ID == flat.ID).FirstOrDefault();
                    if (findedFlat != null)
                    {
                        findedFlat.ADDRESS = flat.ADDRESS;
                        findedFlat.BATH_UNIT = flat.BATH_UNIT;
                        findedFlat.BUILD = flat.BUILD;
                        findedFlat.COMMENT = flat.COMMENT;
                        findedFlat.CONTENT = flat.CONTENT;
                        findedFlat.COOLER = flat.COOLER;
                        findedFlat.DATA = flat.DATA;
                        findedFlat.FLOOR = flat.FLOOR;
                        findedFlat.FRIDGE = flat.FRIDGE;
                        findedFlat.FURNITURE = flat.FURNITURE;

                        findedFlat.LESSOR = flat.LESSOR;
                        findedFlat.LINK = flat.LINK;
                        findedFlat.MECHANIC = flat.MECHANIC;
                        findedFlat.NAME = flat.NAME;
                        findedFlat.PHONE = flat.PHONE;
                        findedFlat.PRICE = flat.PRICE;
                        findedFlat.REGION = flat.REGION;
                        findedFlat.RENT_FROM = flat.RENT_FROM;
                        findedFlat.RENT_TO = flat.RENT_TO;
                        findedFlat.ROOM_COUNT = flat.ROOM_COUNT;
                        findedFlat.STATE = flat.STATE;
                        findedFlat.TERM = flat.TERM;
                        findedFlat.TV = flat.TV;
                        findedFlat.WASHER = flat.WASHER;
                        context.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }

            return true;

        }

        public static bool DeleteFlat(int flatId, out string error)
        {
            error = String.Empty;
            try
            {
                using (MySqlConnection sqlConn = new MySqlConnection(ConnectionManager.ConnectionStringSQLite))
                {
                    string query = @"delete from FLAT_INFO where Id=@flatId";
                    sqlConn.Open();
                    using (MySqlCommand command = new MySqlCommand(query, sqlConn))
                    {
                        command.Parameters.Add(new MySqlParameter() { ParameterName = "@flatId", DbType = DbType.Int32, Value = flatId });
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

        public static DataTable GetAllFlatsAsDataTable()
        {
            DataTable dt = null;
            using (MySqlConnection sqlConn = new MySqlConnection(ConnectionManager.ConnectionStringSQLite))
            {
                string query = "select * from FLAT_INFO order by id desc";
                sqlConn.Open();
                using (MySqlCommand command = new MySqlCommand(query, sqlConn))
                {
                    command.CommandTimeout = 30000;
                    IDataReader rdr = command.ExecuteReader();
                    dt = GetDataTableFromDataReader(rdr);

                    //DataSet ds = new DataSet();
                    //MySqlDataAdapter ret = new MySqlDataAdapter();
                    //ret.SelectCommand = command;
                    //ret.Fill(ds);

                    //if (ds != null && ds.Tables.Count > 0)
                    //    dt = ds.Tables[0];
                }
                sqlConn.Close();
            }
            return dt;
        }


        public static DataTable GetDataTableFromDataReader(IDataReader dataReader)
        {
            DataTable schemaTable = dataReader.GetSchemaTable();
            DataTable resultTable = new DataTable();

            foreach (DataRow dataRow in schemaTable.Rows)
            {
                DataColumn dataColumn = new DataColumn();
                dataColumn.ColumnName = dataRow["ColumnName"].ToString();
                dataColumn.DataType = Type.GetType(dataRow["DataType"].ToString());
                dataColumn.ReadOnly = (bool)dataRow["IsReadOnly"];
                dataColumn.AutoIncrement = (bool)dataRow["IsAutoIncrement"];
                dataColumn.Unique = (bool)dataRow["IsUnique"];

                resultTable.Columns.Add(dataColumn);
            }

            while (dataReader.Read())
            {
                DataRow dataRow = resultTable.NewRow();
                for (int i = 0; i < resultTable.Columns.Count; i++)
                {
                    dataRow[i] = dataReader[i];
                }
                resultTable.Rows.Add(dataRow);
            }

            return resultTable;
        }
    }
}

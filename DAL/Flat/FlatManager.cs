using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using System.Linq.Expressions;
using LinqKit;
using log4net;

namespace DAL
{

    public enum Fiels : int
    {
        NONE = 0,
        ID = 1,
        DATA = 2,
        ROOM_COUNT = 4,
        ADDRESS = 8
    }

    public static class FlatManager
    {
        public static ILog errorLog = LogManager.GetLogger("ErrorLogger");

        public static void FlatList(string filterValue, Int32 filterBy, DateTime startDate, DateTime endDate, Int32 sortBy, bool orderBy, ref Int32 activePage, Int32 pageSize, out List<flat_info> flats, out Int32 pageCount, out Int32 totalRowsNumber)
        {

            flats = null;
            pageCount = 1;
            totalRowsNumber = 0;
            try
            {

                IQueryable<flat_info> query = WcfOperationContext.Current.Context.flat_info;
                #region filterby
                if (!filterValue.Equals(string.Empty))
                {
                    Expression<Func<flat_info, bool>> filter = t => false;
                    if ((filterBy & (int)Fiels.ADDRESS) > 0)
                    {
                        filter = filter.Or(t => t.ADDRESS.ToLower().Contains(filterValue.ToLower()));
                    }
                    if ((filterBy & (int)Fiels.ROOM_COUNT) > 0)
                    {
                        filter = filter.Or(t => t.ROOM_COUNT.ToLower().Contains(filterValue.ToLower()));
                    }
                    query = query.Where(filter.Expand());
                }


                if (!startDate.Equals(DateTime.MinValue))
                    query = query.Where(t => t.DATA >= startDate);

                if (!endDate.Equals(DateTime.MinValue))
                {
                    endDate = endDate.AddDays(1);
                    query = query.Where(t => t.DATA < endDate);
                }


                #endregion

                #region sortby
                //select sort column

                switch ((Fiels)sortBy)
                {
                    case Fiels.ID:
                        query = query.SetOrder(t => t.ID, orderBy);
                        break;
                    case Fiels.DATA:
                        query = query.SetOrder(t => t.DATA, orderBy);
                        break;
                    case Fiels.ROOM_COUNT:
                        query = query.SetOrder(t => t.ROOM_COUNT, orderBy);
                        break;
                    case Fiels.ADDRESS:
                        query = query.SetOrder(t => t.ADDRESS, orderBy);
                        break;
                    default:
                        query = query.SetOrder(t => t.ID, orderBy);
                        break;
                }


                #endregion

                flats = query.GetPage(pageSize, ref activePage, ref pageCount, ref totalRowsNumber)
                            .AsEnumerable().Select(t => t).ToList();

                errorLog.Debug(flats.Count());

            }
            catch (Exception ex)
            {
                errorLog.Error(ex);
            }
        }

        public static List<flat_info> GetAllFlats()
        {
            var context = WcfOperationContext.Current.Context;
            return context.flat_info.Select(f => f).ToList();
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

        public static bool AddNewFlat(flat_info flat)
        {
            try
            {
                var context = WcfOperationContext.Current.Context;
                context.flat_info.Add(flat);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                errorLog.Error(ex);
                return false;
            }
            return true;
        }

        public static flat_info GetFlatById(int flatId)
        {
            flat_info result = null;
            try
            {
                if (flatId > 0)
                    result = WcfOperationContext.Current.Context.flat_info.Where(f => f.ID == flatId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                errorLog.Error(ex);
            }
            return result;
        }

        public static bool UpdateFlat(flat_info flat, out string error)
        {
            error = String.Empty;

            try
            {
                //ConnectionManager.ConnectionStringEntity)
                using (var context = new rentalEntities())
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
                string query = "select * from flat_info order by id desc";
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using System.Linq.Expressions;
using LinqKit;
using log4net;
using RentalCommon;

namespace DAL
{

    public static class FlatManager
    {
        public static readonly ILog errorLog = LogManager.GetLogger("TestApplication");

        public static flat_info FlatByUrl(string url)
        {
            flat_info result = null;
            if (!string.IsNullOrEmpty(url))
            {
                var context = WcfOperationContext.Current.Context;
                result = context.flat_info.Where(f => f.LINK.ToUpper() == url.ToUpper()).FirstOrDefault();
            }
            return result;
        }

        public static void FlatList(List<Filter> filterBy, DateTime startDate, DateTime endDate, Int32 sortBy, bool orderBy, ref Int32 activePage, Int32 pageSize, out List<flat_info> flats, out Int32 pageCount, out Int32 totalRowsNumber)
        {

            flats = null;
            pageCount = 1;
            totalRowsNumber = 0;
            try
            {

                IQueryable<flat_info> query = WcfOperationContext.Current.Context.flat_info;
                #region filterby

                foreach (var f in filterBy)
                {
                    Expression<Func<flat_info, bool>> filter = t => true;
                    if (!string.IsNullOrEmpty(f.StartValue))
                    {
                        switch (f.Field)
                        {
                            case Fiels.ID:
                                int id = int.Parse(f.StartValue);
                                filter = filter.And(t => t.ID == id);
                                break;

                            case Fiels.ROOM_COUNT:
                                int roomCount = int.Parse(f.StartValue);
                                if (f.Comparator == ComapreType.NONE)
                                    filter = filter.And(t => t.ROOM_COUNT == roomCount);
                                if (f.Comparator == ComapreType.MORE)
                                    filter = filter.And(t => (t.ROOM_COUNT) > roomCount);
                                if (f.Comparator == ComapreType.LESS)
                                    filter = filter.And(t => (t.ROOM_COUNT) < roomCount);
                                if (f.Comparator == ComapreType.BETWEEN)
                                {
                                    int roomCountTo = int.Parse(f.EndValue);
                                    filter = filter.And(t => (t.ROOM_COUNT) >= roomCount && (t.ROOM_COUNT) <= roomCountTo);
                                }
                                break;

                            case Fiels.ADDRESS:
                                filter = filter.And(t => t.ADDRESS.Contains(f.StartValue.ToLower()));
                                break;

                            case Fiels.FLOOR:

                                int floor = int.Parse(f.StartValue);
                                if (f.Comparator == ComapreType.NONE)
                                    filter = filter.And(t => t.FLOOR == floor);
                                if (f.Comparator == ComapreType.MORE)
                                    filter = filter.And(t => t.FLOOR > floor);
                                if (f.Comparator == ComapreType.LESS)
                                    filter = filter.And(t => t.FLOOR < floor);
                                if (f.Comparator == ComapreType.BETWEEN)
                                {
                                    int floorTo = int.Parse(f.EndValue);
                                    filter = filter.And(t => t.FLOOR >= floor && t.FLOOR <= floorTo);
                                }
                                break;

                            case Fiels.BATH_UNIT:
                                filter = filter.And(t => t.BATH_UNIT.Contains(f.StartValue.ToLower()));
                                break;

                            case Fiels.BUILD:
                                filter = filter.And(t => t.BUILD.Contains(f.StartValue.ToLower()));
                                break;

                            case Fiels.FURNITURE:
                                filter = filter.And(t => t.FURNITURE.Contains(f.StartValue));
                                break;

                            case Fiels.STATE:
                                filter = filter.And(t => t.STATE.Contains(f.StartValue.ToLower()));
                                break;

                            case Fiels.MECHANIC:
                                filter = filter.And(t => t.MECHANIC.Contains(f.StartValue.ToLower()));
                                break;

                            case Fiels.NAME:
                                filter = filter.And(t => t.NAME.Contains(f.StartValue.ToLower()));
                                break;

                            case Fiels.PRICE:
                                filter = filter.And(t => t.PRICE.Contains(f.StartValue.ToLower()));

                                break;

                            case Fiels.PHONE:
                                filter = filter.And(t => t.PHONE.Contains(f.StartValue.ToLower()));
                                break;

                            case Fiels.COMMENT:
                                filter = filter.And(t => t.COMMENT.Contains(f.StartValue.ToLower()));

                                break;
                            case Fiels.CONTENT:
                                filter = filter.And(t => t.CONTENT.Contains(f.StartValue.ToLower()));

                                break;
                            case Fiels.LINK:
                                filter = filter.And(t => t.LINK.Contains(f.StartValue.ToLower()));

                                break;

                            case Fiels.TERM:
                                filter = filter.And(t => t.TERM.Contains(f.StartValue.ToLower()));

                                break;
                            case Fiels.LESSOR:
                                filter = filter.And(t => t.LESSOR.Contains(f.StartValue.ToLower()));

                                break;

                            case Fiels.REGION:
                                filter = filter.And(t => t.REGION.Contains(f.StartValue));

                                break;
                        }



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

                switch ((Fiels)sortBy)
                {
                    case Fiels.DATA:
                        query = query.SetOrder(t => t.DATA, orderBy);
                        break;
                    case Fiels.ROOM_COUNT:
                        query = query.SetOrder(t => t.ROOM_COUNT, orderBy);
                        break;
                    case Fiels.ADDRESS:
                        query = query.SetOrder(t => t.ADDRESS, orderBy);
                        break;
                    case Fiels.FLOOR:
                        query = query.SetOrder(t => t.FLOOR, orderBy);
                        break;
                    case Fiels.BATH_UNIT:
                        query = query.SetOrder(t => t.BATH_UNIT, orderBy);
                        break;
                    case Fiels.BUILD:
                        query = query.SetOrder(t => t.BUILD, orderBy);
                        break;
                    case Fiels.FURNITURE:
                        query = query.SetOrder(t => t.FURNITURE, orderBy);
                        break;
                    case Fiels.STATE:
                        query = query.SetOrder(t => t.STATE, orderBy);
                        break;
                    case Fiels.MECHANIC:
                        query = query.SetOrder(t => t.MECHANIC, orderBy);
                        break;
                    case Fiels.NAME:
                        query = query.SetOrder(t => t.NAME, orderBy);
                        break;
                    case Fiels.PRICE:
                        query = query.SetOrder(t => t.PRICE, orderBy);
                        break;
                    case Fiels.PHONE:
                        query = query.SetOrder(t => t.PHONE, orderBy);
                        break;
                    case Fiels.COMMENT:
                        query = query.SetOrder(t => t.COMMENT, orderBy);
                        break;
                    case Fiels.CONTENT:
                        query = query.SetOrder(t => t.CONTENT, orderBy);
                        break;
                    case Fiels.LINK:
                        query = query.SetOrder(t => t.LINK, orderBy);
                        break;
                    case Fiels.TERM:
                        query = query.SetOrder(t => t.TERM, orderBy);
                        break;
                    case Fiels.RENT_FROM:
                        query = query.SetOrder(t => t.RENT_FROM, orderBy);
                        break;
                    case Fiels.RENT_TO:
                        query = query.SetOrder(t => t.RENT_TO, orderBy);
                        break;
                    case Fiels.LESSOR:
                        query = query.SetOrder(t => t.LESSOR, orderBy);
                        break;
                    case Fiels.FRIDGE:
                        query = query.SetOrder(t => t.FRIDGE, orderBy);
                        break;
                    case Fiels.TV:
                        query = query.SetOrder(t => t.TV, orderBy);
                        break;
                    case Fiels.WASHER:
                        query = query.SetOrder(t => t.WASHER, orderBy);
                        break;
                    case Fiels.COOLER:
                        query = query.SetOrder(t => t.COOLER, orderBy);
                        break;
                    case Fiels.REGION:
                        query = query.SetOrder(t => t.REGION, orderBy);
                        break;
                    default:
                        query = query.SetOrder(t => t.ID, orderBy);
                        break;
                }


                #endregion

                 flats = query.GetPage(pageSize, ref activePage, ref pageCount, ref totalRowsNumber).AsEnumerable().ToList();
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

        public static int FlatAdd(flat_info flat)
        {
            var context = WcfOperationContext.Current.Context;
            context.flat_info.Add(flat);
            context.SaveChanges();
            return flat.ID;
        }

        public static flat_info GetFlatById(int flatId)
        {
            return WcfOperationContext.Current.Context.flat_info.Where(f => f.ID == flatId).FirstOrDefault();
        }

        public static void FlatUpdate(flat_info flat)
        {
            var context = WcfOperationContext.Current.Context;
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

                findedFlat.TYPE = flat.TYPE;
                findedFlat.CATEGORY = flat.CATEGORY;
                findedFlat.EMAIL = flat.EMAIL;

                context.SaveChanges();
            }

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
            errorLog.Error("GetAllFlatsAsDataTable");

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
            errorLog.Error("GetDataTableFromDataReader");

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

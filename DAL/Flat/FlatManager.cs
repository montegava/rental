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

        public static SearchResult<view_flat_info> FlatSearch(SearchQuery query)
        {
            var context = WcfOperationContext.Current.Context;
            IQueryable<view_flat_info> result = context.view_flat_info;
            if (query.Filters != null)
            {
                Expression<Func<view_flat_info, bool>> expr = u => true;
                #region set
                foreach (var filter in query.Filters)
                {


                    switch (filter.Field)
                    {
                        #region Fields.ID
                        case Fields.ID:
                            int? value = filter.Value == null ? null : (int?)Convert.ToInt32(filter.Value);
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                    expr = expr.And(c => c.ID > value);
                                    break;
                                case FilterConditions.LESS:
                                    expr = expr.And(c => c.ID < value);
                                    break;
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.ID == value);
                                    break;
                                case FilterConditions.CONTAIN:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                            }
                            break;
                        #endregion

                        #region Fields.DATA
                        case Fields.DATA:
                            var dvalue = Convert.ToDateTime(filter.Value);
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                    expr = expr.And(c => c.DATA > dvalue);
                                    break;
                                case FilterConditions.LESS:
                                    expr = expr.And(c => c.DATA < dvalue);
                                    break;
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.DATA == dvalue);
                                    break;
                                case FilterConditions.CONTAIN:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                            }
                            break;
                        #endregion

                        #region Fields.ROOM_COUNT
                        case Fields.ROOM_COUNT:
                            value = filter.Value == null ? null : (int?)Convert.ToInt32(filter.Value);
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                    expr = expr.And(c => c.ROOM_COUNT > value);
                                    break;
                                case FilterConditions.LESS:
                                    expr = expr.And(c => c.ROOM_COUNT < value);
                                    break;
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.ROOM_COUNT == value);
                                    break;
                                case FilterConditions.CONTAIN:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                            }
                            break;
                        #endregion

                        #region Fields.ADDRESS
                        case Fields.ADDRESS:
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                case FilterConditions.LESS:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.ADDRESS.Equals((string)filter.Value, StringComparison.InvariantCultureIgnoreCase));
                                    break;
                                case FilterConditions.CONTAIN:
                                    expr = expr.And(c => c.ADDRESS.ToUpper().Contains(((string)filter.Value).ToUpper()));
                                    break;
                            }
                            break;
                        #endregion

                        #region Fields.FLOOR
                        case Fields.FLOOR:
                            value = filter.Value == null ? null : (int?)Convert.ToInt32(filter.Value);
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                    expr = expr.And(c => c.FLOOR > value);
                                    break;
                                case FilterConditions.LESS:
                                    expr = expr.And(c => c.FLOOR < value);
                                    break;
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.FLOOR == value);
                                    break;
                                case FilterConditions.CONTAIN:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                            }
                            break;
                        #endregion

                        #region Fields.BATH_UNIT
                        case Fields.BATH_UNIT:
                            value = filter.Value == null ? null : (int?)Convert.ToInt32(filter.Value);
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                case FilterConditions.LESS:
                                case FilterConditions.CONTAIN:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.bathunit_type_id == value);
                                    break;
                            }
                            break;
                        #endregion

                        #region Fields.BUILD
                        case Fields.BUILD:
                            value = filter.Value == null ? null : (int?)Convert.ToInt32(filter.Value);
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                case FilterConditions.LESS:
                                case FilterConditions.CONTAIN:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.buld_type_id == value);
                                    break;
                            }
                            break;
                        #endregion

                        #region Fields.FURNITURE
                        case Fields.FURNITURE:
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                case FilterConditions.LESS:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.FURNITURE.Equals((string)filter.Value, StringComparison.InvariantCultureIgnoreCase));
                                    break;
                                case FilterConditions.CONTAIN:
                                    expr = expr.And(c => c.FURNITURE.ToUpper().Contains(((string)filter.Value).ToUpper()));
                                    break;
                            }
                            break;
                        #endregion

                        #region Fields.STATE
                        case Fields.STATE:
                            value = filter.Value == null ? null : (int?)Convert.ToInt32(filter.Value);
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                case FilterConditions.LESS:
                                case FilterConditions.CONTAIN:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.state_type_id== value);
                                    break;
                            }
                            break;
                        #endregion

                        #region Fields.NAME
                        case Fields.NAME:
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                case FilterConditions.LESS:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.NAME.Equals((string)filter.Value, StringComparison.InvariantCultureIgnoreCase));
                                    break;
                                case FilterConditions.CONTAIN:
                                    expr = expr.And(c => c.NAME.ToUpper().Contains(((string)filter.Value).ToUpper()));
                                    break;
                            }
                            break;
                        #endregion

                        #region Fields.PRICE
                        case Fields.PRICE:
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                case FilterConditions.LESS:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.PRICE.Equals((string)filter.Value, StringComparison.InvariantCultureIgnoreCase));
                                    break;
                                case FilterConditions.CONTAIN:
                                    expr = expr.And(c => c.PRICE.ToUpper().Contains(((string)filter.Value).ToUpper()));
                                    break;
                            }
                            break;
                        #endregion

                        #region Fields.PHONE
                        case Fields.PHONE:
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                case FilterConditions.LESS:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.PHONE.Equals((string)filter.Value, StringComparison.InvariantCultureIgnoreCase));
                                    break;
                                case FilterConditions.CONTAIN:
                                    expr = expr.And(c => c.PHONE.ToUpper().Contains(((string)filter.Value).ToUpper()));
                                    break;
                            }
                            break;
                        #endregion

                        #region Fields.COMMENT
                        case Fields.COMMENT:
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                case FilterConditions.LESS:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.COMMENT.Equals((string)filter.Value, StringComparison.InvariantCultureIgnoreCase));
                                    break;
                                case FilterConditions.CONTAIN:
                                    expr = expr.And(c => c.COMMENT.ToUpper().Contains(((string)filter.Value).ToUpper()));
                                    break;
                            }
                            break;
                        #endregion

                        #region Fields.CONTENT
                        case Fields.CONTENT:
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                case FilterConditions.LESS:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.CONTENT.Equals((string)filter.Value, StringComparison.InvariantCultureIgnoreCase));
                                    break;
                                case FilterConditions.CONTAIN:
                                    expr = expr.And(c => c.CONTENT.ToUpper().Contains(((string)filter.Value).ToUpper()));
                                    break;
                            }
                            break;
                        #endregion

                        #region Fields.LINK
                        case Fields.LINK:

                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                case FilterConditions.LESS:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.LINK.Equals((string)filter.Value, StringComparison.InvariantCultureIgnoreCase));
                                    break;
                                case FilterConditions.CONTAIN:
                                    expr = expr.And(c => c.LINK.ToUpper().Contains(((string)filter.Value).ToUpper()));
                                    break;
                            }
                            break;
                        #endregion

                        #region Fields.TERM
                        case Fields.TERM:
                            value = filter.Value == null ? null : (int?)Convert.ToInt32(filter.Value);
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                case FilterConditions.LESS:
                                case FilterConditions.CONTAIN:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.term_type_id == value);
                                    break;
                            }
                            break;
                        #endregion

                        #region Fields.RENT_FROM
                        case Fields.RENT_FROM:
                            dvalue = Convert.ToDateTime(filter.Value);
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                    expr = expr.And(c => c.RENT_FROM > dvalue);
                                    break;
                                case FilterConditions.LESS:
                                    expr = expr.And(c => c.RENT_FROM < dvalue);
                                    break;
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.RENT_FROM == dvalue);
                                    break;
                                case FilterConditions.CONTAIN:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                            }
                            break;
                        #endregion

                        #region Fields.RENT_TO
                        case Fields.RENT_TO:
                            dvalue = Convert.ToDateTime(filter.Value);
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                    expr = expr.And(c => c.RENT_TO > dvalue);
                                    break;
                                case FilterConditions.LESS:
                                    expr = expr.And(c => c.RENT_TO < dvalue);
                                    break;
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.RENT_TO == dvalue);
                                    break;
                                case FilterConditions.CONTAIN:
                                    throw new Exception("CONTAIN not applicable for RENT_TO");
                                    break;
                            }
                            break;
                        #endregion

                        #region Fields.LESSOR
                        case Fields.LESSOR:
                            value = filter.Value == null ? null : (int?)Convert.ToInt32(filter.Value);
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                case FilterConditions.LESS:
                                case FilterConditions.CONTAIN:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.lessor_type_id == value);
                                    break;
                            }
                            break;
                        #endregion

                        #region Fields.FRIDGE
                        case Fields.FRIDGE:
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                case FilterConditions.LESS:
                                case FilterConditions.CONTAIN:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.FRIDGE == (bool)filter.Value);
                                    break;
                            }
                            break;
                        #endregion

                        #region Fields.TV
                        case Fields.TV:
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                case FilterConditions.LESS:
                                case FilterConditions.CONTAIN:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.TV == (bool)filter.Value);
                                    break;

                            }
                            break;
                        #endregion

                        #region Fields.WASHER
                        case Fields.WASHER:
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                case FilterConditions.LESS:
                                case FilterConditions.CONTAIN:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.WASHER == (bool)filter.Value);
                                    break;
                            }
                            break;
                        #endregion

                        #region Fields.COOLER
                        case Fields.COOLER:
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                case FilterConditions.LESS:
                                case FilterConditions.CONTAIN:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.COOLER == (bool)filter.Value);
                                    break;
                            }
                            break;
                        #endregion

                        #region Fields.REGION
                        case Fields.REGION:
                            value = filter.Value == null ? null : (int?)Convert.ToInt32(filter.Value);
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                case FilterConditions.LESS:
                                case FilterConditions.CONTAIN:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));

                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.region_type_id == value);
                                    break;
                            }
                            break;
                        #endregion

                        #region Fields.EMAIL
                        case Fields.EMAIL:
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                case FilterConditions.LESS:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.EMAIL.Equals((string)filter.Value, StringComparison.InvariantCultureIgnoreCase));
                                    break;
                                case FilterConditions.CONTAIN:
                                    expr = expr.And(c => c.EMAIL.ToUpper().Contains(((string)filter.Value).ToUpper()));
                                    break;
                            }
                            break;
                        #endregion

                        #region Fields.CATEGORY
                        case Fields.CATEGORY:
                            value = filter.Value == null ? null : (int?)Convert.ToInt32(filter.Value);
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                case FilterConditions.LESS:
                                case FilterConditions.CONTAIN:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.category_type_id == value);
                                    break;
                            }
                            break;
                        #endregion

                        #region Fields.TYPE
                        case Fields.TYPE:
                            value = filter.Value == null ? null : (int?)Convert.ToInt32(filter.Value);
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                case FilterConditions.LESS:
                                case FilterConditions.CONTAIN:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.rent_type_id == value);
                                    break;
                            }
                            break;
                        #endregion

                        #region Fields.PAYMENT
                        case Fields.PAYMENT:
                            value = filter.Value == null ? null : (int?)Convert.ToInt32(filter.Value);
                            switch (filter.FilterCondition)
                            {
                                case FilterConditions.MORE:
                                case FilterConditions.LESS:
                                case FilterConditions.CONTAIN:
                                    throw new Exception(String.Format("{0} not applicable for {1}", filter.FilterCondition.ToString(), filter.Field.ToString()));
                                case FilterConditions.EQUAL:
                                    expr = expr.And(c => c.payment_type_id == value);
                                    break;
                            }
                            break;
                        #endregion

                    }

                }
                #endregion
                result = result.Where(expr.Expand());
            }
            return result.Select(c => c)
                .ToSearchResult(query, e => e.InjectIntoNew<view_flat_info>());
        }

        public static List<flat_info> FlatListAll()
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

        public static flat_info FlatById(int flatId)
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
                findedFlat.COMMENT = flat.COMMENT;
                findedFlat.CONTENT = flat.CONTENT;
                 findedFlat.DATA = flat.DATA;
                findedFlat.FLOOR = flat.FLOOR;
                findedFlat.FURNITURE = flat.FURNITURE;

                findedFlat.lessor_type_id = flat.lessor_type_id;
                findedFlat.bathunit_type_id = flat.bathunit_type_id;
                findedFlat.buld_type_id = flat.buld_type_id;
                findedFlat.region_type_id = flat.region_type_id;
                findedFlat.state_type_id = flat.state_type_id;
                findedFlat.term_type_id = flat.term_type_id;
                findedFlat.rent_type_id = flat.rent_type_id;
                findedFlat.category_type_id = flat.category_type_id;

                findedFlat.LINK = flat.LINK;
                findedFlat.NAME = flat.NAME;
                findedFlat.PHONE = flat.PHONE;
                findedFlat.PRICE = flat.PRICE;
                
                findedFlat.RENT_FROM = flat.RENT_FROM;
                findedFlat.RENT_TO = flat.RENT_TO;
                findedFlat.ROOM_COUNT = flat.ROOM_COUNT;

                findedFlat.FRIDGE = flat.FRIDGE;
                findedFlat.COOLER = flat.COOLER;
                findedFlat.TV = flat.TV;
                findedFlat.WASHER = flat.WASHER;

                findedFlat.EMAIL = flat.EMAIL;

                context.SaveChanges();
            }

        }

        public static void FlatDelete(int flatId)
        {
            var context = WcfOperationContext.Current.Context;
            var flat = context.flat_info.Where(f => f.ID == flatId).FirstOrDefault();
            if (flat != null)
            {
                context.flat_info.Remove(flat);
                context.SaveChanges();
            }
        }

    }
}

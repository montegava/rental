﻿using System;
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
                            case Fields.ID:
                                int id = int.Parse(f.StartValue);
                                filter = filter.And(t => t.ID == id);
                                break;

                            case Fields.ROOM_COUNT:
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

                            case Fields.ADDRESS:
                                filter = filter.And(t => t.ADDRESS.Contains(f.StartValue.ToLower()));
                                break;

                            case Fields.FLOOR:

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

                            case Fields.BATH_UNIT:
                                filter = filter.And(t => t.BATH_UNIT.Contains(f.StartValue.ToLower()));
                                break;

                            case Fields.BUILD:
                                filter = filter.And(t => t.BUILD.Contains(f.StartValue.ToLower()));
                                break;

                            case Fields.FURNITURE:
                                filter = filter.And(t => t.FURNITURE.Contains(f.StartValue));
                                break;

                            case Fields.STATE:
                                filter = filter.And(t => t.STATE.Contains(f.StartValue.ToLower()));
                                break;

                            case Fields.MECHANIC:
                                filter = filter.And(t => t.MECHANIC.Contains(f.StartValue.ToLower()));
                                break;

                            case Fields.NAME:
                                filter = filter.And(t => t.NAME.Contains(f.StartValue.ToLower()));
                                break;

                            case Fields.PRICE:
                                filter = filter.And(t => t.PRICE.Contains(f.StartValue.ToLower()));

                                break;

                            case Fields.PHONE:
                                filter = filter.And(t => t.PHONE.Contains(f.StartValue.ToLower()));
                                break;

                            case Fields.COMMENT:
                                filter = filter.And(t => t.COMMENT.Contains(f.StartValue.ToLower()));

                                break;
                            case Fields.CONTENT:
                                filter = filter.And(t => t.CONTENT.Contains(f.StartValue.ToLower()));

                                break;
                            case Fields.LINK:
                                filter = filter.And(t => t.LINK.Contains(f.StartValue.ToLower()));

                                break;

                            case Fields.TERM:
                                filter = filter.And(t => t.TERM.Contains(f.StartValue.ToLower()));

                                break;
                            case Fields.LESSOR:
                                filter = filter.And(t => t.LESSOR.Contains(f.StartValue.ToLower()));

                                break;

                            case Fields.REGION:
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

                switch ((Fields)sortBy)
                {
                    case Fields.DATA:
                        query = query.SetOrder(t => t.DATA, orderBy);
                        break;
                    case Fields.ROOM_COUNT:
                        query = query.SetOrder(t => t.ROOM_COUNT, orderBy);
                        break;
                    case Fields.ADDRESS:
                        query = query.SetOrder(t => t.ADDRESS, orderBy);
                        break;
                    case Fields.FLOOR:
                        query = query.SetOrder(t => t.FLOOR, orderBy);
                        break;
                    case Fields.BATH_UNIT:
                        query = query.SetOrder(t => t.BATH_UNIT, orderBy);
                        break;
                    case Fields.BUILD:
                        query = query.SetOrder(t => t.BUILD, orderBy);
                        break;
                    case Fields.FURNITURE:
                        query = query.SetOrder(t => t.FURNITURE, orderBy);
                        break;
                    case Fields.STATE:
                        query = query.SetOrder(t => t.STATE, orderBy);
                        break;
                    case Fields.MECHANIC:
                        query = query.SetOrder(t => t.MECHANIC, orderBy);
                        break;
                    case Fields.NAME:
                        query = query.SetOrder(t => t.NAME, orderBy);
                        break;
                    case Fields.PRICE:
                        query = query.SetOrder(t => t.PRICE, orderBy);
                        break;
                    case Fields.PHONE:
                        query = query.SetOrder(t => t.PHONE, orderBy);
                        break;
                    case Fields.COMMENT:
                        query = query.SetOrder(t => t.COMMENT, orderBy);
                        break;
                    case Fields.CONTENT:
                        query = query.SetOrder(t => t.CONTENT, orderBy);
                        break;
                    case Fields.LINK:
                        query = query.SetOrder(t => t.LINK, orderBy);
                        break;
                    case Fields.TERM:
                        query = query.SetOrder(t => t.TERM, orderBy);
                        break;
                    case Fields.RENT_FROM:
                        query = query.SetOrder(t => t.RENT_FROM, orderBy);
                        break;
                    case Fields.RENT_TO:
                        query = query.SetOrder(t => t.RENT_TO, orderBy);
                        break;
                    case Fields.LESSOR:
                        query = query.SetOrder(t => t.LESSOR, orderBy);
                        break;
                    case Fields.FRIDGE:
                        query = query.SetOrder(t => t.FRIDGE, orderBy);
                        break;
                    case Fields.TV:
                        query = query.SetOrder(t => t.TV, orderBy);
                        break;
                    case Fields.WASHER:
                        query = query.SetOrder(t => t.WASHER, orderBy);
                        break;
                    case Fields.COOLER:
                        query = query.SetOrder(t => t.COOLER, orderBy);
                        break;
                    case Fields.REGION:
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

        public static void FlatDelete(int flatId)
        {
            var context = WcfOperationContext.Current.Context;
            var flat = context.flat_info.Where(f => f.ID == flatId).FirstOrDefault();
            if (flat != null)
                context.flat_info.Remove(flat);
        }

    }
}

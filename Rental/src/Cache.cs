﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using RentalCommon;

namespace Rental.src
{

    public class FlatRow
    {



        public flat_info Flat;

        public FlatRow(flat_info flat)
        {
            Flat = flat;
        }

        public object this[string fieldName]
        {
            get
            {
                Fields field;
                if (Enum.TryParse<Fields>(fieldName, out field))
                    return this[(int)field];
                return string.Empty;
            }
        }


        public object this[int i]
        {
            get
            {
                var field = (Fields)i;

                switch (field)
                {
                    case Fields.ID:
                        return Flat.ID;
                    case Fields.DATA:
                        return Flat.DATA;

                    case Fields.ROOM_COUNT:
                        return Flat.ROOM_COUNT;

                    case Fields.ADDRESS:
                        return Flat.ADDRESS;

                    case Fields.FLOOR:
                        return Flat.FLOOR;

                    case Fields.BATH_UNIT:
                        return Flat.BATH_UNIT;

                    case Fields.BUILD:
                        return Flat.BUILD;

                    case Fields.FURNITURE:
                        return Flat.FURNITURE;

                    case Fields.STATE:
                        return Flat.STATE;

                    case Fields.MECHANIC:
                        return Flat.MECHANIC;

                    case Fields.NAME:
                        return Flat.NAME;

                    case Fields.PRICE:
                        return Flat.PRICE;

                    case Fields.PHONE:
                        return Flat.PHONE;

                    case Fields.COMMENT:
                        return Flat.COMMENT;

                    case Fields.CONTENT:
                        return Flat.CONTENT;

                    case Fields.LINK:
                        return Flat.LINK;

                    case Fields.TERM:
                        return Flat.TERM;

                    case Fields.RENT_FROM:
                        return Flat.RENT_FROM;

                    case Fields.RENT_TO:
                        return Flat.RENT_TO;

                    case Fields.LESSOR:
                        return Flat.LESSOR;

                    case Fields.FRIDGE:
                        return Flat.FRIDGE;

                    case Fields.TV:
                        return Flat.TV;

                    case Fields.WASHER:
                        return Flat.WASHER;

                    case Fields.COOLER:
                        return Flat.COOLER;

                    case Fields.REGION:
                        return Flat.REGION;

                    case Fields.EMAIL:
                        return Flat.EMAIL;

                    case Fields.CATEGORY:
                        return Flat.CATEGORY;

                    case Fields.TYPE:
                        return Flat.TYPE;

                    case Fields.PAYMENT:
                        return Flat.PAYMENT;

                    default:
                        return string.Empty;
                }

            }
        }
    }

    public class Cache
    {
        Dictionary<int, FlatRow> FlatRows = new Dictionary<int, FlatRow>();

        public int TotalRowsNumber;

        public FlatRow this[int i]
        {
            get
            {

                if (!FlatRows.ContainsKey(i))
                {

                    //int pageActive = this.listModel.Query.Page;
                    //int pageCount = this.listModel.Result.TotallCount / this.listModel.Query.PageSize;
                    //if ((this.listModel.Result.TotallCount % this.listModel.Query.PageSize) > 0)
                    //{
                    //    pageCount++;
                    //}


                    int activePage = (int)((i + FlatRows.Count) / 50);
                    flat_info[] flats;
                    int pageCount;
                    int totalRowsNumber;


                   var result = NameListCache.proxy.FlatSearch(new SearchQuery() { SortField = Fields.ID, Ascending = false, Page = activePage, PageSize =50 });


                  TotalRowsNumber = result.TotallCount;

                    var curRow = i;

                    foreach (var item in result.Items)
                    {

                        if (!FlatRows.ContainsKey(curRow))
                        {
                            FlatRows.Add(curRow, new FlatRow(item));

                        }
                        curRow++;
                    }

                }

                return FlatRows[i];




            }
        }

        public int Count { get { return FlatRows.Count; } }

        public void Remove(int index)
        {
            if (FlatRows.ContainsKey(index))
                FlatRows.Remove(index);
        }

        public void RemoveAll()
        {
            FlatRows.Clear();
        }

    }


    public class NameListCache
    {
        public int PageSize = 50;
        public int TotalRowsNumber;
        public Cache CachedData;
        public static RentalCore.RentalCoreClient proxy = new RentalCore.RentalCoreClient();

        public NameListCache(int pageSize)
        {
            CachedData = new Cache();
            PageSize = pageSize;
            LoadPage(0);
        }

        public void LoadPage(int rowIndex)
        {
            var result = CachedData[rowIndex];
            TotalRowsNumber = CachedData.TotalRowsNumber;
        }
    }

}

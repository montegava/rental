using System;
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

        public object this[int i]
        {
            get
            {
                var field = (Fiels)i;

                switch (field)
                {
                    case Fiels.ID:
                        return Flat.ID;
                    case Fiels.DATA:
                        return Flat.DATA;

                    case Fiels.ROOM_COUNT:
                        return Flat.ROOM_COUNT;

                    case Fiels.ADDRESS:
                        return Flat.ADDRESS;

                    case Fiels.FLOOR:
                        return Flat.FLOOR;

                    case Fiels.BATH_UNIT:
                        return Flat.BATH_UNIT;

                    case Fiels.BUILD:
                        return Flat.BUILD;

                    case Fiels.FURNITURE:
                        return Flat.FURNITURE;

                    case Fiels.STATE:
                        return Flat.STATE;

                    case Fiels.MECHANIC:
                        return Flat.MECHANIC;

                    case Fiels.NAME:
                        return Flat.NAME;

                    case Fiels.PRICE:
                        return Flat.PRICE;

                    case Fiels.PHONE:
                        return Flat.PHONE;

                    case Fiels.COMMENT:
                        return Flat.COMMENT;

                    case Fiels.CONTENT:
                        return Flat.CONTENT;

                    case Fiels.LINK:
                        return Flat.LINK;

                    case Fiels.TERM:
                        return Flat.TERM;

                    case Fiels.RENT_FROM:
                        return Flat.RENT_FROM;

                    case Fiels.RENT_TO:
                        return Flat.RENT_TO;

                    case Fiels.LESSOR:
                        return Flat.LESSOR;

                    case Fiels.FRIDGE:
                        return Flat.FRIDGE;

                    case Fiels.TV:
                        return Flat.TV;

                    case Fiels.WASHER:
                        return Flat.WASHER;

                    case Fiels.COOLER:
                        return Flat.COOLER;

                    case Fiels.REGION:
                        return Flat.REGION;

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
                    int activePage = (int)((i + FlatRows.Count) / 50);
                    flat_info[] flats;
                    int pageCount;
                    int totalRowsNumber;


                    NameListCache.proxy.FlatList(
                        new Filter[] { },
                        DateTime.MinValue,
                        DateTime.MinValue,
                        1,
                        false,
                        ref activePage,
                        out flats,
                        out pageCount,
                        out totalRowsNumber,
                        50);

                    TotalRowsNumber = totalRowsNumber;

                    var curRow = i;

                    foreach (var item in flats)
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



        public int Count
        {

            get
            {
                return FlatRows.Count;
            }
        }

    }


    public class NameListCache
    {
        public int PageSize = 5000;
        public int TotalRowsNumber;
        public Cache CachedData = new Cache();

        public static RentalCore.RentalCoreClient proxy = new RentalCore.RentalCoreClient();


        int _lastRowIndex = -1;

        public NameListCache(int pageSize)
        {
            PageSize = pageSize;
            LoadPage(0);
        }

        public void LoadPage(int rowIndex)
        {
            int lastRowIndex = rowIndex - (rowIndex % PageSize);
            if (lastRowIndex == _lastRowIndex) return;
            _lastRowIndex = lastRowIndex;

            long? totalCount = 0;

            var d = CachedData[rowIndex];

            TotalRowsNumber = CachedData.TotalRowsNumber;

       
        }
    }

}

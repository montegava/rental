using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace DataGridViewVirtualModePaging
{


    public class FlatRow
    {

       

        public RentalCore.flat_info Flat;

        public FlatRow(RentalCore.flat_info flat)
        {
            Flat = flat;
        }

        public object this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return Flat.ADDRESS;
                    case 1:
                        return Flat.FLOOR;
                    default:
                        return Flat.DATA;
                }
            }
        }
    }

    public class Cache
    {
        Dictionary<int, FlatRow> FlatRows = new  Dictionary<int, FlatRow>();

        public int TotalRowsNumber;

        public FlatRow this[int i]
        {
            get 
            {
              
                    if (!FlatRows.ContainsKey(i))
                    {
                        int activePage = (int)((i + FlatRows.Count) / 50);
                        RentalCore.flat_info[] flats;
                        int pageCount;
                        int totalRowsNumber;


                        NameListCache.proxy.FlatList(
                            new RentalCore.Filter[] { },
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

    

        public int Count {

            get {
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

            //if (CachedData.Count - 1 < rowIndex)
            //{

            //int activePage = 0;
            //RentalCore.flat_info[] flats;
            //int pageCount;
            //int totalRowsNumber;


            //proxy.FlatList(
            //     new RentalCore.Filter[] { },
            //    DateTime.MinValue,
            //    DateTime.MinValue,
            //    1,
            //    false,
            //    ref activePage,
            //    out flats,
            //    out pageCount,
            //    out TotalRowsNumber,
            //    50);


            //foreach (var item in flats)
            //{

            //    if (!FlatRows.ContainsKey(curRow))
            //    {
            //        FlatRows.Add(curRow, new FlatRow(item));

            //    }
            //    curRow++;
            //}

            //        CachedData.Add(flats);

            //}


       


       //     TotalRowsNumber = CachedData.Count;
        }
    }
}

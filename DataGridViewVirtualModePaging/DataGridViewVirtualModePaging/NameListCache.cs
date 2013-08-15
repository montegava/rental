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
        List<FlatRow> FlatRows = new List<FlatRow>();


        public FlatRow this[int i]
        {
            get 
            {


                if (FlatRows.Count - 1 < i)
                {
                    int activePage =   (int)((i + FlatRows.Count)/50)   ;
                    RentalCore.flat_info[] flats;
                    int pageCount;
                    int totalRowsNumber;


                    NameListCache.proxy.FlatList(
                        "",
                        0,
                        DateTime.MinValue,
                        DateTime.MinValue,
                        1,
                        false,
                        ref activePage,
                        out flats,
                        out pageCount,
                        out totalRowsNumber,
                        50);

                    Add(flats);
                }

                return FlatRows[i];
            
            }
        }

        public void Add(RentalCore.flat_info[] flats)
        {
            if (FlatRows.Count == 0)
            {
                FlatRows.AddRange(flats.Select(f=>new FlatRow(f)));
            }
            else
            {
                foreach (var item in flats)
                {
                    if (!FlatRows.Any(f => f.Flat.ID == item.ID))
                    {
                        FlatRows.Add(new FlatRow(item));
                    }
                }

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





            if (CachedData.Count - 1 < rowIndex)
            {

                    int activePage = 0;
                    RentalCore.flat_info[] flats;
                    int pageCount;
                    int totalRowsNumber;


                    proxy.FlatList(
                        "", 
                        0,
                        DateTime.MinValue,
                        DateTime.MinValue,
                        1,
                        false,
                        ref activePage,
                        out flats,
                        out pageCount,
                        out TotalRowsNumber,
                        50);

                    CachedData.Add(flats);

            }


       


       //     TotalRowsNumber = CachedData.Count;
        }
    }
}

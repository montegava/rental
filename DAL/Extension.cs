using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace DAL
{
    /// <summary>
    /// Class of extensions to entity query
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// Select all by key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="keys">The keys.</param>
        /// <returns></returns>
        public static IEnumerable<T> SelectAllByKey<T, TKey>(this IDictionary<TKey, T> dictionary, IEnumerable<TKey> keys)
        {
            foreach (var key in keys)
            {
                T value;
                if (dictionary.TryGetValue(key, out value))
                {
                    yield return value;
                }
            }
        }

        /// <summary>
        /// Sets the order and condition of sorting.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TT"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="sort">The sorting condition.</param>
        /// <param name="order">if set to <c>true</c> ascending sort if <c>false</c> descending.</param>
        /// <returns></returns>
        public static IQueryable<T> SetOrder<T, TT>(this IQueryable<T> query, Expression<Func<T, TT>> sort, bool order)
        {
            return order ? query.OrderBy(sort) : query.OrderByDescending(sort);
        }

        /// <summary>
        /// Gets elements for specific page.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="activePage">The active page number.</param>
        /// <param name="pageCount">The page count.</param>
        /// <param name="totalCount">The total rows count.</param>
        /// <returns></returns>
        public static IQueryable<T> GetPage<T>(this IQueryable<T> query, int pageSize, ref int activePage, ref int pageCount, ref int totalCount)
        {
            totalCount = query.Count();
            pageCount = totalCount / pageSize;
            if (totalCount % pageSize != 0) pageCount++;
            // -- validate active page - must be between 1 - number of pages (nrOfPages)
            if (pageCount < activePage + 1) activePage = pageCount;
            if (activePage <= 0) activePage = 1;
            return query.Skip((activePage - 1) * pageSize).Take(pageSize);
        }



        /// <summary>
        /// Gets elements for specific page.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TT">The type of the T.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="condition">The condition.</param>
        /// <param name="listResult">The list result.</param>
        /// <returns></returns>
        public static IQueryable<T> GetPage<T, TT>(this IQueryable<T> query, ListQuery condition, ListResult<TT> listResult)
        {
            listResult.TotalRowsCount = query.Count();
            listResult.PageCount = listResult.TotalRowsCount / condition.PageSize;
            if (listResult.TotalRowsCount % condition.PageSize != 0) listResult.PageCount++;
            // -- validate active page - must be between 1 - number of pages (nrOfPages)
            if (listResult.PageCount < condition.ActivePage + 1) listResult.ActivePage = listResult.PageCount;
            if (condition.ActivePage <= 0) listResult.ActivePage = 1;
            return query.Count() > 0 ? query.Skip((listResult.ActivePage - 1) * condition.PageSize).Take(condition.PageSize) : query;
        }

 

    }

    public class ListQuery
    {

        public int SortBy { get; set; }

        public bool OrderBy { get; set; }

        public string FilterValue { get; set; }

        public int FilterBy { get; set; }

        public int PageSize { get; set; }

        public int ActivePage { get; set; }
    }

    public class ListResult<T>
    {
        public ListResult(ListQuery condition)
        {
            ActivePage = condition.ActivePage;
        }

   
        public List<T> Items { get; set; }

     
        public int ActivePage { get; set; }

      
        public int PageCount { get; set; }

       
        public int TotalRowsCount { get; set; }
    }
}
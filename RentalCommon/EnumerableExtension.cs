using System;
using System.Linq;


namespace RentalCommon
{
    public static class EnumerableExtension
    {
        /// <summary>
        /// Orders passed IQueryable and selects page required in the query, converts entities to business objects using mapper function
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <typeparam name="TE">The type of the business object.</typeparam>
        /// <param name="items">The items which should be processed.</param>
        /// <param name="query">The query based on which ordering, and paging would be done.</param>
        /// <param name="mapper">The mapper function to convert entities to business objects.</param>
        /// <returns></returns>
        public static SearchResult<T> ToSearchResult<T>(this IQueryable<T> items, SearchQuery query)
        {
            return ToSearchResult(items, query, t=>t);
        }

        /// <summary>
        /// Orderes passed IQueryable and selects page required in the query, converts entities to business objects using mapper function
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <typeparam name="TE">The type of the business oject.</typeparam>
        /// <param name="items">The items which should be processed.</param>
        /// <param name="query">The query based on which ordering, and paging would be done.</param>
        /// <param name="mapper">The mapper function to convert entities to business objects.</param>
        /// <returns></returns>
        public static SearchResult<TE> ToSearchResult<T, TE>(this IQueryable<T> items, SearchQuery query, Func<T, TE> mapper)
        {
            var count = items.Count();
            var results = items.OrderBy(query.SortField.ToString(), query.Ascending)
                .Skip(query.PageSize * query.Page)
                .Take(query.PageSize);

            return new SearchResult<TE>(results.Select(mapper), count);
        }
    }
}
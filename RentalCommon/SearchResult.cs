using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace RentalCommon
{
    /// <summary>
    /// Holds the result of entity query, entities and totall count required to do the pagination
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    public class SearchResult<T> : ISearchResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResult&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="items">The filtered, ordered, and paged list of entities.</param>
        /// <param name="count">The total count of entities matched by filter.</param>
        public SearchResult(IEnumerable<T> items, int count)
        {
            this.Items = items.ToArray();
            this.TotallCount = count;
        }

        /// <summary>
        /// Gets or sets the array of items returned by query, its length should be less or equal to page size
        /// </summary>
        [DataMember]
        public T[] Items { get; set; }

        /// <summary>
        /// Gets or sets the totall count of entities matched by query
        /// </summary>
        [DataMember]
        public int TotallCount { get; set; }

        IEnumerable ISearchResult.Items { get { return Items; } }
    }
}

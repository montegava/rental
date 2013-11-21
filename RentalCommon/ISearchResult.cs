using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace RentalCommon
{
     /// <summary>
    /// Holds the result of entity query, entities and totall count required to do the pagination
    /// </summary>
    public interface ISearchResult
    {    
        /// <summary>
        /// Gets or sets the array of items returned by query, its length should be less or equal to page size
        /// </summary>
        IEnumerable Items { get; }

        /// <summary>
        /// Gets or sets the totall count of entities matched by query
        /// </summary>
        int TotallCount { get; }
    }
}


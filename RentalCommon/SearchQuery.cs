using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace RentalCommon
{
   /// <summary>
    /// Base class for entity queries, contains ordering and filtering abilities, should inherited and extended if more filter criterias required
    /// </summary>
    [DataContract]
    [Serializable]
    public class SearchQuery
    {
        public SearchQuery()
        {
            this.Page = 0;
            this.PageSize = 25;
            this.Ascending = false;
            this.SortField = Fields.DATA;
        }
        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        [DataMember]
        public int Page { get; set; }
        /// <summary>
        /// Gets or sets the selected page size, 25 by default
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }
        /// <summary>
        /// Gets or sets the field resulting set is ordered by.
        /// </summary>
        [DataMember]
        public Fields SortField { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether ordering should be performed in ascending way.
        /// </summary>
        /// <value>
        ///   <c>true</c> if ascending; otherwise, <c>descenting</c>.
        /// </value>
        [DataMember]
        public bool Ascending { get; set; }

        [DataMember]
        public Filter1[] Filters { get; set; }
    }
}

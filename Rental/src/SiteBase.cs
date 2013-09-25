using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace Rental.src
{
    class SiteBase
    {
        protected List<black_list> ExcludeList {get; set;} 
        private SiteBase() { }

        public SiteBase(List<black_list> excludeList)
        {
            ExcludeList = excludeList;
        }

    }
}

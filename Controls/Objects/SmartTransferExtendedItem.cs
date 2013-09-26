using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PageControls
{
    [Serializable()]
    public class SmartTransferExtendedItem
    {
        #region fields
        private int id;
        private string keyword;
        private DateTime startDate;
        private DateTime endDate;
        #endregion

        #region constructor
        public SmartTransferExtendedItem() { }
        public SmartTransferExtendedItem(int id, string keyword, DateTime startDate, DateTime endDate)
        {
            this.id = id;
            this.keyword = keyword;
            this.startDate = startDate;
            this.endDate = endDate;
        }
        #endregion

        #region getters/setters
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Keyword
        {
            get { return keyword; }
            set { keyword = value; }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        #endregion
    }
}

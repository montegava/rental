using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PageControls
{
    [Serializable()]
    public class SmartItem
    {
        #region fields
        private int id;
        private string keyword;
        #endregion

        #region constructor
        public SmartItem() { }
        public SmartItem(int id, string keyword)
        {
            this.id = id;
            this.keyword = keyword;
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
        #endregion
    }
}

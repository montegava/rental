using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PageControls
{
    [Serializable()]
    public class SmartTooltipItem
    {
        #region fields
        private int id;
        private string keyword;
        private string tooltip;
        #endregion

        #region constructor
        public SmartTooltipItem() { }
        public SmartTooltipItem(int id, string keyword, string tooltip)
        {
            this.id = id;
            this.keyword = keyword;
            this.tooltip = tooltip;
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
        public string Tooltip
        {
            get { return tooltip; }
            set { tooltip = value; }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartControls
{
    [Serializable()]
    public class SmartItems
    {
        #region fields
        private SmartItem item1;
        private SmartItem item2;
        #endregion

        #region constructor
        public SmartItems() { }
        public SmartItems(SmartItem item1, SmartItem item2)
        {
            this.item1 = item1;
            this.item2 = item2;
        }
        #endregion

        #region getters/setters
        public SmartItem Item1
        {
            get { return item1; }
            set { item1 = value; }
        }
        public SmartItem Item2
        {
            get { return item2; }
            set { item2 = value; }
        }
        #endregion
    }
}

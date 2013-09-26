using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PageControls
{
    [Serializable()]
    public class SmartCollectionV5Item
    {
        #region fields
        private SmartItem item1;
        private SmartItem item2;
        private SmartItem item3;
        private SmartItem s1;
        #endregion

        #region constructor
        public SmartCollectionV5Item() { }
        public SmartCollectionV5Item(SmartItem item1, SmartItem item2, SmartItem item3, SmartItem s1)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
            this.s1 = s1;
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

        public SmartItem Item3
        {
            get { return item3; }
            set { item3 = value; }
        }

        public SmartItem S1
        {
            get { return s1; }
            set { s1 = value; }
        }

        #endregion
    }
}

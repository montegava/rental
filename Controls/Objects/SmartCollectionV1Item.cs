using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PageControls
{
    [Serializable()]
    public class SmartCollectionV1Item
    {
        #region fields
        private Int64 id;
        private SmartItem firstItem;
        private SmartItem secondItem;
        private int position;
        #endregion

        #region constructor
        public SmartCollectionV1Item() { }
        public SmartCollectionV1Item(Int64 id, SmartItem firstItem, SmartItem secondItem, int position)
        {
            this.id = id;
            this.firstItem = firstItem;
            this.secondItem = secondItem;
            this.position = position;
        }
        #endregion

        #region getters/setters
        public Int64 Id
        {
            get { return id; }
            set { id = value; }
        }
        public SmartItem FirstItem
        {
            get { return firstItem; }
            set { firstItem = value; }
        }
        public SmartItem SecondItem
        {
            get { return secondItem; }
            set { secondItem = value; }
        }
        public int Position
        {
            get { return position; }
            set { position = value; }
        }
        #endregion
    }
}

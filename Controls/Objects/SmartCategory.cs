using System;
using System.Collections.Generic;

namespace PageControls
{
    [Serializable]
    public class SmartCategory : SmartSelectableItem
    {
        #region fields
        private List<SmartSelectableItem> items;
        #endregion

        #region constructor
        public SmartCategory()
        {
            Items = new List<SmartSelectableItem>();
        }

        public SmartCategory(int id, string keyword, bool selected)
            : base(id, keyword, selected)
        {
            Items = new List<SmartSelectableItem>();
        }
        #endregion

        #region getters/setters
        public List<SmartSelectableItem> Items
        {
            get { return items; }
            set { items = value; }
        }
        #endregion

    }
}
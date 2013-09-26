using System;

namespace PageControls
{

    [Serializable]
    public class SmartSelectableItem : SmartItem
    {
        #region fields
        private bool selected;
        #endregion

        #region constructor
        public SmartSelectableItem() { }
        public SmartSelectableItem(int id, string keyword, bool selected)
            : base(id, keyword)
        {
            this.selected = selected;
        }
        #endregion

        #region getters/setters
        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        #endregion

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PageControls
{
    [Serializable()]
    public class SmartCompositeV2Item
    {
        #region fields
        private Int32 type; // (No Chapters, Manual Definition, Automatic Split)

        private List<SmartCollectionV3Item> type2List;
        private List<SmartItem> type2AllList;

        private Int32 type3Option;
        private Int32 type3Value;
        #endregion

        #region constructor
        public SmartCompositeV2Item() { }
        public SmartCompositeV2Item(Int32 type, List<SmartCollectionV3Item> type2List, List<SmartItem> type2AllList, Int32 type3Option, Int32 type3Value)
        {
            this.type = type;
            this.type2List = type2List;
            this.type2AllList = type2AllList;
            this.type3Option = type3Option;
            this.type3Value = type3Value;
        }
        #endregion

        #region getters/setters
        public Int32 Type
        {
            get { return type; }
            set { type = value; }
        }
        public List<SmartCollectionV3Item> Type2List
        {
            get { return type2List; }
            set { type2List = value; }
        }
        public Int32 Type3Option
        {
            get { return type3Option; }
            set { type3Option = value; }
        }
        public Int32 Type3Value
        {
            get { return type3Value; }
            set { type3Value = value; }
        }
        public List<SmartItem> Type2AllList
        {
            get { return type2AllList; }
            set { type2AllList = value; }
        }
        #endregion
    }
}

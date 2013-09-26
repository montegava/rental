using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PageControls
{
    [Serializable()]
    public class SmartCollectionV3Item
    {
        #region fields
        private string s1;
        private string s2;
        private SmartItem si1;
        #endregion

        #region constructor
        public SmartCollectionV3Item() { }
        public SmartCollectionV3Item(string s1, string s2, SmartItem si1)
        {
            this.s1 = s1;
            this.s2 = s2;
            this.si1 = si1;
        }
        #endregion

        #region getters/setters
        public string S1
        {
            get { return s1; }
            set { s1 = value; }
        }
        public string S2
        {
            get { return s2; }
            set { s2 = value; }
        }
        public SmartItem Si1
        {
            get { return si1; }
            set { si1 = value; }
        }
        #endregion
    }
}

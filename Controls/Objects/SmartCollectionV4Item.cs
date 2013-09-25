using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartControls
{
    [Serializable()]
    public class SmartCollectionV4Item
    {
        #region fields
        private string s1;
        private string s2;
        private string s3;
        private string s4;
        private bool b1;
        private SmartItem si1;
        #endregion

        #region constructor
        public SmartCollectionV4Item() { }
        public SmartCollectionV4Item(string s1, string s2, string s3, string s4, bool b1, SmartItem si1)
        {
            this.s1 = s1;
            this.s2 = s2;
            this.s3 = s3;
            this.s4 = s4;
            this.b1 = b1;
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
        public string S3
        {
            get { return s3; }
            set { s3 = value; }
        }
        public string S4
        {
            get { return s4; }
            set { s4 = value; }
        }
        public bool B1
        {
            get { return b1; }
            set { b1 = value; }
        }
        public SmartItem Si1
        {
            get { return si1; }
            set { si1 = value; }
        }
        #endregion
    }
}

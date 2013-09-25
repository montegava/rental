using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartControls
{
    [Serializable()]
    public class SmartCompositeItem
    {
        #region fields

        // -- producer, writer, director = 1; actor = 2; musician = 3;
        private Int32 type;
        private Int32 position;

        // -- name of the producer or writer or director
        private string type1_value;

        // -- name of the actor and role name
        private string type2_value1;
        private string type2_value2;

        // -- name of the band and the songs
        private string type3_value;
        private List<string> type3_keywords;
        #endregion

        #region constructor
        public SmartCompositeItem() { }
        public SmartCompositeItem(Int32 type, Int32 position, string type1_value, string type2_value1, string type2_value2, string type3_value, List<string> type3_keywords)
        {
            this.type = type;
            this.position = position;
            this.type1_value = type1_value;
            this.type2_value1 = type2_value1;
            this.type2_value2 = type2_value2;
            this.type3_value = type3_value;
            this.type3_keywords = type3_keywords;
        }
        #endregion

        #region getters/setters
        public Int32 Type
        {
            get { return type; }
            set { type = value; }
        }
        public string Type1_value
        {
            get { return type1_value; }
            set { type1_value = value; }
        }
        public string Type2_value1
        {
            get { return type2_value1; }
            set { type2_value1 = value; }
        }
        public string Type2_value2
        {
            get { return type2_value2; }
            set { type2_value2 = value; }
        }
        public string Type3_value
        {
            get { return type3_value; }
            set { type3_value = value; }
        }
        public List<string> Type3_keywords
        {
            get { return type3_keywords; }
            set { type3_keywords = value; }
        }
        public int Position
        {
            get { return position; }
            set { position = value; }
        }
        #endregion
    }
}

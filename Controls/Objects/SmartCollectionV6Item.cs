using System;

namespace SmartControls
{
    [Serializable()]
    public class SmartCollectionV6Item
    {
        #region fields
        private Int64 id;
        private string keyword;
        private int position;
        private string type;

        #endregion

        #region constructor
        public SmartCollectionV6Item() { }
        public SmartCollectionV6Item(Int64 id, string keyword, int position)
        {
            this.id = id;
            this.keyword = keyword;
            this.position = position;
        }
        #endregion

        #region getters/setters
        public Int64 Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Position
        {
            get { return position; }
            set { position = value; }
        }

        public string Keyword
        {
            get { return keyword; }
            set { keyword = value; }
        }

        public string Type
        {
            get {
                return type;
            }
            set {
                type = value;
            }
        }

        #endregion
    }
}
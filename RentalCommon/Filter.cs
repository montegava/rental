using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace RentalCommon
{
    [DataContract]
    public class Filter1
    {
        public Filter1(Fields field,  FilterConditions condition, object value)
        {
            FilterCondition = condition;
            Field = field;
            Value = value;
        }

        [DataMember]
        public object Value { get; set; }

        [DataMember]
        public Fields Field { get; set; }

        [DataMember]
        public FilterConditions FilterCondition { get; set; }

        private Filter1() { }
    }


    [DataContract]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class Filter
    {
        public Filter()
        {
            Comparator = ComapreType.NONE;
            StartValue = string.Empty;
            EndValue = string.Empty;
        }

        [DataMember]
        public Fields Field { get; set; }

        [DataMember]
        public string StartValue { get; set; }


        [DataMember]
        public string EndValue { get; set; }

        [DataMember]
        public ComapreType Comparator { get; set; }
    }

    [DataContract]
    public enum ComapreType
    {
        [EnumMember]
        NONE = 0,

        [EnumMember]
        MORE = 1,

        [EnumMember]
        LESS = 2,

        [EnumMember]
        BETWEEN = 3
    }

  
  


}

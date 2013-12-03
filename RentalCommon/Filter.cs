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

        public Filter1() { }
    }

}

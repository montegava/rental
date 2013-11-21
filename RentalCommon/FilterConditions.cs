using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace RentalCommon
{
   [DataContract]
    public enum FilterConditions
    {
        // a > 1
        [EnumMember]
        MORE = 1,

        //a < 1
        [EnumMember]
        LESS = 2,

        //a = 1
        [EnumMember]
        EQUAL = 3,

        //a like '%1%'
        [EnumMember]
        CONTAIN = 4

    }

}

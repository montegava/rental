using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace RentalCommon
{
    [DataContract]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class Filter
    {
        [DataMember]
        public Fiels Field { get; set; }

        [DataMember]
        public string Value { get; set; }
    }

    [DataContract]
    public enum Fiels : int
    {
        [EnumMember]
        NONE = 0,
        [EnumMember]
        ID = 1,
        [EnumMember]
        DATA = 2,
        [EnumMember]
        ROOM_COUNT = 3,

        [EnumMember]
        ADDRESS = 4,
        [EnumMember]
        FLOOR = 5,
        [EnumMember]
        BATH_UNIT = 6,
        [EnumMember]
        BUILD = 7,
        [EnumMember]
        FURNITURE = 8,
        [EnumMember]
        STATE = 9,
        [EnumMember]
        MECHANIC = 10,
        [EnumMember]
        NAME = 11,
        [EnumMember]
        PRICE = 12,
        [EnumMember]
        PHONE = 13,
        [EnumMember]
        COMMENT = 14,
        [EnumMember]
        CONTENT = 15,
        [EnumMember]
        LINK = 16,
        [EnumMember]
        TERM = 17,
        [EnumMember]
        RENT_FROM = 18,
        [EnumMember]
        RENT_TO = 19,
        [EnumMember]
        LESSOR = 20,
        [EnumMember]
        FRIDGE = 21,
        [EnumMember]
        TV = 22,
        [EnumMember]
        WASHER = 23,
        [EnumMember]
        COOLER = 24,
        [EnumMember]
        REGION = 25,
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace RentalCommon
{
    [DataContract]
    public enum Fields : int
    {
         [EnumMember]
        ID = 1,

        //Дата создания записи
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

        [EnumMember]
        EMAIL = 26,
        [EnumMember]
        CATEGORY = 27,
        [EnumMember]
        TYPE = 28,
        [EnumMember]
        PAYMENT = 29,
    }

}

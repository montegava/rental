//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class view_flat_info
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> DATA { get; set; }
        public Nullable<int> ROOM_COUNT { get; set; }
        public string ADDRESS { get; set; }
        public Nullable<int> FLOOR { get; set; }
        public string BATH_UNIT { get; set; }
        public string BUILD { get; set; }
        public string FURNITURE { get; set; }
        public string STATE { get; set; }
        public string NAME { get; set; }
        public string PRICE { get; set; }
        public string PHONE { get; set; }
        public string COMMENT { get; set; }
        public string CONTENT { get; set; }
        public string LINK { get; set; }
        public string TERM { get; set; }
        public Nullable<System.DateTime> RENT_FROM { get; set; }
        public Nullable<System.DateTime> RENT_TO { get; set; }
        public string LESSOR { get; set; }
        public bool FRIDGE { get; set; }
        public bool TV { get; set; }
        public bool WASHER { get; set; }
        public bool COOLER { get; set; }
        public string REGION { get; set; }
        public string EMAIL { get; set; }
        public string CATEGORY { get; set; }
        public string TYPE { get; set; }
        public string PAYMENT { get; set; }
        public Nullable<int> bathunit_type_id { get; set; }
        public Nullable<int> buld_type_id { get; set; }
        public Nullable<int> state_type_id { get; set; }
        public Nullable<int> term_type_id { get; set; }
        public Nullable<int> lessor_type_id { get; set; }
        public Nullable<int> region_type_id { get; set; }
        public Nullable<int> category_type_id { get; set; }
        public Nullable<int> rent_type_id { get; set; }
        public Nullable<int> payment_type_id { get; set; }
    }
}
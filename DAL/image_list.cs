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
    
    public partial class image_list
    {
        public int ID { get; set; }
        public Nullable<int> FLAT_ID { get; set; }
        public string IMAGE_PATH { get; set; }
    
        public virtual flat_info flat_info { get; set; }
    }
}

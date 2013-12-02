using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using log4net;


namespace DAL
{
    public static class TypesManager
    {
        public static ILog errorLog = LogManager.GetLogger("ErrorLogger");

        public static build_type[] BuldTypeAll()
        {
            return WcfOperationContext.Current.Context.build_type.ToArray();
        }

        public static bath_unit[] BathunitTypeAll()
        {
            return WcfOperationContext.Current.Context.bath_unit.ToArray();
        }

        public static state_type[] StateTypeAll()
        {
            return WcfOperationContext.Current.Context.state_type.ToArray();
        }
        public static term_type[] TermTypeAll()
        {
            return WcfOperationContext.Current.Context.term_type.ToArray();
        }
        public static lessor_type[] LessorTypeAll()
        {
            return WcfOperationContext.Current.Context.lessor_type.ToArray();
        }
        public static region_type[] RegionTypeAll()
        {
            return WcfOperationContext.Current.Context.region_type.ToArray();
        }
        public static category_type[] CategoryTypeAll()
        {
            return WcfOperationContext.Current.Context.category_type.ToArray();
        }
        public static rent_type[] RentTypeAll()
        {
            return WcfOperationContext.Current.Context.rent_type.ToArray();
        }
        public static payment_type[] PaymentTypeAll()
        {
            return WcfOperationContext.Current.Context.payment_type.ToArray();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using DAL;
using RentalCommon;


namespace RentalCore
{
    public partial class RentalCore : IRentalCore
    {
        public build_type[] BuldTypeAll()
        {
            return TypesManager.BuldTypeAll();
        }

        public bath_unit[] BathunitTypeAll()
        {
            return TypesManager.BathunitTypeAll();
        }

        public state_type[] StateTypeAll()
        {
            return TypesManager.StateTypeAll();
        }
        public term_type[] TermTypeAll()
        {
            return TypesManager.TermTypeAll();
        }
        public lessor_type[] LessorTypeAll()
        {
            return TypesManager.LessorTypeAll();
        }
        public region_type[] RegionTypeAll()
        {
            return TypesManager.RegionTypeAll();
        }
        public category_type[] CategoryTypeAll()
        {
            return TypesManager.CategoryTypeAll();
        }
        public rent_type[] RentTypeAll()
        {
            return TypesManager.RentTypeAll();
        }
        public payment_type[] PaymentTypeAll()
        {
            return TypesManager.PaymentTypeAll();
        }
    }
    
}

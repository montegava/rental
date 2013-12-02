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
    public partial interface IRentalCore
    {
        build_type[] BuldTypeAll();

        bath_unit[] BathunitTypeAll();

        state_type[] StateTypeAll();

        term_type[] TermTypeAll();

        lessor_type[] LessorTypeAll();

        region_type[] RegionTypeAll();

        category_type[] CategoryTypeAll();

        rent_type[] RentTypeAll();

        payment_type[] PaymentTypeAll();

    }


}

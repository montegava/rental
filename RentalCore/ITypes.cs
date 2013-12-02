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
        [OperationContract]
        build_type[] BuldTypeAll();

        [OperationContract]
        bath_unit[] BathunitTypeAll();

        [OperationContract]
        state_type[] StateTypeAll();

        [OperationContract]
        term_type[] TermTypeAll();

        [OperationContract]
        lessor_type[] LessorTypeAll();

        [OperationContract]
        region_type[] RegionTypeAll();

        [OperationContract]
        category_type[] CategoryTypeAll();

        [OperationContract]
        rent_type[] RentTypeAll();

        [OperationContract]
        payment_type[] PaymentTypeAll();

    }


}

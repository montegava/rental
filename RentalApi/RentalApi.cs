using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DAL;

namespace RentalApi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class RentalApi : IRentalApi
    {
        public flat_info GetFlatList(Int32 sortBy, bool orderBy, ref Int32 activePage, Int32 pageSize, out Int32 pageCount, out Int32 totalRowsNumber)
        {
            pageCount = 1;
            totalRowsNumber = 0;
            try
            {
                string error;
                return DAL.FlatManager.GetFlatById(22, out error);

                //                throw new Exception("Eadsasdfasd");
                //              return null;
            }
            catch (Exception ex)
            {

                MyCustomException fault = new MyCustomException();
                fault.MyMessage = "ThrowsTypedCustomFaultException: FaultException<MyCustomException> " + ex.Message;
                throw new FaultException<MyCustomException>(fault, new FaultReason("No reason!"));
            }


        }
    }
}

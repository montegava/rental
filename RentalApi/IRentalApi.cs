using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DAL;

namespace RentalApi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IRentalApi
    {
        [OperationContract]
        flat_info GetFlatList(Int32 sortBy, bool orderBy, ref Int32 activePage, Int32 pageSize, out Int32 pageCount, out Int32 totalRowsNumber);
    }

    [DataContract]
    public class MyCustomException
    {
        [DataMember]
        public string MyMessage { get; set; }
    }
}

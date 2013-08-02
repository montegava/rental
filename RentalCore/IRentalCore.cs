using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RentalCore
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IRentalCore
    {

        [OperationContract(IsOneWay = false)]
        string Upload(System.IO.Stream data);

        [OperationContract(IsOneWay = false)]
        System.IO.Stream DownloadFile(string remotePath);
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RentalApi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IRentalApi
    {
            [OperationContract(IsOneWay = true)]
            void Upload(FileTransferRequest request);
    }

    [DataContract]
    public class MyCustomException
    {
        [DataMember]
        public string MyMessage { get; set; }
    }

    [MessageContract()]
    public class FileTransferRequest
    {
        [MessageHeader(MustUnderstand = true)]
        public string FileName;

        [MessageBodyMember(Order = 1)]
        public System.IO.Stream Data;

    }
}

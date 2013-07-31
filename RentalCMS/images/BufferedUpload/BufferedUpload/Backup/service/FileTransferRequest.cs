using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

namespace service
{
    [MessageContract()]
    public class FileTransferRequest
    {
        [MessageHeader(MustUnderstand = true)]
        public string FileName;

        [MessageBodyMember(Order = 1)]
        public System.IO.Stream Data;

    }
}

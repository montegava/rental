using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace service
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost fileTransferHost = null;
            try
            {
                fileTransferHost = new ServiceHost(typeof(FileTransferService));
                fileTransferHost.Open();

                foreach (ServiceEndpoint endpoint in fileTransferHost.Description.Endpoints)
                {
                    Console.WriteLine("File Transfer service listening on {0}", endpoint.Address.ToString());
                }

                Console.WriteLine("Press a key to close this application");
                Console.ReadLine();
            }
            finally
            {
                if (fileTransferHost != null)
                    ((IDisposable)fileTransferHost).Dispose();
            }
        }
    }
}

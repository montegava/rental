using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (FileStream fs = new FileStream("Test.txt", FileMode.Open))
            {
                FileTransferServiceClient proxy = new FileTransferServiceClient();

                proxy.Upload("Test.txt", fs);
                proxy.Close();

                Console.WriteLine("File sent to the server");
                Console.ReadLine();
            }
        }
    }
}

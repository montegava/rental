using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.IO;
using System.Configuration;

namespace service
{
    [ServiceContract()]
    public interface IFileTransferService
    {
        [OperationContract(IsOneWay = true)]
        void Upload(FileTransferRequest request);
    }

    /// <summary>
    /// A set of methods to upload chunks of a file using MTOM
    /// </summary>
    public class FileTransferService : IFileTransferService
    {
        public FileTransferService()
        {
        }
        
        public void Upload(FileTransferRequest request)
        {
            string fileName = request.FileName;

            if (ConfigurationManager.AppSettings["UploadPath"] == null)
            {
                throw new ApplicationException("Missing upload path");
            }

            string uploadPath = ConfigurationManager.AppSettings["UploadPath"];
            string filePath = Path.Combine(Path.GetFullPath(uploadPath), fileName);

            FileStream fs = null;
            try
            {
                fs = File.Create(filePath);
                byte[] buffer = new byte[1024];
                int read = 0;
                while ((read = request.Data.Read(buffer, 0, buffer.Length)) != 0)
                {
                    fs.Write(buffer, 0, read);
                }
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }

                if (request.Data != null)
                {
                    request.Data.Close();
                    request.Data.Dispose();
                }
            }
        }

    }
}

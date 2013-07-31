using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text;
using System.IO;
using System.Configuration;
using System.Web;

namespace RentalApi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class RentalApi : IRentalApi
    {
        public void Upload(FileTransferRequest request)
        {
            string fileName = request.FileName;

            //if (ConfigurationManager.AppSettings["UploadPath"] == null)
            //{
            //    throw new ApplicationException("Missing upload path");
            //}

            string uploadPath = ".";//ConfigurationManager.AppSettings["UploadPath"];

           
            string filePath = Path.Combine(Path.GetFullPath( "."), fileName);

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

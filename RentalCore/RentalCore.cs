using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using log4net;

namespace RentalCore
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class RentalCore : IRentalCore
    {
        public static ILog errorLog = log4net.LogManager.GetLogger(typeof(RentalCore));



      public void Upload(System.IO.Stream  data)
      {
          string RepositoryDirectory = "Media";
           string fileName = Guid.NewGuid() + ".jpg";
           string filePath = Path.Combine(RepositoryDirectory, String.Format("{0}_{1}\\{2}", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString()), fileName);
          string dir = Path.GetDirectoryName(filePath);

          if (!Directory.Exists(dir))
              Directory.CreateDirectory(dir);

           

            errorLog.Error(fileName);
            
            //if (ConfigurationManager.AppSettings["UploadPath"] == null)
            //{
            //    throw new ApplicationException("Missing upload path");
            //}

            string uploadPath = ".";//ConfigurationManager.AppSettings["UploadPath"];

           
          

        

            FileStream fs = null;
            try
            {
                fs = File.Create(filePath);
                byte[] buffer = new byte[1024];
                int read = 0;
                while ((read = data.Read(buffer, 0, buffer.Length)) != 0)
                {
                    fs.Write(buffer, 0, read);
                }

                errorLog.Error("Uploaded sucsessfully!!!");
            }
            catch(Exception ex)
            {
                errorLog.Error(ex);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }

                if (data != null)
                {
                    data.Close();
                    data.Dispose();
                }
            }
        }
    }
}

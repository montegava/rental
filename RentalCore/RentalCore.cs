using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using log4net;
using System.ServiceModel.Activation;
using System.Web;
using DAL;
using RentalCommon;

namespace RentalCore
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RentalCore : IRentalCore
    {
        public static ILog errorLog = log4net.LogManager.GetLogger(typeof(RentalCore));

        public void Upload(Stream data, string fileName)
        {

            var result = new StringBuilder();
            result.Append("Upload");
            result.AppendLine();
            try
            {

                var RepositoryDirectory = @"d:\hst\amiravrn-ru_bd4c5401\http\Media";
                string filePath = Path.Combine(RepositoryDirectory, String.Format("{0}", fileName));
                string dir = Path.GetDirectoryName(filePath);

                result.Append("dir = ");
                result.Append(dir);
                result.AppendLine();

                if (!Directory.Exists(dir))
                {
                    result.Append("Dir not exist!");
                    result.AppendLine();
                    Directory.CreateDirectory(dir);
                }



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
                    result.Append("sucsessfully");
                    result.AppendLine();

                    errorLog.Error("Uploaded sucsessfully!!!");
                }
                catch (Exception ex)
                {
                    result.Append(ex.Message);
                    result.AppendLine();
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
            catch (Exception ex)
            {
                result.Append(ex.Message);
                result.AppendLine();
                errorLog.Error(ex);
            }
        }

        public Stream DownloadFile(string remotePath)
        {
            Stream result = null;
            var RepositoryDirectory = @"d:\hst\amiravrn-ru_bd4c5401\http\";
            string filePath = Path.Combine(RepositoryDirectory, remotePath);
            if (File.Exists(filePath))
                result = new FileStream(filePath, FileMode.Open);
            return result;
        }

        public void FlatList(List<Filter> filterBy, DateTime startDate, DateTime endDate, Int32 sortBy, bool orderBy, ref Int32 activePage, Int32 pageSize, out List<flat_info> flats, out Int32 pageCount, out Int32 totalRowsNumber)
        {
            FlatManager.FlatList(filterBy, startDate, endDate, sortBy, orderBy, ref activePage, pageSize, out flats, out pageCount, out totalRowsNumber);
        }

        public flat_info FlatByUrl(string url)
        {
            return FlatManager.FlatByUrl(url);
        }

        public flat_info FlatById(int id)
        {
            return FlatManager.FlatById(id);
        }

        public int FlatAdd(flat_info flat)
        {
            return FlatManager.FlatAdd(flat);
        }

        public void FlatUpdate(flat_info flat)
        {
            FlatManager.FlatUpdate(flat);
        }

        public void FlatDelete(int id)
        {
            FlatManager.FlatDelete(id);
        }

        public void ImageUpdate(image_list[] images, int flatId)
        {
            ImageManager.ImageUpdate(images, flatId);
        }

        public image_list[] ImagesByFlatId(int flatId)
        {
            return ImageManager.ImagesByFlatId(flatId).ToArray();
        }

        public black_list[] BlackListAll()
        {
            return BlackListManager.BlackListAll();
        }

        public black_list BlackListById(int id)
        {
            return BlackListManager.BlackListById(id);
        }

        public void BlackListDelete(int id)
        {
            BlackListManager.BlackListDelete(id);
        }

        public int BlackListAdd(black_list blackList)
        {
            return BlackListManager.BlackListAdd(blackList);
        }

    }
}

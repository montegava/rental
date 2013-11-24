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

        public static string RepositoryDirectory = @"d:\hst\amiravrn-ru_bd4c5401\http\Media";

        public void FileUpload(string fileName, byte[] data)
        {
                string filePath = Path.Combine(RepositoryDirectory, fileName);
                string dir = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                File.WriteAllBytes(filePath, data);
        }

        public byte[] FileDownload(string remotePath)
        {
            string filePath = Path.Combine(RepositoryDirectory, remotePath);
            if (File.Exists(filePath))
                return File.ReadAllBytes(filePath);
            return new byte[]{};
        }

        public void FlatList(List<Filter> filterBy, DateTime startDate, DateTime endDate, Int32 sortBy, bool orderBy, ref Int32 activePage, Int32 pageSize, out List<flat_info> flats, out Int32 pageCount, out Int32 totalRowsNumber)
        {
            FlatManager.FlatList(filterBy, startDate, endDate, sortBy, orderBy, ref activePage, pageSize, out flats, out pageCount, out totalRowsNumber);
        }

        public SearchResult<flat_info> FlatSearch(SearchQuery query)
        {
            return FlatManager.FlatSearch(query);
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

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
    public partial class RentalCore : IRentalCore
    {
        public static ILog errorLog = log4net.LogManager.GetLogger(typeof(RentalCore));

        public static string RepositoryDirectory = @"d:\hst\amiravrn-ru_bd4c5401\http\Media";

        public bool Login(string login, string password)
        {
          return  UserManagment.Login(login, password);
        }



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

        public SearchResult<view_flat_info> FlatSearch(SearchQuery query)
        {
            return FlatManager.FlatSearch(query);
        }

        public flat_info FlatByUrl(string url)
        {
            return FlatManager.FlatByUrl(url);
        }

        public flat_info FlatById(int id)
        {
            var flat  = FlatManager.FlatById(id);
            return flat;
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

        public build_type[] BuldTypeAll()
        {
            return TypesManager.BuldTypeAll();
        }

        public bath_unit[] BathunitTypeAll()
        {
            return TypesManager.BathunitTypeAll();
        }

        public state_type[] StateTypeAll()
        {
            return TypesManager.StateTypeAll();
        }
        public term_type[] TermTypeAll()
        {
            return TypesManager.TermTypeAll();
        }
        public lessor_type[] LessorTypeAll()
        {
            return TypesManager.LessorTypeAll();
        }
        public region_type[] RegionTypeAll()
        {
            return TypesManager.RegionTypeAll();
        }
        public category_type[] CategoryTypeAll()
        {
            return TypesManager.CategoryTypeAll();
        }
        public rent_type[] RentTypeAll()
        {
            return TypesManager.RentTypeAll();
        }
        public payment_type[] PaymentTypeAll()
        {
            return TypesManager.PaymentTypeAll();
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using DAL;
using RentalCommon;

namespace RentalCore
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public partial interface IRentalCore
    {

        [OperationContract]
        bool Login(string login, string password);

        [OperationContract(IsOneWay = false)]
        void FileUpload(string fileName, byte[] data);

        [OperationContract(IsOneWay = false)]
        byte[] FileDownload(string remotePath);

        [OperationContract]
        SearchResult<view_flat_info> FlatSearch(SearchQuery query);

        [OperationContract]
        flat_info FlatByUrl(string url);

        [OperationContract]
        flat_info FlatById(int id);

        [OperationContract]
        int FlatAdd(flat_info flat);

        [OperationContract]
        void FlatUpdate(flat_info flat);

        [OperationContract]
        void FlatDelete(int id);

        [OperationContract]
        void ImageUpdate(image_list[] images, int flatId);

        [OperationContract]
        image_list[] ImagesByFlatId(int flatId);

        [OperationContract]
        black_list[] BlackListAll();

        [OperationContract]
        black_list BlackListById(int id);

        [OperationContract]
        void BlackListDelete(int id);

        [OperationContract]
        int BlackListAdd(black_list blackList);


        [OperationContract]
        build_type[] BuldTypeAll();

        [OperationContract]
        bath_unit[] BathunitTypeAll();

        [OperationContract]
        state_type[] StateTypeAll();

        [OperationContract]
        term_type[] TermTypeAll();

        [OperationContract]
        lessor_type[] LessorTypeAll();

        [OperationContract]
        region_type[] RegionTypeAll();

        [OperationContract]
        category_type[] CategoryTypeAll();

        [OperationContract]
        rent_type[] RentTypeAll();

        [OperationContract]
        payment_type[] PaymentTypeAll();
    }




}

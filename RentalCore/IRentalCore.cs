﻿using System;
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
    public interface IRentalCore
    {

        [OperationContract(IsOneWay = false)]
        string Upload(System.IO.Stream data);

        [OperationContract(IsOneWay = false)]
        Stream DownloadFile(string remotePath);

        [OperationContract]
        void FlatList(List<Filter> filters, DateTime startDate, DateTime endDate, Int32 sortBy, bool orderBy, ref Int32 activePage, Int32 pageSize, out List<flat_info> flats, out Int32 pageCount, out Int32 totalRowsNumber);


        [OperationContract]
        flat_info FlatByUrl(string url);

        [OperationContract]
        flat_info FlatById(int id);

        [OperationContract]
        int FlatAdd(flat_info flat);

        [OperationContract]
        void FlatUpdate(flat_info flat);

        [OperationContract]
        void ImageUpdate(image_list[] images, int flatId);
    
    }


}

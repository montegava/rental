﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18052
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RentalCMS.RentalCore {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RentalCore.IRentalCore")]
    public interface IRentalCore {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRentalCore/Upload", ReplyAction="http://tempuri.org/IRentalCore/UploadResponse")]
        string Upload(System.IO.Stream data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRentalCore/DownloadFile", ReplyAction="http://tempuri.org/IRentalCore/DownloadFileResponse")]
        System.IO.Stream DownloadFile(string remotePath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRentalCore/FlatList", ReplyAction="http://tempuri.org/IRentalCore/FlatListResponse")]
        void FlatList(RentalCommon.Filter[] filters, System.DateTime startDate, System.DateTime endDate, int sortBy, bool orderBy, ref int activePage, out DAL.flat_info[] flats, out int pageCount, out int totalRowsNumber, int pageSize);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRentalCore/FlatByUrl", ReplyAction="http://tempuri.org/IRentalCore/FlatByUrlResponse")]
        DAL.flat_info FlatByUrl(string url);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRentalCore/FlatById", ReplyAction="http://tempuri.org/IRentalCore/FlatByIdResponse")]
        DAL.flat_info FlatById(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRentalCore/FlatAdd", ReplyAction="http://tempuri.org/IRentalCore/FlatAddResponse")]
        int FlatAdd(DAL.flat_info flat);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRentalCore/FlatUpdate", ReplyAction="http://tempuri.org/IRentalCore/FlatUpdateResponse")]
        void FlatUpdate(DAL.flat_info flat);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRentalCore/ImageUpdate", ReplyAction="http://tempuri.org/IRentalCore/ImageUpdateResponse")]
        void ImageUpdate(DAL.image_list[] images, int flatId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRentalCore/BlackListAll", ReplyAction="http://tempuri.org/IRentalCore/BlackListAllResponse")]
        DAL.black_list[] BlackListAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRentalCore/BlackListById", ReplyAction="http://tempuri.org/IRentalCore/BlackListByIdResponse")]
        DAL.black_list BlackListById(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRentalCore/BlackListDelete", ReplyAction="http://tempuri.org/IRentalCore/BlackListDeleteResponse")]
        void BlackListDelete(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRentalCore/BlackListAdd", ReplyAction="http://tempuri.org/IRentalCore/BlackListAddResponse")]
        int BlackListAdd(DAL.black_list blackList);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRentalCoreChannel : RentalCMS.RentalCore.IRentalCore, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RentalCoreClient : System.ServiceModel.ClientBase<RentalCMS.RentalCore.IRentalCore>, RentalCMS.RentalCore.IRentalCore {
        
        public RentalCoreClient() {
        }
        
        public RentalCoreClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RentalCoreClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RentalCoreClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RentalCoreClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Upload(System.IO.Stream data) {
            return base.Channel.Upload(data);
        }
        
        public System.IO.Stream DownloadFile(string remotePath) {
            return base.Channel.DownloadFile(remotePath);
        }
        
        public void FlatList(RentalCommon.Filter[] filters, System.DateTime startDate, System.DateTime endDate, int sortBy, bool orderBy, ref int activePage, out DAL.flat_info[] flats, out int pageCount, out int totalRowsNumber, int pageSize) {
            base.Channel.FlatList(filters, startDate, endDate, sortBy, orderBy, ref activePage, out flats, out pageCount, out totalRowsNumber, pageSize);
        }
        
        public DAL.flat_info FlatByUrl(string url) {
            return base.Channel.FlatByUrl(url);
        }
        
        public DAL.flat_info FlatById(int id) {
            return base.Channel.FlatById(id);
        }
        
        public int FlatAdd(DAL.flat_info flat) {
            return base.Channel.FlatAdd(flat);
        }
        
        public void FlatUpdate(DAL.flat_info flat) {
            base.Channel.FlatUpdate(flat);
        }
        
        public void ImageUpdate(DAL.image_list[] images, int flatId) {
            base.Channel.ImageUpdate(images, flatId);
        }
        
        public DAL.black_list[] BlackListAll() {
            return base.Channel.BlackListAll();
        }
        
        public DAL.black_list BlackListById(int id) {
            return base.Channel.BlackListById(id);
        }
        
        public void BlackListDelete(int id) {
            base.Channel.BlackListDelete(id);
        }
        
        public int BlackListAdd(DAL.black_list blackList) {
            return base.Channel.BlackListAdd(blackList);
        }
    }
}

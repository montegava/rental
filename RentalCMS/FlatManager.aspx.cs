using System;
using System.Collections.Generic;
using System.Linq;
using DAL;

namespace RentalCMS
{
    public partial class FlatManager : System.Web.UI.Page
    {

        private string AssetBasePath { get { return "Media/"; } }

        private int SelectedId { get { return UIConvert.ToInt32(Request["Id"], 0); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
                SetDefaultData();
        }

        protected void CancelClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Flats.aspx");
        }

        private void SetDefaultData()
        {
            if (SelectedId > 0)
            {
                var flat = DAL.FlatManager.FlatById(SelectedId);
                if (flat != null)
                {
                    tblAddress.Text = flat.ADDRESS;
                    lblRoomCount.Text = Convert.ToString(flat.ROOM_COUNT);
                    lblPhone.Text = "+7 (903) 652-90-28";
                    tbContent.Text = flat.COMMENT;

                    _fyImage.PageActive = "0";
                    _fyImage.NoImgUrl = "/images/no_image.png";
                    _fyImage.ImageList = this.GetItemFromSelectedImages(ImageManager.ImagesByFlatId(flat.ID), AssetBasePath);
                }
            }
        }

        public List<PageControls.ImageInfo> GetItemFromSelectedImages(IEnumerable<image_list> items, string basePath = null)
        {
            if (items == null || !items.Any()) 
                return new List<PageControls.ImageInfo>();
            return items.Select(e => new PageControls.ImageInfo
                                    {
                                        Name = System.IO.Path.GetFileName(e.IMAGE_PATH),
                                        Path = (basePath ?? string.Empty) + e.IMAGE_PATH
                                    }).ToList();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;


namespace RentalCMS
{
    public partial class FlatManager : System.Web.UI.Page
    {


        int SelectedId
        {
            get
            {
                return UIConvert.ToInt32(Request["Id"], 0);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                SetDefaultData();
            }
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
                }


                


                //var img = FlatImageManager.GetFlatImagesByFlatId(SelectedId);

                // -- fill smart image control, with all images
                //_fyImage.VisibleName = GetTextByCulture("TM_Images");
                _fyImage.PageActive = "0";
                _fyImage.NoImgUrl = "/images/no_image.png";

                var images = ImageManager.ImagesByFlatId(flat.ID);

                _fyImage.ImageList = this.GetItemFromSelectedImages(images, GetAssetBasePath());
            }

        }

        public string GetAssetBasePath()
        {
            return "Media/";
        }

        public List<PageControls.ImageInfo> GetItemFromSelectedImages(IEnumerable<image_list> items, string basePath = null)
        {
            // -- prepare url string array from selected item's
            if (items == null || !items.Any()) return new List<PageControls.ImageInfo>();
            return items.Select(e => new PageControls.ImageInfo
                                    {
                                        Name = System.IO.Path.GetFileName(e.IMAGE_PATH),
                                        Path = (basePath ?? string.Empty) + e.IMAGE_PATH
                                    }).ToList();
        }

    }
}
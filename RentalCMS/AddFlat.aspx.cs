using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Valentica.Libraries.Utilities;

namespace RentalCMS
{
    public partial class AddFlat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ddlCategory.DataBind();
            this.ddlRegion.DataBind();
            this.ddlType.DataBind();
            this.ddlFloor.DataBind();
            this.ddlHouseType.DataBind();
            this.ddlRentType.DataBind();
            this.ddlRoomCount.DataBind();
        }

        protected void CancelClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Flats.aspx");
        }

        protected void Upload_Click(object sender, EventArgs e)
        {
            var RepositoryDirectory = @"d:\hst\amiravrn-ru_bd4c5401\http\Media";


            HttpFileCollection hfc = Request.Files;
            for (int i = 0; i < hfc.Count; i++)
            {
                HttpPostedFile hpf = hfc[i];
                if (hpf.ContentLength > 0)
                {
                    var imgName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(hpf.FileName);

                    hpf.SaveAs(RepositoryDirectory + "\\" + imgName);
                    //lblMsg.Text += " <br/> <b>File: </b>" + hpf.FileName +
                    //  " <b>Size:</b> " + hpf.ContentLength + " <b>Type:</b> " +
                    //  hpf.ContentType + " Uploaded Successfully!";
                }
            }
          


            //Uploader uploader = new Uploader();



            //uploader.UploadPath = RepositoryDirectory;// Server.MapPath("~\\Content\\Uploads");
            //uploader.IsLowerName = false;
            //uploader.IsEncryptName = false;
            //uploader.IsOverwrite = false;
            //uploader.Prefix = "Hello_";
            //uploader.Suffix = "_Boom";
            ////uploader.MaxSize

            //uploader.AllowedExtensions.Add(".jpg");
            //uploader.AllowedExtensions.Add(".jpeg");
            //uploader.AllowedExtensions.Add(".gif");
            //uploader.AllowedExtensions.Add(".png");

            //bool success = uploader.DoUpload("FileUpload");

            //if (success)
            //{
            //    Response.Write("Success");
            //}
            //else
            //{
            //    Response.Write(uploader.UploadError.Message);
            //    Response.Write("Failed");
            //}
        }

    }
}
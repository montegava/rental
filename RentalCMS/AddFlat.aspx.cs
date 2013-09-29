using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

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
            this.ddlPayment.DataBind();
        }

        protected void CancelClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Flats.aspx");
        }

        protected void Upload_Click(object sender, EventArgs e)
        {
            var flat = new flat_info()
            {
                ID = -1,
                DATA = DateTime.Now,
                NAME = tbName.Text,
                EMAIL = tbEmail.Text,
                PHONE = tbPhone.Text,
                REGION = EnumConvertor.Convert((Region)ddlRegion.SelectedEnum),
                CATEGORY = EnumConvertor.Convert((Category)ddlCategory.SelectedEnum),
                TYPE = EnumConvertor.Convert((DAL.Type)ddlCategory.SelectedEnum),
                ROOM_COUNT = ddlRoomCount.SelectedIntValue,
                FLOOR = ddlFloor.SelectedIntValue,
                BUILD = EnumConvertor.Convert((DAL.HouseType)ddlHouseType.SelectedEnum),
                TERM = EnumConvertor.Convert((DAL.RentType)ddlRentType.SelectedEnum),
                //squery = 
                ADDRESS = tbAdres.Text,
                CONTENT = tbDescription.Text,
                PRICE = tbPrice.Text,
                PAYMENT = EnumConvertor.Convert((DAL.Payment)ddlPayment.SelectedEnum),
            };


            var flatId = DAL.FlatManager.FlatAdd(flat);


            var RepositoryDirectory = @"d:\hst\amiravrn-ru_bd4c5401\http\";

            var images = new List<image_list>();

            HttpFileCollection hfc = Request.Files;
            for (int i = 0; i < hfc.Count; i++)
            {
                HttpPostedFile hpf = hfc[i];
                if (hpf.ContentLength > 0)
                {
                    var imgName = "Media\\"+Guid.NewGuid().ToString() + System.IO.Path.GetExtension(hpf.FileName);
                    images.Add(new image_list() { ID = -1, IMAGE_PATH = imgName, FLAT_ID = flatId });
                    hpf.SaveAs(RepositoryDirectory + "\\" + imgName);
                    //lblMsg.Text += " <br/> <b>File: </b>" + hpf.FileName +
                    //  " <b>Size:</b> " + hpf.ContentLength + " <b>Type:</b> " +
                    //  hpf.ContentType + " Uploaded Successfully!";
                }
            }

            DAL.ImageManager.ImageUpdate(images.ToArray(), flatId);

            Response.Redirect("~/FlatManager.aspx?id="+ flatId);
        
        }

    }
}
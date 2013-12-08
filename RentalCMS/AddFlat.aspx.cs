using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using RentalCMS.RentalCore;

namespace RentalCMS
{
    public partial class AddFlat : System.Web.UI.Page
    {

        public static RentalCoreClient core = new RentalCoreClient();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind(ddlRegion, core.RegionTypeAll());
                Bind(ddlCategory, core.CategoryTypeAll());
                Bind(ddlType, core.RentTypeAll());
                Bind(ddlHouseType, core.BuldTypeAll());
                Bind(ddlTermType, core.TermTypeAll());
                Bind(ddlPayment, core.PaymentTypeAll());
                this.ddlFloor.DataBind();
                this.ddlRoomCount.DataBind();
            }
         

          
        }

        private void Bind(DropDownList ddl, object data)
        {
            ddl.DataSource = data;
            ddl.DataTextField = "name";
            ddl.DataValueField = "id";
            ddl.DataBind();
        }

        private int? Selected(DropDownList ddl)
        {
            return ddl.SelectedIndex != -1 && Convert.ToInt32(ddl.SelectedValue) > 0 ? (int?)Convert.ToInt32(ddl.SelectedValue) : null;
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
                ROOM_COUNT = Convert.ToInt32( ddlRoomCount.SelectedValue),
                FLOOR =  Convert.ToInt32(ddlFloor.SelectedIntValue),
                ADDRESS = tbAdres.Text,
                COMMENT = System.Text.RegularExpressions.Regex.Replace(tbDescription.Text, @"/\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/", "+7 (903) 652-90-28"),
                PRICE = tbPrice.Text,

                payment_type_id = Selected(ddlPayment),
                region_type_id = Selected(ddlRegion),
                category_type_id = Selected(ddlCategory),
                rent_type_id = Selected(ddlType),
                buld_type_id = Selected(ddlHouseType),
                term_type_id = Selected(ddlTermType),
            };


            var flatId = DAL.FlatManager.FlatAdd(flat);


            var RepositoryDirectory = @"d:\hst\amiravrn-ru_bd4c5401\http\Media";

            var images = new List<image_list>();

            HttpFileCollection hfc = Request.Files;
            for (int i = 0; i < hfc.Count; i++)
            {
                HttpPostedFile hpf = hfc[i];
                if (hpf.ContentLength > 0)
                {
                    var imgName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(hpf.FileName);
                    images.Add(new image_list() { ID = -1, IMAGE_PATH = imgName, FLAT_ID = flatId });
                    hpf.SaveAs(RepositoryDirectory + "\\" + imgName);
                }
            }

            DAL.ImageManager.ImageUpdate(images.ToArray(), flatId);

            Response.Redirect("~/FlatManager.aspx?id="+ flatId);
        
        }

    }
}
using System;
using System.Globalization;
using System.Web.UI.WebControls;

namespace RentalCMS.Controls
{
    public class EnumDropDownList : DropDownList
    {
        private Type type;
        private string typeName;


        public string Type
        {
            get { return this.type.Name; }
            set
            {
                this.typeName = value;
                this.type = System.Type.GetType(value);
            }
        }

        public override void DataBind()
        {
            if (this.type == null) throw new InvalidOperationException(string.Format("Type property was not set or type {0} is not found", this.typeName));
            this.Items.Clear();
            foreach (var value in Enum.GetValues(this.type))
            {
                var name = Enum.GetName(this.type, value);


                this.Items.Add(new ListItem(name, ((int)value).ToString(CultureInfo.InvariantCulture)));
            }
            base.DataBind();
        }

        public object SelectedEnum
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(SelectedValue))
                    return Enum.Parse(this.type, this.SelectedValue);
                else
                {
                    return 0;
                }
            }
            set { SelectedValue = ((int)value).ToString(CultureInfo.InvariantCulture); }
        }
    }
}
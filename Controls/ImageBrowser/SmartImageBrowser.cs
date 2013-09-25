using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Web.Script.Serialization;

namespace SmartControls
{
    public class SmartImageBrowser : BaseControl, IPostBackDataHandler
    {
        #region private fields
        // -- visible name of the control (label)
        private string visibleName = "";

        // -- image path string
        private string imgPath;

        private string imgWidth;
        private string imgHeight;

        // -- readOnly mode
        private bool isReadOnly;
        #endregion

        #region public getters/setters
        public string VisibleName
        {
            set { visibleName = value; ViewState["VisibleName"] = value; }
            get { return (string)ViewState["VisibleName"]; }
        }

        public string ImgPath
        {
            set { imgPath = value; ViewState["ImgPath"] = value; }
            get { return (string)ViewState["ImgPath"]; }
        }
        
        public string ImgWidth
        {
            set { imgWidth = value; ViewState["ImgWidth"] = value; }
            get { return (string)ViewState["ImgWidth"]; }
        }

        public string ImgHeight
        {
            set { imgHeight = value; ViewState["ImgHeight"] = value; }
            get { return (string)ViewState["ImgHeight"]; }
        }

        public bool IsReadOnly
        {
            set { isReadOnly = value; ViewState["IsReadOnly"] = value == true ? "1" : "0"; }
            get { return (string)ViewState["IsReadOnly"] == "1" ? true : false; }
        }
        #endregion

        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Div; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            // Add stylesheet to parent page
            string cssUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "SmartControls.SmartImageBrowser.SmartImageBrowser.css");
            HtmlLink cssLink = new HtmlLink();
            cssLink.Href = cssUrl;
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("type", "text/css");
            this.Page.Header.Controls.Add(cssLink);

            // Add class name
            this.CssClass = "SmartImageBrowser";

            // Add Javascript include
            string scriptUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "SmartControls.SmartImageBrowser.SmartImageBrowser.jquery.js");
            Page.ClientScript.RegisterClientScriptInclude("SmartImageBrowser", scriptUrl);

            // Call the jQuery script (for each added webcontrol in the page)
            StringBuilder csText = new StringBuilder();
            csText.Append("<script type=\"text/javascript\"> jQuery(function ($) {");
            csText.AppendFormat("$(\"#{0}\").smartImageBrowserPlugin( {{ isReadOnly : {1} }} );", this.ClientID, IsReadOnly ? 1 : 0);
            csText.Append("}); </script>");
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.ClientID + "Script", csText.ToString());

            base.OnPreRender(e);
        }

        protected override void RenderContents(System.Web.UI.HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            // -- Render the control's label
            RenderLabel(writer);
            // -- Render file select input field
            RenderImageSelect(writer);
            // -- Render div containing selected image
            RenderImageDiv(writer);
            // -- Render Hidden Field containing JSON formatted data needed by jQuery
            RenderHiddenField(writer);

            // -- clear left floating
            writer.Write("<br class=\"clearLeft\"/>");

            writer.RenderEndTag();

            base.RenderContents(writer);
        }

        #region postback data
        // -- See: http://msdn.microsoft.com/en-us/library/aa720415(v=vs.71).aspx
        public event EventHandler ListOfAddedItemsChanged;

        public virtual bool LoadPostData(string postDataKey,
                          NameValueCollection values)
        {
            if (ImgPath != null)
            {
                // -- get the posted json string
                string postedJsonString = values[postDataKey];
                ImgPath = postedJsonString;
                return true;
            }
            return false;
        }

        public virtual void RaisePostDataChangedEvent()
        {
            OnListOfAddedItemsChanged(EventArgs.Empty);
        }

        protected virtual void OnListOfAddedItemsChanged(EventArgs e)
        {
            if (ListOfAddedItemsChanged != null)
                ListOfAddedItemsChanged(this, e);
        }
        #endregion

        #region private helper methods
        private void RenderLabel(HtmlTextWriter writer)
        {            
            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.Write(VisibleName); // -- content of the span
            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        private void RenderImageSelect(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "file");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "file_" + VisibleName);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, "file" + VisibleName);
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }

        private void RenderImageDiv(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "imgpath");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if (ImgPath != null && ImgPath.Length > 0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Height, ImgHeight == null ? "100" : ImgHeight);
                writer.AddAttribute(HtmlTextWriterAttribute.Width, ImgWidth == null ? "100" : ImgWidth);
                writer.AddAttribute(HtmlTextWriterAttribute.Src, ImgPath);
                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                writer.RenderEndTag();
            }
            writer.RenderEndTag();
        }

        private void RenderHiddenField(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
            writer.AddAttribute(HtmlTextWriterAttribute.Value, ImgPath);
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }
        #endregion
    }
}

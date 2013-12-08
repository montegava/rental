using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Web.Script.Serialization;

namespace PageControls
{
    public class SmartImages : BaseControl, IPostBackDataHandler
    {
        #region private fields
        /// <summary>
        /// this is use to define the name of controls
        /// </summary>
        private string visibleName = "";

        /// <summary>
        /// height of the images
        /// </summary>
        private string height = "";

        /// <summary>
        /// width of the images
        /// </summary>
        private string width = "";

        /// <summary>
        /// images contained in this control
        /// </summary>
        private List<ImageInfo> imageList = null;

        /// <summary>
        /// url of the image if there is no image in the imageList
        /// </summary>
        private string noImgUrl = "";

        /// <summary>
        /// this variable holds the active page number between postbacks
        /// </summary>
        private string pageActive = "0";
        #endregion

        #region public getters/setters
        public string VisibleName
        {
            set { visibleName = value; ViewState["VisibleName"] = value; }
            get { return (string)ViewState["VisibleName"]; }
        }

        [DefaultValue("")]
        public string ImgHeight
        {
            set { height = value; ViewState["ImgHeight"] = value; }
            get { return (string)ViewState["ImgHeight"]; }
        }
        
        [DefaultValue("")]
        public string ImgWidth
        {
            set { width = value; ViewState["ImgWidth"] = value; }
            get { return (string)ViewState["ImgWidth"]; }
        }

        public List<ImageInfo> ImageList
        {
            set { imageList = value; ViewState["ImageList"] = imageList; }
            get { return (List<ImageInfo>)ViewState["ImageList"]; }
        }

        public string NoImgUrl
        {
            set { noImgUrl = value; ViewState["NoImgUrl"] = value; }
            get { return (string)ViewState["NoImgUrl"]; }
        }

        public string PageActive
        {
            set { pageActive = value; ViewState["PageActive"] = value; }
            get { return (string)ViewState["PageActive"]; }
        }
        #endregion

        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Div; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            // Add stylesheet to parent page
            string cssUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "PageControls.SmartImages.css");
            HtmlLink cssLink = new HtmlLink();
            cssLink.Href = cssUrl;
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("type", "text/css");
            this.Page.Header.Controls.Add(cssLink);

            // Add class name
            this.CssClass = "SmartImages";

            if (ImageList != null && ImageList.Any())
            {
                // Add Javascript include

                string scriptUrl1 = Page.ClientScript.GetWebResourceUrl(this.GetType(), "PageControls.jquery.pajinate.js");
                Page.ClientScript.RegisterClientScriptInclude("SmartImages1", scriptUrl1);

                string scriptUrl2 = Page.ClientScript.GetWebResourceUrl(this.GetType(), "PageControls.SmartImages.jquery.js");
                Page.ClientScript.RegisterClientScriptInclude("SmartImages2", scriptUrl2);

                // Call the jQuery script (for each added webcontrol in the page)
                StringBuilder csText = new StringBuilder();
                csText.Append("<script type=\"text/javascript\"> jQuery(function ($) {");
                csText.AppendFormat("$(\"#{0}\").pajinate( {{ items_per_page: {1}, num_page_links_to_display : {2}, start_page : {3} }} );", this.ClientID + "js", 1, 5, pageActive);
                csText.AppendFormat("$(\"#{0}\").smartImagesPlugin();", this.ClientID);
                csText.Append("}); </script>");
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.ClientID + "Script", csText.ToString());
            }

            base.OnPreRender(e);
        }

        protected override void RenderContents(System.Web.UI.HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "js");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "container"); // needed by the pajinate plugin
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            // -- Render the control's label
            RenderLabel(writer);
            if (ImageList != null && ImageList.Any())
            {
                // -- Render images
                RenderImages(writer);
                // -- Render navigation div
                RenderNav(writer);
            }
            else
            {
                // -- render a 'no' image available
                RenderNoImage(writer);
            }
            // -- Render hidden field for postback data
            RenderHiddenField(writer);

            writer.RenderEndTag();

            base.RenderContents(writer);
        }

        #region postback data
        // -- See: http://msdn.microsoft.com/en-us/library/aa720415(v=vs.71).aspx
        public event EventHandler ImageChanged;

        public virtual bool LoadPostData(string postDataKey,
                          NameValueCollection values)
        {
            pageActive = values[postDataKey];
            return false;
        }

        public virtual void RaisePostDataChangedEvent()
        {
            OnImageChanged(EventArgs.Empty);
        }

        protected virtual void OnImageChanged(EventArgs e)
        {
            if (ImageChanged != null)
                ImageChanged(this, e);
        }
        #endregion

        #region private helper methods
        private void RenderLabel(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "above");
            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "above");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.Write(VisibleName); // -- content of the span
            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        private void RenderNav(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "page_navigation"); // needed by pajinate plugin
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderEndTag();
        }

        private void RenderImages(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "content"); // needed by pajinate plugin
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);

            foreach (var img in ImageList)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "text-align: center;");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                writer.AddAttribute(HtmlTextWriterAttribute.Height, ImgHeight);
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "max-width: " + ImgWidth + "px;");
                //writer.AddAttribute(HtmlTextWriterAttribute.Width, ImgWidth);
                writer.AddAttribute(HtmlTextWriterAttribute.Src, Page.ResolveUrl(img.Path));
                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                writer.RenderEndTag();

                //Add signature
                //writer.RenderBeginTag(HtmlTextWriterTag.Br);
                //writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "width : 100%;");
                writer.RenderBeginTag(HtmlTextWriterTag.Label);
                writer.Write(img.Name);
                writer.RenderEndTag();

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "clearBoth");
                writer.RenderBeginTag(HtmlTextWriterTag.Br);
                writer.RenderEndTag();
                
                writer.RenderEndTag();

                writer.RenderEndTag();
            }

            writer.RenderEndTag();
        }

        private void RenderNoImage(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Height, ImgHeight);
            writer.AddAttribute(HtmlTextWriterAttribute.Width, ImgWidth);
            writer.AddAttribute(HtmlTextWriterAttribute.Src, NoImgUrl);
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();
        }

        private void RenderHiddenField(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
            writer.AddAttribute(HtmlTextWriterAttribute.Value, pageActive);
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }

        #endregion
    }
}


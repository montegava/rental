using System;
using System.Web.UI;
using System.Collections.Specialized;

namespace PageControls
{
    /// <summary>
    /// This class is use to generate a custom paging control
    /// </summary>
    public class SmartPagingLink : BaseControl, IPostBackDataHandler, IPostBackEventHandler
    {
        // event when we change paging
        public event EventHandler Paging;

        // selected page field
        private int pageSelected = 0;

        #region "constants"
        // default value of number of link show ex: 1 2 3 4 5
        private const int NR_OF_LINKS = 5;

        // numer of links on left/right part from selected page middle of NR_OF_LINKS
        private const int NR_OF_SHOW = 2;
        #endregion

        #region "propierties"
        /// <summary>
        /// set the page count nr.
        /// </summary>
        public int PageCount
        {
            get
            {
                try
                {
                    return System.Convert.ToInt32(ViewState["pageCount"]);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            set { ViewState["pageCount"] = value.ToString(); }
        }

        /// <summary>
        /// set active page
        /// </summary>
        public int PageActive
        {
            get
            {
                try
                {
                    return System.Convert.ToInt32(ViewState["pageActive"]);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            set { ViewState["pageActive"] = value.ToString(); }
        }

        /// <summary>
        ///  set the css for active page
        /// </summary>
        public string CssActivePage
        {
            get
            {
                if (ViewState["cssActivePage"] != null)
                    return ViewState["cssActivePage"].ToString();
                else
                    return string.Empty;

            }
            set { ViewState["cssActivePage"] = value; }
        }

        /// <summary>
        /// return selected page
        /// </summary>
        /// <remarks>this is read only</remarks>
        public int PageSelected
        {
            get
            {
                try
                {
                    return pageSelected;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// images use for First, Back, Next, Last - navigation
        /// </summary>
        public string ImageFirstPage
        {
            get
            {
                if (ViewState["FirstPage"] != null)
                    return ViewState["FirstPage"].ToString();
                else
                    return string.Empty;
            }
            set { ViewState["FirstPage"] = value; }
        }

        public string ImageBackPage
        {
            get
            {
                if (ViewState["BackPage"] != null)
                    return ViewState["BackPage"].ToString();
                else
                    return string.Empty;
            }
            set { ViewState["BackPage"] = value; }
        }

        public string ImageNextPage
        {
            get
            {
                if (ViewState["NextPage"] != null)
                    return ViewState["NextPage"].ToString();
                else
                    return string.Empty;
            }
            set { ViewState["NextPage"] = value; }
        }

        public string ImageLastPage
        {
            get
            {
                if (ViewState["LastPage"] != null)
                    return ViewState["LastPage"].ToString();
                else
                    return string.Empty;
            }
            set { ViewState["LastPage"] = value; }
        }
        #endregion

        /// <summary>
        /// implemnent LoadPostData function
        /// </summary>
        /// <param name="postDataKey"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool LoadPostData(String postDataKey, NameValueCollection values)
        {
            return false;
        }

        /// <summary>
        /// implement RaisePostDataChangedEvent function
        /// </summary>
        public void RaisePostDataChangedEvent()
        {
        }

        /// <summary>
        /// implement RaisePostBackEvent function
        /// </summary>
        /// <param name="eventArgument"></param>
        public void RaisePostBackEvent(string eventArgument)
        {
            pageSelected = System.Convert.ToInt32(eventArgument);
            OnPaging(new EventArgs());
        }

        /// <summary>
        /// Invoke delegates registered with the Paging event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPaging(EventArgs e)
        {
            if (Paging != null)
            {
                Paging(this, e);
            }
        }

        /// <summary>
        /// rewrite base function OnPreRender
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
        }

        /// <summary> 
        /// Render this control to the output parameter specified.
        /// </summary>
        /// <param name="output"> The HTML writer to write out to </param>
        protected override void Render(HtmlTextWriter output)
        {
            int pageActive = this.PageActive;
            int pageCount = this.PageCount;
            string xPageString = "<a href=\"javascript:{0}" +
                                 "\" class=\"{1}\"><img src=\"{2}" +
                                 "\" width=\"14\" height=\"11\" alt=\"\"/></a>";

            // check for one page we don't show nothing
            if (pageCount <= 1 || pageActive == 0 || pageActive > pageCount)
            {
                //output.Write("");
                output.Write(String.Format(xPageString, "void(0);", "arrowLeft arrowLeft-2 disabled", ImageFirstPage));
                output.Write(String.Format(xPageString, "void(0);", "arrowLeft disabled", ImageBackPage));
                output.Write("<span class=\"nr\">");
                output.Write("</span>");
                output.Write(String.Format(xPageString, "void(0);", "arrowRight arrowRight-2 disabled", ImageLastPage));
                output.Write(String.Format(xPageString, "void(0);", "arrowRight disabled", ImageNextPage));
            }
            else
            {
                //-- set begining of list
                if (pageActive > 1)
                {
                    output.Write(String.Format(xPageString, Page.ClientScript.GetPostBackEventReference(this, "1"), "arrowLeft arrowLeft-2", ImageFirstPage));
                    output.Write(String.Format(xPageString, Page.ClientScript.GetPostBackEventReference(this, (pageActive - 1).ToString()), "arrowLeft", ImageBackPage));
                }
                else
                {
                    output.Write(String.Format(xPageString, "void(0);", "arrowLeft arrowLeft-2 disabled", ImageFirstPage));
                    output.Write(String.Format(xPageString, "void(0);", "arrowLeft disabled", ImageBackPage));
                }

                // set start/end page list
                int startPage = pageActive - NR_OF_SHOW;
                int endPage = pageActive + NR_OF_SHOW;
                // check if pageActive is two small (<NR_OF_SHOW) 
                if (pageActive <= NR_OF_SHOW)
                    endPage += NR_OF_SHOW - pageActive + 1;
                if (pageActive > pageCount - NR_OF_SHOW)
                    startPage += pageCount - pageActive - NR_OF_SHOW;

                if (endPage > pageCount)
                    endPage = pageCount;
                if (startPage < 1)
                    startPage = 1;

                output.Write("<span class=\"nr\">");
                for (int counter = startPage; counter < endPage + 1; counter++)
                {
                    if (counter == pageActive)
                        output.Write("<a href=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, counter.ToString()) + "\" class=\"" + this.CssActivePage + "\">" + counter.ToString() + "</a>&nbsp;");
                    else
                        output.Write("<a href=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, counter.ToString()) + "\">" + counter.ToString() + "</a>&nbsp;");
                }
                output.Write("</span>");
                // set end of list 
                if (pageActive < pageCount)
                {
                    output.Write(String.Format(xPageString, Page.ClientScript.GetPostBackEventReference(this, pageCount.ToString()), "arrowRight arrowRight-2", ImageLastPage));
                    output.Write(String.Format(xPageString, Page.ClientScript.GetPostBackEventReference(this, (pageActive + 1).ToString()), "arrowRight", ImageNextPage));
                }
                else
                {
                    output.Write(String.Format(xPageString, "void(0);", "arrowRight arrowRight-2 disabled", ImageLastPage));
                    output.Write(String.Format(xPageString, "void(0);", "arrowRight disabled", ImageNextPage));
                }
            }
        }
    }
}

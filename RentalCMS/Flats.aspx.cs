using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Web.UI.HtmlControls;
using RentalCMS.RentalCore;

using RentalCommon;

namespace RentalCMS
{

   



    public partial class Flats : System.Web.UI.Page
    {
       

        public static ILog errorLog = log4net.LogManager.GetLogger(typeof(Flats));

        protected bool isFilterInitiated = false;

        private int PageSize = 100;

        public static int MAX_NUMBER_OF_COLUMNS = 128;

        public static string sel_no = "sort";

        public static string sel_up = "sort-up";

        public static string sel_dw = "sort-down";

        public List<Filter> Filters 
        {
            get 
            {
                var f = Session["Filters"] as List<Filter>;
                if (f != null)
                    return f;
                else
                {
                     Session["Filters"] = new List<Filter>();
                }
                return Session["Filters"] as List<Filter>;
            }

            set
            {
                Session["Filters"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetDefaulData();
            }
            else
            {
              
            }
        }

        private void SetDefaulData()
        {

            DateTime selectedStartDate = UIConvert.ToDateTime(_tbStartDateText.Text);
            DateTime selectedEndDate = UIConvert.ToDateTime(_tbEndDateText.Text);

            // -- pagination info
            int selectedActivePage = _plInfoGrid.PageSelected != 0 ? _plInfoGrid.PageSelected : 1;
            int selectedPageSize = UIConvert.ToInt32(pageItems.SelectedValue, PageSize);
            int pageCount = 0;
            int totalRowsNumber = 0;

            errorLog.Debug("IN the function");

            DAL.flat_info[] flats;// = DAL.FlatManager.GetAllFlats();


            errorLog.Debug("Try to get");


            var core = new RentalCoreClient();
            core.FlatList(this.Filters.ToArray(),
                selectedStartDate,

                selectedEndDate,

                 Convert.ToInt32(SortExpression),
                SortAscending,
                ref selectedActivePage,
                out flats,
                out pageCount,
                out totalRowsNumber, selectedPageSize);



            //DAL.FlatManager.FlatList(
            //    selectedSearchText,
            //    selectedFilter,
            //    selectedStartDate,
            //    selectedEndDate,
            //    Convert.ToInt32(SortExpression),
            //    SortAscending,
            //    ref selectedActivePage,
            //    selectedPageSize,
            //    out flats,
            //    out pageCount,
            //    out totalRowsNumber);

            //  flats = DAL.FlatManager.GetAllFlats();

            errorLog.Debug("Set data source");
            this._lwInfoListEdit.DataSource = flats;
            this._lwInfoListEdit.DataBind();


            // -- check if we have a valid session data
            if (flats != null && flats.Any())
            {
                // -- result show button for mutiple action
                ShowNavigation();

                // Show number rows per page with spec settings
                lbSelNumPerPage.Text = getCurrentPageNumberRows(selectedActivePage, flats.Count(), totalRowsNumber, selectedPageSize);


                // -- set pagging data
                _plInfoGrid.Visible = true;
                _plInfoGrid.PageCount = pageCount;
                _plInfoGrid.PageActive = selectedActivePage;
            }

        }

        protected string getCurrentPageNumberRows(int activePage, int itemsCount, int totalRowsNumber, int pageSize)
        {
            int firstCurrentPageRowNumber = (itemsCount > 0) ? (activePage - 1) * pageSize + 1 : 0;
            int lastCurrentPageRowNumber = (activePage - 1) * pageSize + itemsCount;

            return String.Format("{0} to {1} of {2}", firstCurrentPageRowNumber, lastCurrentPageRowNumber, totalRowsNumber);
        }

        private void ShowNavigation(bool toShow = true)
        {
            _lwInfoListEdit.Visible = toShow;
            _navPag.Visible = toShow;
        }




        protected void _btFilterData_Click(object sender, EventArgs e)
        {
            // Set a value that indicates, that filter was initiated
            isFilterInitiated = true;
            // -- check if have valid filter criteria
            SetDefaulData();
        }

        protected void btnAddFilterClick(object sender, EventArgs e)
        {
            var t = _tbSearchText.Text;
            if (!string.IsNullOrEmpty(t))
            { 
                var f = (Fiels)Convert.ToInt32(ddlFields.SelectedValue);
                this.Filters.Add(new Filter() { Field = f, Value = t });
                _tbSearchText.Text = string.Empty;
                FillFilters();
            }
        }


        protected void FillFilters()
        {
            rFilters.DataSource = this.Filters;
            rFilters.DataBind();
        }

        protected void _btClearData_Click(object sender, EventArgs e)
        {
            // -- clean filter and reload data grid information
            // -- clear filter
            _tbSearchText.Text = string.Empty;
            _tbStartDateText.Text = string.Empty;
            _tbEndDateText.Text = string.Empty;
            // -- clear check use for text
            _cbAddress.Checked = false;
            _cbRoomCount.Checked = false;

            this.Filters = null;
            FillFilters();
            SetDefaulData();


        }

        protected void _lwInfoList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            // -- get command option
            string command = e.CommandName;
            string argument = string.Empty;
            if (e.CommandArgument != null)
                argument = e.CommandArgument.ToString();
            if (command == "Action")
            {
                HiddenField hiddenFieldID = (HiddenField)e.Item.FindControl("hfID");
                int selectedID = Convert.ToInt32(hiddenFieldID.Value);

                this.Response.Redirect("~/FlatManager.aspx?id=" + selectedID);
            }
        }

        protected void _lwInfoList_Sorting(object sender, ListViewSortEventArgs e)
        {
            // string i = e.SortExpression;
            // -- if is second time, change direction, if is first time is true
            if (e.SortExpression == SortExpression)
                SortAscending = !SortAscending;
            else
                SortAscending = true;

            SortExpression = e.SortExpression;
            SetDefaulData();
            UpdateHeaderSort(this._lwInfoListEdit);
        }



        public void UpdateHeaderSort(ListView _lwInfoList)
        {
            // try to pars all columns
            for (int count = 1; count <= MAX_NUMBER_OF_COLUMNS; count++)
            {
                // try to find a label with this name
                HtmlGenericControl label = (HtmlGenericControl)_lwInfoList.FindControl("sort" + count.ToString());
                if (label != null)
                {
                    // if label exist set call with no select
                    label.Attributes["class"] = sel_no;
                    if (count.ToString() == SortExpression) // if count is sort expresion set up to proper direction
                        label.Attributes["class"] = SortAscending ? sel_up : sel_dw;
                }
            }
        }

        #region ONBase

        internal bool SortAscending
        {
            set
            {
                ViewState["SortAscending"] = value;
            }

            get
            {
                object obj = ViewState["SortAscending"];
                return (obj == null) || System.Convert.ToBoolean(obj);
            }
        }

        internal string SortExpression
        {
            set
            {
                ViewState["SortExpression"] = value;
            }

            get
            {
                object obj = ViewState["SortExpression"];
                return (obj == null) ? "0" : obj.ToString();
            }
        }


        protected void pageItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDefaulData();
        }

        protected void _plInfoGrid_Paging(object sender, System.EventArgs e)
        {
            SetDefaulData();
        }
        #endregion
    }
}
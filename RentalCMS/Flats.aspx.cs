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

        public SearchQuery GetFilters()
        {
           var result = new SearchQuery();

           var filters = new List<Filter1>();

            //Кол-во комнат
            if ((ddlRoomCount.SelectedIndex) > 0)
            {
                var field = this.CreateFilter(ddlRoomCount.SelectedItem.Text, Fields.ROOM_COUNT);
                filters.AddRange(field);
            }

            //Адрес
            if (!string.IsNullOrEmpty(tbAddress.Text))
            {
                var field = this.CreateFilter(tbAddress.Text, Fields.ADDRESS);
                filters.AddRange(field);
            }

            //Район 
            if ( (ddlRegion.SelectedIndex) > 0)
            {
                var field = this.CreateFilter(ddlRegion.SelectedItem.Text, Fields.REGION);
                filters.AddRange(field);
            }

            //Этаж
            if (ddlFloor.SelectedIndex > 0)
            {
                var field = this.CreateFilter(ddlFloor.SelectedItem.Text, Fields.FLOOR);
                filters.AddRange(field);
            }

            //Мебель
            if ( ddlFurniture.SelectedIndex > 0)
            {
                var field = this.CreateFilter(ddlFurniture.SelectedItem.Text, Fields.FURNITURE);
                filters.AddRange(field);
            }

            //Цена
            if (!string.IsNullOrEmpty(tbPrice.Text))
            {
                var field = this.CreateFilter(tbPrice.Text, Fields.PRICE);
                filters.AddRange(field);
            }

            DateTime selectedStartDate = UIConvert.ToDateTime(_tbStartDateText.Text);
            DateTime selectedEndDate = UIConvert.ToDateTime(_tbEndDateText.Text);

            if (selectedStartDate != DateTime.MinValue)
            {
                filters.Add(new Filter1(Fields.DATA, FilterConditions.MORE, selectedStartDate.AddSeconds(-1)));
                
            }

            if (selectedEndDate != DateTime.MinValue)
            {
                filters.Add(new Filter1(Fields.DATA, FilterConditions.LESS, selectedEndDate.AddSeconds(1)));

            }


            result.Filters = filters.ToArray();

            return result;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                SetDefaulData();
        }

        private void SetDefaulData()
        {

          

            // -- pagination info
            int selectedActivePage = _plInfoGrid.PageSelected != 0 ? _plInfoGrid.PageSelected : 1;
            int selectedPageSize = UIConvert.ToInt32(pageItems.SelectedValue, PageSize);
            int pageCount = 0;
            int totalRowsNumber = 0;

            var query = this.GetFilters();
            query.Page = selectedActivePage;
            query.PageSize =selectedPageSize;

            if ( Convert.ToInt32(SortExpression) > 0)
                query.SortField = (Fields) Convert.ToInt32(SortExpression);
            query.Ascending = SortAscending;
            
               

                //ref selectedActivePage,
                //out flats,
                //out pageCount,
                //out totalRowsNumber, selectedPageSize


            var core = new RentalCoreClient();
            var flats =  core.FlatSearch(query);

            totalRowsNumber = flats.TotallCount;

            

            errorLog.Debug("Set data source");
            this._lwInfoListEdit.DataSource = flats.Items;
            this._lwInfoListEdit.DataBind();


            if (flats != null && flats.Items.Any())
            {
                ShowNavigation();
                // Show number rows per page with spec settings
                lbSelNumPerPage.Text = getCurrentPageNumberRows(selectedActivePage, flats.Items.Count(), totalRowsNumber, selectedPageSize);
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




        protected void ApplyFilters(object sender, EventArgs e)
        {
            isFilterInitiated = true;
            SetDefaulData();
        }

        protected void ClearFilters(object sender, EventArgs e)
        {
            tbAddress.Text = string.Empty;
            _tbStartDateText.Text = string.Empty;
            _tbEndDateText.Text = string.Empty;
            ddlFloor.SelectedIndex = 0;
            ddlRegion.SelectedIndex = 0;
            ddlRoomCount.SelectedIndex = 0;
            ddlFurniture.SelectedIndex = 0;
            tbPrice.Text = string.Empty;
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


        private Filter1[] CreateFilter(string value, Fields field)
        {

            var result = new List<Filter1>();
            if (!string.IsNullOrEmpty(value))
            {
                if (value.Contains('>'))
                    result.Add(new Filter1(field,FilterConditions.MORE, value));
                if (value.Contains('<'))
                    result.Add(new Filter1(field,FilterConditions.LESS, value));
                if (value.Contains('-'))
                {
                    string[] vals = value.Split('-');
                    var start = Convert.ToInt32(vals[0]) -1;
                    var end = Convert.ToInt32(vals[1]) + 1;
                    result.Add(new Filter1(field,FilterConditions.MORE, start));
                    result.Add(new Filter1(field,FilterConditions.LESS, end));
                }
            }
            return result.ToArray();
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
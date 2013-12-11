using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Web.UI.HtmlControls;
using RentalCMS.RentalCore;
using DAL;

using RentalCommon;

namespace RentalCMS
{





    public partial class Flats : System.Web.UI.Page
    {


        public static ILog errorLog = log4net.LogManager.GetLogger(typeof(Flats));

        public static RentalCoreClient core = new RentalCoreClient();

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
                filters.Add(new Filter1(Fields.ADDRESS, FilterConditions.CONTAIN, tbAddress.Text));
            }

            //Район 
            if ((ddlRegion.SelectedIndex) > 0 && Convert.ToInt32(ddlRegion.SelectedValue) > 0)
            {
                var field = this.CreateFilter(ddlRegion.SelectedValue, Fields.REGION);
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
            int priceFrom;
            if (!string.IsNullOrEmpty(tbPriceFrom.Text) && int.TryParse(tbPriceFrom.Text.Trim(), out priceFrom))
            {
                filters.Add(new Filter1(Fields.PRICE, FilterConditions.MOREEQUAL, priceFrom));
            }
            int priceTo;
            if (!string.IsNullOrEmpty(tbPriceTo.Text) && int.TryParse(tbPriceTo.Text.Trim(), out priceTo))
            {
                filters.Add(new Filter1(Fields.PRICE, FilterConditions.LESSEQUAL, priceTo));
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
            {
                var regions = new List<region_type>();
                regions.Add(new region_type() { id = -1, name = "-- неизвестно --" });
                regions.AddRange(core.RegionTypeAll());

                ddlRegion.DataSource = regions;
                ddlRegion.DataTextField = "name";
                ddlRegion.DataValueField = "id";
                ddlRegion.DataBind();

                SetDefaulData();
                
            }
        }

        private void SetDefaulData()
        {
            int selectedActivePage = _plInfoGrid.PageSelected != 0 ? _plInfoGrid.PageSelected : 1;
            int selectedPageSize = UIConvert.ToInt32(pageItems.SelectedValue, PageSize);
           
            var query = this.GetFilters();
            query.Page = selectedActivePage -1 ;
            query.PageSize =selectedPageSize;

            if (Convert.ToInt32(SortExpression) > 0)
                query.SortField = (Fields)Convert.ToInt32(SortExpression);
            else
                query.SortField = Fields.ID;



            query.Ascending = SortAscending;
            var flats =  core.FlatSearch(query);

            this._lwInfoListEdit.DataSource = flats.Items;
            this._lwInfoListEdit.DataBind();


            if (flats != null && flats.Items.Any())
            {
                ShowNavigation();
                lbSelNumPerPage.Text = getCurrentPageNumberRows(selectedActivePage, flats.Items.Count(), flats.TotallCount, selectedPageSize);

                var pageCount = flats.TotallCount / selectedPageSize;
                if (flats.TotallCount % selectedPageSize != 0) pageCount++;

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
            tbPriceFrom.Text = string.Empty;
            tbPriceTo.Text = string.Empty;
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
                    result.Add(new Filter1(field,FilterConditions.MORE, value.Replace(">","")));

                else if (value.Contains('<'))
                    result.Add(new Filter1(field, FilterConditions.LESS, value.Replace("<", "")));

                else if (value.Contains('-'))
                {
                    string[] vals = value.Split('-');
                    var start = Convert.ToInt32(vals[0]) ;
                    var end = Convert.ToInt32(vals[1]) ;
                    result.Add(new Filter1(field,FilterConditions.MOREEQUAL, start));
                    result.Add(new Filter1(field,FilterConditions.LESSEQUAL, end));
                }
                else
                    result.Add(new Filter1(field, FilterConditions.EQUAL, value));

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
                if (obj == null)
                    return false;
                else
                    return System.Convert.ToBoolean(obj);
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
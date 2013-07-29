using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;

namespace RentalCMS
{
    public partial class Flats : System.Web.UI.Page
    {
        public static ILog errorLog = log4net.LogManager.GetLogger(typeof(Flats));

        protected bool isFilterInitiated = false;

        private int PageSize = 100;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetDefaulData();
            }
        }

        private void SetDefaulData()
        {

            errorLog.Debug("IN the function");

            List<DAL.flat_info> flats;// = DAL.FlatManager.GetAllFlats();

            int activePage = 1;
            int totalRowsNumber;
            int pageCount;
            errorLog.Debug("Try to get");

            DAL.FlatManager.FlatList(
                "",
                (int)DAL.Fiels.ID,
                DateTime.MinValue,
                DateTime.MinValue,
                (int)DAL.Fiels.ID,
                true,
                ref activePage,
                PageSize,
                out flats,
                out pageCount,
                out totalRowsNumber);

           // flats = DAL.FlatManager.GetAllFlats();
            errorLog.Debug("Set data source");
            this._lwInfoListEdit.DataSource = flats;
            this._lwInfoListEdit.DataBind();
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
             
            }
        }

        protected void _btFilterData_Click(object sender, EventArgs e)
        {
            // Set a value that indicates, that filter was initiated
            isFilterInitiated = true;
            // -- check if have valid filter criteria
          
        }

        protected void _btClearData_Click(object sender, EventArgs e)
        {
            // -- clean filter and reload data grid information
            // -- clear filter
            _tbSearchText.Text = string.Empty;
            _tbStartDateText.Text = string.Empty;
            _tbEndDateText.Text = string.Empty;
            // -- clear check use for text
            _cbOriginalName.Checked = false;
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
                return (obj == null) ? String.Empty : obj.ToString();
            }
        }

        #endregion
    }
}
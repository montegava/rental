using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;

namespace RentalCMS.Controls
{
    /// <summary>
    /// Alows user to select year, automaticaly generates list based on From/To properties
    /// </summary>
    public class RangeDropDownList : DropDownList
    {
        private int cachedSelectedYear;

        /// <summary>
        /// Gets or sets the year to start from.
        /// </summary>
        public int From { get; set; }

        /// <summary>
        /// Gets or sets the year to end.
        /// </summary>
        public int To { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
        }

        protected override void PerformDataBinding(IEnumerable dataSource)
        {
            var items = new List<ListItem>();
            if (From > To) 
                throw new InvalidOperationException("FromYear must be less than ToYear");

            var hasSelected = false;
            for (int i = From; i <= To; i++)
            {
                items.Add(new ListItem(i.ToString(CultureInfo.InvariantCulture), i.ToString(CultureInfo.InvariantCulture)));
                hasSelected = hasSelected | (i == cachedSelectedYear);
            }
            if (!hasSelected)
            {
             //   items.Insert Add(new ListItem(cachedSelectedYear.ToString(CultureInfo.InvariantCulture), cachedSelectedYear.ToString(CultureInfo.InvariantCulture)));
            }
            base.PerformDataBinding(items);

        }

        /// <summary>
        /// Gets or sets the year currently selected in this control
        /// </summary>
        public int SelectedYear
        {
            get
            {
                return Convert.ToInt32(this.SelectedValue);
            }
            set
            {
                this.cachedSelectedYear = value;
                this.SelectedValue = value.ToString(CultureInfo.InvariantCulture);
            }

        }
    }
}
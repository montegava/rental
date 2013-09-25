using System;
using System.Web;
using System.Web.UI;
using System.Reflection;
using System.Web.UI.WebControls;

namespace SmartControls
{
    public class BaseControl : WebControl
    {
        /// <summary>
        /// default id use non valid value from object
        /// </summary>
        protected const int DEFAULT_ID = -1;


        // -- set to use default control's css
        public bool useDefaultCss = true;


    }
}

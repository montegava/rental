using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Rental
{
    public class AvitoManager
    {
        public static Advert GetAdvert(string url)
        {
            string error;
            string page = WebPage.LoadPage(url, Encoding.GetEncoding("utf-8"), out error);

            if (string.IsNullOrEmpty(error))
            {
                Match m = Regex.Match( page, "t_i_title t_i_title_r\">.*?<a.*?href=\"(.*?)\"", RegexOptions.Singleline);



                if (m.Success && m.Groups.Count > 1)
                { 
                    
                }
            }
            return null;
        }
    }
}

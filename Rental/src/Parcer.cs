using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Rental
{
    /// <summary>
    /// Iterator
    /// </summary>
    interface IParser
    {
        /// <summary>
        /// get page content by url
        /// </summary>
        /// <param name="url">lint to page</param>
        /// <returns>page content</returns>
        string GetPage(string url);
    }


    static class WebPage
    {
        public static CookieCollection Cookies = new CookieCollection();

        public static string LoadPage(string AUrl, out string error)
        {
            return LoadPage(AUrl, null, out error);
        }


        //Загружает страницу - результат = исходный код страницы
        public static string LoadPage(string AUrl, Encoding encoding, out string error)
        {
            

            string res = String.Empty;
            error = String.Empty;
            HttpWebResponse WebResponse = null;

            try
            {
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(AUrl);
                Request.UserAgent = "Opera/9.80 (Windows NT 6.1; U; ru) Presto/2.8.131 Version/11.10";
                Request.Accept = "text/html, application/xml;q=0.9, application/xhtml+xml, image/png, image/webp, image/jpeg, image/gif, image/x-xbitmap, */*;q=0.1";
                Request.Timeout = 20000;
                Request.ReadWriteTimeout = 20000;
                Request.Headers.Add("Accept-Language", "ru");
                Request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                Request.MaximumAutomaticRedirections = 60;
                Request.KeepAlive = true;

                Request.CookieContainer = new CookieContainer();
                if (Cookies != null)
                    Request.CookieContainer.Add(Cookies);

                WebResponse = (HttpWebResponse)Request.GetResponse();

                if (WebResponse.Cookies != null && WebResponse.Cookies.Count > 0)
                {
                    Cookies = new CookieCollection();
                    Cookies.Add(WebResponse.Cookies);
                }

                StreamReader Reader;
                if (encoding != null)
                    Reader = new StreamReader(WebResponse.GetResponseStream(), encoding);
                else
                    Reader = new StreamReader(WebResponse.GetResponseStream());
                res = Reader.ReadToEnd();
                Reader.Close();

            }
            catch (Exception ex)
            {
                error = "ERROR on get page " + AUrl + ". Exception: " + ex.Message;
                res = "";
            }
            finally
            {
                if (WebResponse != null)
                    WebResponse.Close();
            }
            return res;
        }

    }
}

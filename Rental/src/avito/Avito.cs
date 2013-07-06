using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DAL;
using System.Net;
using System.Drawing;
using System.IO;
using System.Threading;
using ImageProcessing;

namespace Rental
{
    public class Avito
    {
        public List<Advert> m_adverts = new List<Advert>();
        private List<DAL.black_list> m_Exclude;

        #region Для обновления контролла с класса
        public event SetUIProgress onSetUIProgress;
        public event SetPageCountForLoad onSetPageCountForLoad;
        public event SetPageCountLoaded onSetPageCountLoaded;
        public event CheckCansel onCheckCansel;
        #endregion

        public Avito(List<DAL.black_list> exclude)
        {
            m_Exclude = exclude;
        }

        /// <summary>
        /// Парсит страницу и формирует список объявлений
        /// </summary>
        /// <param name="pageListContent">Страница со списком объяв</param>
        /// <returns></returns>
        public List<Advert> GetAdvertList(string pageListContent)
        {
            List<Advert> result = new List<Advert>();
            if (!String.IsNullOrEmpty(pageListContent))
            {
                #region 1. Collect all links from page
                Match m = Regex.Match(pageListContent, "t_i_title t_i_title_r\">.*?<a.*?href=\"(.*?)\"", RegexOptions.Singleline);
                List<string> linklist = new List<string>();
                while (m.Success)
                {
                    if (onCheckCansel())
                        return result;
                    if (m.Groups.Count > 1)
                    {
                        string[] parts = m.Groups[1].ToString().Split('_');
                        linklist.Add("http://m.avito.ru/item/" + parts[parts.Length - 1]);
                    }
                    m = m.NextMatch();
                }
                onSetPageCountForLoad(linklist.Count);
                onSetUIProgress();
                #endregion

                //2. Load
                foreach (string url in linklist)
                {
                    #region 2.1. Check if url already uploaded and added
                    if (onCheckCansel())
                        return result;
                    DAL.flat_info flat = null;
                    if (FlatManager.CheckUrlIfExist(url, out flat) && flat != null)
                    {
                        Advert a = Convetor.Flat2Advert(flat);
                        a.IsStar = true;
                        a.IsBlocked = false;
                        a.ImageIndex = (int)ImageMode.imAvito;
                        result.Add(a);
                        continue;
                    }
                    #endregion

                    Advert advert = GetAdvertByUrl(url);
                    if (advert != null)
                        result.Add(advert);
                    Thread.Sleep(2500);
                }
            }
            return result;
        }

        /// <summary>
        /// Parcing advert from url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Advert GetAdvertByUrl(string url)
        {
            Advert result = null;
            string error;
            WebPage.Cookies = null;
            Log.Append("\tLoadPage " + url);
            string page = WebPage.LoadPage(url, Encoding.GetEncoding("utf-8"), out error);
            onSetPageCountLoaded(1);
            onSetUIProgress();

            if (string.IsNullOrEmpty(error))
            {
                result = GetAdvert(page, url);
                if (result != null)
                {
                    result.Link = url;
                    result.ImageIndex = (int)ImageMode.imAvito;
                    IsBlocked(result);
                }
            }
            return result;
        }

        public Advert GetAdvert(string contentPage, String url)
        {
            Advert result = new Advert();

            #region Phone OLD
            //Match m = Regex.Match(contentPage, "\"Телефон продавца\" src=\"(.*?)\"", RegexOptions.Singleline);
            //if (m.Success && m.Groups.Count > 1)
            //{
            //    Log.Append("\tLoadPage item " + "http://m.avito.ru" + m.Groups[1]);
            //    // item/66524359/phone/de3bbcc77141c11234e2b3c7bf3fc223
            //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://m.avito.ru" + m.Groups[1]);
            //    request.CookieContainer = new CookieContainer();
            //    request.CookieContainer.Add(WebPage.Cookies);
            //    request.UserAgent = "Opera/9.80 (Windows NT 6.1; U; ru) Presto/2.8.131 Version/11.10";
            //    request.Accept = "text/html, application/xml;q=0.9, application/xhtml+xml, image/png, image/webp, image/jpeg, image/gif, image/x-xbitmap, */*;q=0.1";
            //    request.Timeout = 20000;
            //    request.ReadWriteTimeout = 20000;
            //    request.Headers.Add("Accept-Language", "ru");
            //    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            //    request.MaximumAutomaticRedirections = 60;
            //    request.KeepAlive = true;
            //    request.Referer = "http://m.avito.ru/item/" + m.Groups[1].ToString();
            //    request.Headers.Add(HttpRequestHeader.AcceptCharset, "windows-1251,utf-8;q=0.7,*;q=0.3");
            //    using (var response = request.GetResponse())
            //    using (var stream = response.GetResponseStream())
            //    {
            //        Image bmp = Bitmap.FromStream(stream);
            //        Log.Append("\tphone before recognition");

            //        string phone = String.Empty;
            //        try
            //        {
            //             phone = ImageProcessing.ImageRecognition.RecognizeImage(bmp, false);
            //        }
            //        catch (Exception e)
            //        {

            //            Log.Append("\tERROR " + e.Message);
            //            return null;
            //        }
                    
                    
                    
            //        Log.Append("\tphone after recognition: " + phone);
            //        phone = phone.Replace("-", "");
            //        phone = phone.Replace("(", "");
            //        phone = phone.Replace(")", "");
            //        phone = phone.Replace(" ", "");

            //        string[] phones = phone.Split(',');

            //        result.Phones = new List<string>();
            //        result.Phones.AddRange(phones);
            //    }

            //}
            #endregion

            #region Phone NEW

            Match m = Regex.Match(contentPage, @"<a id=""showPhoneBtn"".*?href=""(.*?)""", RegexOptions.Singleline);
            if (m.Success && m.Groups.Count > 1)
            {
                Log.Append("\tLoadPage item " + "http://m.avito.ru" + m.Groups[1]);

                string phone = string.Empty;
                string error = String.Empty;
                     
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://m.avito.ru" + m.Groups[1] + "?async");
                    request.CookieContainer = new CookieContainer();
                    request.CookieContainer.Add(WebPage.Cookies);
                    request.UserAgent = "Opera/9.80 (Windows NT 6.1; U; ru) Presto/2.8.131 Version/11.10";
                    request.Accept = "text/html, application/xml;q=0.9, application/xhtml+xml, image/png, image/webp, image/jpeg, image/gif, image/x-xbitmap, */*;q=0.1";
                    request.Timeout = 20000;
                    request.ReadWriteTimeout = 20000;
                    request.Headers.Add("Accept-Language", "ru-ru,ru;q=0.8,en-us;q=0.5,en;q=0.3");
                    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                    request.MaximumAutomaticRedirections = 60;
                    request.KeepAlive = true;
                    request.Referer = url;
                    request.Headers.Add(HttpRequestHeader.AcceptCharset, "windows-1251,utf-8;q=0.7,*;q=0.3");

                    var WebResponse = (HttpWebResponse)request.GetResponse();

                    StreamReader Reader;
                    Reader = new StreamReader(WebResponse.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                    phone = Reader.ReadToEnd();
                    Reader.Close();
                }
                catch (Exception ex)
                {

                    error = ex.Message;
                }
             

              

              //  string phone = WebPage.LoadPage("http://m.avito.ru" + m.Groups[1] + "?async", Encoding.GetEncoding("utf-8"), out error);


            
                if (String.IsNullOrWhiteSpace(error))
                {
                   


                    Log.Append("\tphone after recognition: " + phone);
                    phone = phone.Replace("-", "");
                    phone = phone.Replace("(", "");
                    phone = phone.Replace(")", "");
                    phone = phone.Replace(" ", "");

                    string[] phones = phone.Split(',');

                    result.Phones = new List<string>();
                    result.Phones.AddRange(phones);
                }
                else
                    Log.Append("ERROR: " + error);

            }
            #endregion

            #region Content

            m = Regex.Match(contentPage, "<ul class=\"list\">(.*?)</ul>", RegexOptions.Singleline);
            StringBuilder sb = new StringBuilder();

            while (m.Success)
            {
                result.Content = m.Groups[1].ToString() + "\r\n";
                if (m.Groups.Count > 1)
                {
                    sb.Append(m.Groups[1].ToString());
                    sb.Append("\r\n");
                }
                m = m.NextMatch();
            }

            string content = sb.ToString();
            content = Regex.Replace(content, "<.*?>", "");
            content = content.Replace("Телефон", "");
            content = content.Replace(" Написать письмо", "");
            content = Regex.Replace(content, "&#160.*", "", RegexOptions.Singleline);
            content = Regex.Replace(content, "\n", " ", RegexOptions.Singleline);
            result.Content = content;
            #endregion

            #region has image
            m = Regex.Match(contentPage, "m_item_images");
            result.HasPhoto = m.Success;
            #endregion

            return result;
        }

        /// <summary>
        /// Check if advert consist stop words!
        /// </summary>
        /// <param name="advert"></param>
        /// <returns></returns>
        private bool IsBlocked(Advert advert)
        {
            if (m_Exclude != null && advert != null)
                foreach (DAL.black_list exc in m_Exclude)
                {

                    if (exc.TYPE_ID == 1) //word
                        if (advert.Content != null && advert.Content.IndexOf(exc.STOP) > 0)
                        {
                            advert.IsBlocked = true;
                            return true;
                        }

                    if (exc.TYPE_ID == 0) //phone
                    {
                        if (advert.Phones.Contains(exc.STOP))
                        {
                            advert.IsBlocked = true;
                            return true;
                        }
                    }
                }
            return false;
        }


    }
}

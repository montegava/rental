using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Drawing;
using System.IO;
using System.Threading;
using ImageProcessing;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using log4net;

namespace Rental
{
    public class Slando
    {
        public static readonly ILog Log = LogManager.GetLogger("TestApplication");

        public List<Advert> m_adverts = new List<Advert>();
        private List<DAL.black_list> m_Exclude;

        #region Для обновления контролла с класса
        public event SetUIProgress onSetUIProgress;
        public event SetPageCountForLoad onSetPageCountForLoad;
        public event SetPageCountLoaded onSetPageCountLoaded;
        public event CheckCansel onCheckCansel;
        #endregion

        public Slando(List<DAL.black_list> exclude)
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
                Match m = Regex.Match(pageListContent, @"<td class=""offer.*?<a href=""(http://voronezh\.vor\.slando\.ru/obyavlenie.*?)"".*?</td>", RegexOptions.Singleline);
                List<string> linklist = new List<string>();
                while (m.Success)
                {
                    if (onCheckCansel()) return result;
                    if (m.Groups.Count > 1)
                        linklist.Add(m.Groups[1].ToString());
                    m = m.NextMatch();
                }
                Log.Debug("\tLink count: " + linklist.Count.ToString());
                onSetPageCountForLoad(linklist.Count);
                onSetUIProgress();


                //2. Load
                foreach (string url in linklist)
                {
                    Log.Debug("\t\tGet link: " + url);
                    #region 2.1. Check if url already uploaded and added
                    if (onCheckCansel())
                        return result;
                    DAL.flat_info flat = null;
                    //if (FlatManager.CheckUrlIfExist(url, out flat) && flat != null)
                    //{
                    //    Log.Append("\t\tAlready in DB!");
                    //    Advert a = Convetor.Flat2Advert(flat);
                    //    a.IsStar = true;
                    //    a.IsBlocked = false;
                    //    a.ImageIndex = (int)ImageMode.imSlando;
                    //    result.Add(a);
                    //    continue;
                    //}
                    #endregion
                    Log.Debug("\t\tLoading...");
                    Advert advert = GetAdvertByUrl(url);
                    if (advert != null)
                        result.Add(advert);
                    GC.Collect();
                    Thread.Sleep(500);
                }
            }
            else
                Log.Debug("\tContent is empty ");
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
            string page = WebPage.LoadPage(url, Encoding.GetEncoding("utf-8"), out error);
            onSetPageCountLoaded(1);
            onSetUIProgress();

            if (string.IsNullOrEmpty(error))
            {
                Log.Debug("\t\tPage size: " + page.Length.ToString());
                result = GetAdvert(page, url);
                if (result != null)
                {
                    result.Link = url;
                    result.ImageIndex = (int)ImageMode.imSlando;
                    IsBlocked(result);
                }
            }
            else
                Log.Debug("\t\tERROR: " + error);
            return result;
        }

        public Advert GetAdvert(string contentPage, string url)
        {
            Log.Debug("\t\tGetAdvert");
            Advert result = new Advert();

            #region Phone
            string[] parts = url.Split('-');
            string lasPart = parts[parts.Length - 1];
            var m = Regex.Match(lasPart, @"ID(.*?)\.");
            if (m.Success && m.Groups.Count > 1)
            {

                var imageUrl = String.Format("http://voronezh.vor.slando.ru/ajax/misc/phoneimage/{0}/?nomobile=1", m.Groups[1].ToString());
                Log.Debug("\t\tLoad image: " + imageUrl);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imageUrl);
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(WebPage.Cookies);
                request.UserAgent = "Opera/9.80 (Windows NT 6.1; U; ru) Presto/2.8.131 Version/11.10";
                request.Accept = "text/html, application/xml;q=0.9, application/xhtml+xml, image/png, image/webp, image/jpeg, image/gif, image/x-xbitmap, */*;q=0.1";
                request.Timeout = 20000;
                request.ReadWriteTimeout = 20000;
                request.Headers.Add("Accept-Language", "ru");
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                request.MaximumAutomaticRedirections = 60;
                request.KeepAlive = true;
                request.Referer = url;
                request.Headers.Add(HttpRequestHeader.AcceptCharset, "windows-1251,utf-8;q=0.7,*;q=0.3");
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    Image bmp = Bitmap.FromStream(stream);
                    Log.Debug("\t\tdone");
                    Log.Debug("\t\tRecognition...");
                    string phone = string.Empty;
                    try
                    {
                        phone = ImageProcessing.ImageRecognition.RecognizeImage(bmp, true);
                    }
                    catch (Exception ex)
                    {

                        Log.Debug("\t\tERROR: " + ex.Message + "\r\n"+ ex.InnerException + ex.InnerException + ex.Source + ex.TargetSite + ex.HelpLink + ex.ToString() );
                    }
                    
                    Log.Debug("\t\tdone. " + phone);
                    phone = phone.Replace("-", "");
                    phone = phone.Replace("(", "");
                    phone = phone.Replace(")", "");
                    phone = phone.Replace(" ", "");
                    phone = phone.Replace("\n", "");


                    string[] phones = phone.Split(',');
                    if (phone.IndexOf(';') > 0)
                        phones = phone.Split(';');


                    result.Phones = new List<string>();
                    if (phones.Length > 0)
                        foreach (var phone1 in phones)
                        {
                            phone = phone1;
                            if (phone.Length > 14 && phone.IndexOf('8') >= 0 && phone.IndexOf('8') < phone.Length)
                                phone = phone.Remove(0, phone.IndexOf('8'));
                            result.Phones.Add(phone);
                        }
                }
            }
            #endregion


            #region Content

            m = Regex.Match(contentPage, "<p class=\"marginbott20 lheight20 large pdingright20\">(.*?)</p", RegexOptions.Singleline);
            if (m.Success && m.Groups.Count > 1)
            {
                string content = m.Groups[1].ToString();
                content = Regex.Replace(content, "<.*?>", "");
                content = Regex.Replace(content, "\n", " ", RegexOptions.Singleline);
                result.Content = content;
            }
            #endregion

            #region has image
            m = Regex.Match(contentPage, "gallery_img");
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

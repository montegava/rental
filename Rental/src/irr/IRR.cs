using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Text.RegularExpressions;

namespace Rental
{
    class IRR
    {
        // Founded list
        public List<Advert> m_adverts = new List<Advert>();
        // Exclude list
        private List<DAL.black_list> m_Exclude;

        #region Для обновления контролла с класса
        public event SetUIProgress onSetUIProgress;
        public event SetPageCountForLoad onSetPageCountForLoad;
        public event SetPageCountLoaded onSetPageCountLoaded;
        public event CheckCansel onCheckCansel;
        #endregion

        public IRR(List<DAL.black_list> exclude)
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
                Match m = Regex.Match(pageListContent, @"<td[\s]class=""tdTxt"">.*?<a[\s]href=""(.*?)"">", RegexOptions.Singleline);
                List<string> linklist = new List<string>();
                while (m.Success)
                {
                    if (onCheckCansel())
                        return result;
                    if (m.Groups.Count > 1)
                        linklist.Add(m.Groups[1].ToString());
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
                        a.ImageIndex = (int)ImageMode.imIRR;
                        result.Add(a);
                        continue;
                    }
                    #endregion

                    Advert advert = GetAdvertByUrl(url);
                    if (advert != null)
                        result.Add(advert);

                }
            }
            return result;
        }

        /// <summary>
        /// Parcing advert from url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private Advert GetAdvertByUrl(string url)
        {
            Advert result = null;
            string error;
            string page = WebPage.LoadPage(url, Encoding.GetEncoding("utf-8"), out error);
            onSetPageCountLoaded(1);
            onSetUIProgress();

            if (string.IsNullOrEmpty(error))
            {
                result = GetAdvert(page);
                if (result != null)
                {
                    result.Link = url;
                    result.ImageIndex = (int)ImageMode.imIRR;
                    IsBlocked(result);
                }
            }
            return result;
        }

        public Advert GetAdvert(string contentPage)
        {
            Advert result = new Advert();

            var m = Regex.Match(contentPage, @"<h1>(.*?)<", RegexOptions.Singleline);
            if (m.Success && m.Groups.Count > 1)
                result.Content += m.Groups[1].ToString() + "\r\n";


            m = Regex.Match(contentPage, @"<span[\s]class=""advert_text"">(.*?)</span>", RegexOptions.Singleline);
            if (m.Success && m.Groups.Count > 1)
                result.Content += m.Groups[1].ToString() + "\r\n";

            m = Regex.Match(contentPage, @"<div[\s]class=""padB10[\s]nameConpany"">(.*?)</div>", RegexOptions.Singleline);
            if (m.Success && m.Groups.Count > 1)
                result.Content += "\r\n" + m.Groups[1] + "\r\n";

            string phoneContext = "";
            // m = Regex.Match(content, @"<div[\s]class=""wrap"">.*?nameConpany(.*?)redMessage", RegexOptions.Singleline);
            m = Regex.Match(contentPage, @"<div[\s]class=""wrap""[^>]*?>[\s]*<div[\s]class=""padB10[\s]nameConpany"">(.*?)redMessage", RegexOptions.Singleline);
            if (m.Success && m.Groups.Count > 1)
                phoneContext = m.Groups[1].ToString();
            else
            {
                m = Regex.Match(contentPage, @"<div[\s]class=""wrap""[^>]*?>[\s]*<div[\s]class=""padB10[\s]wrapMargin"">(.*?)redMessage", RegexOptions.Singleline);
                if (m.Success && m.Groups.Count > 1)
                    phoneContext = m.Groups[1].ToString();
            }

            if (phoneContext.Length > 0)
            {
                List<string> phone_list = new List<string>();
                Regex r = new Regex(@"[\d-)(+ ]{6,}");
                m = r.Match(phoneContext);
                while (m.Success)
                {
                    string phone = m.Groups[0].ToString().Trim();

                    phone = phone.Replace("-", "");
                    phone = phone.Replace("(", "");
                    phone = phone.Replace(")", "");
                    phone = phone.Replace(" ", "");

                    if (!phone_list.Contains(phone))
                        phone_list.Add(phone);
                    m = m.NextMatch();
                }
                result.Phones = phone_list;


                m = Regex.Match(phoneContext, @"<div[\s]class=""wrapIcons"">(.*?)</div>");
                string data = "";
                while (m.Success)
                {
                    data += "\r\n" + m.Groups[0].ToString().Trim();
                    m = m.NextMatch();
                }

                r = new Regex("<.*?>|&nbsp", RegexOptions.Singleline);
                string adv_content = r.Replace(data, "").Trim();
                result.Content += adv_content;
            }
            else
                Log.Append("\tERROR on parse contacts");

            if (result != null && result.Content != null && result.Content.Length > 0)
            {
                Regex r = new Regex(@"<br[\s]*/>", RegexOptions.Singleline);
                result.Content = r.Replace(result.Content, "\r\n").Trim();
            }

            result.ImageIndex = (int)ImageMode.imMoyaReklama;


            #region has image
            m = Regex.Match(contentPage, "b-photo");
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
                    if (advert.Phones.Contains(exc.STOP))
                    {
                        advert.IsBlocked = true;
                        return true;
                    }
                }
            return false;
        }
    }

}

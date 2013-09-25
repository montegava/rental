using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using DAL;

namespace Rental
{
    class Camelot
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

        public Camelot(List<DAL.black_list> exclude)
        {
            m_Exclude = exclude;
        }

        /// <summary>
        /// Парсит страницу и формирует список объявлений
        /// </summary>
        /// <param name="pageListContent">Страница</param>
        /// <returns></returns>
        public List<Advert> GetAdvertList(string pageListContent, DateTime posterDate)
        {
            List<Advert> result = new List<Advert>();
            if (!String.IsNullOrEmpty(pageListContent))
            {
                #region 1. Collect all links from page
                Match m = Regex.Match(pageListContent, @"<div[\s]class=""ads-content-div"">(.*?)</div>", RegexOptions.Singleline);
                List<string> linklist = new List<string>();
                while (m.Success)
                {
                    if (onCheckCansel())
                        return result;
                    if (m.Groups.Count > 1)
                        linklist.Add(m.Groups[1].ToString());
                    m = m.NextMatch();
                }
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
                        a.ImageIndex = (int)ImageMode.imCamelot;
                        result.Add(a);
                        continue;
                    }
                    #endregion

                    Advert adv = GetAdvert(url);
                    if (adv != null && !isOld(adv, posterDate))
                    {
                        IsBlocked(adv);
                        if (!adv.IsBlocked && !adv.IsStar)
                        {
                            if (onCheckCansel())
                                return result;
                            onSetPageCountForLoad(1);
                            onSetUIProgress();
                            //Надо грузиться вглубь
                            string error;
                            string page = WebPage.LoadPage(adv.Link, Encoding.GetEncoding("windows-1251"), out error);
                            onSetPageCountLoaded(1);
                            onSetUIProgress();
                            if (!String.IsNullOrEmpty(error))
                            {
                                adv.GetPhones(page);
                                // повторно проверяем    
                                IsBlocked(adv);
                            }
                        }
                        result.Add(adv);
                    }
                }
            }
            return result;
        }

        public Advert GetAdvert(string pageListContent)
        {
            Advert result = null;
            if (!String.IsNullOrEmpty(pageListContent))
            {
                //#region 1. Collect all links from page
                Match m = Regex.Match(pageListContent, @"<a[\s]href=""(.*?)""", RegexOptions.Singleline);
                List<string> linklist = new List<string>();
                if (m.Success && m.Groups.Count > 1)
                {
                    string url = Const.CNT_CAMELOT_DOMAIN + m.Groups[1].ToString().Trim();

                    //Check if url already uploaded and added
                    DAL.flat_info flat = null;
                    if (FlatManager.CheckUrlIfExist(url, out flat) && flat != null)
                    {
                        Advert a = Convetor.Flat2Advert(flat);
                        a.IsStar = true;
                        a.IsBlocked = false;
                        a.ImageIndex = (int)ImageMode.imCamelot;
                        return a;
                    }

                    string adv_content = Regex.Replace(pageListContent, "<.*?>|&nbsp", "").Trim();
                    List<string> phone_list = new List<string>();

                    m = Regex.Match(adv_content, @"[\d-)(+]{6,}");
                    while (m.Success)
                    {
                        string phone = m.Groups[0].ToString().Trim();
                        phone = phone.Replace("-", "");
                        phone = phone.Replace("(", "");
                        phone = phone.Replace(")", "");
                        phone_list.Add(phone);
                        m = m.NextMatch();
                    }

                    Advert adv = new Advert();
                    adv.Link = url;
                    adv.Content = adv_content;
                    adv.Phones = phone_list;
                    adv.ImageIndex = (int)ImageMode.imCamelot;
                    return adv;
                }
            }
            return result;
        }

        private bool isOld(Advert adv, DateTime posterDate)
        {
            if (posterDate != DateTime.MinValue && adv != null && !String.IsNullOrEmpty(adv.Content))
            {
                Match m = Regex.Match(adv.Content, "опубликовано в газете(.*)");
                if (m.Success && m.Groups.Count > 1)
                {
                    string data = m.Groups[1].ToString().Trim();
                    try
                    {
                        DateTime advDate = DateTime.Parse(data);
                        return advDate.AddHours(1) < posterDate;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
            return false;
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

        /// <summary>
        /// Parse poster date time //анонс
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static DateTime GetPosterDate(string page)
        {
            DateTime result = new DateTime();
            if (!String.IsNullOrWhiteSpace(page))
            {
                var m = Regex.Match(page, @"value=""last"">.*?анонс(.*?)<", RegexOptions.IgnoreCase);
                if (m.Success && m.Groups.Count > 1)
                    DateTime.TryParse(m.Groups[1].ToString().Trim(), out result);
            }
            return result;
        }

        public static int GetPageCount(string page)
        {
            int result = 0;
            if (!String.IsNullOrWhiteSpace(page))
            {

            }
            return result;
        }
    }
}

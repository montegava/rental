using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Text.RegularExpressions;

namespace Rental
{
    class MoyaReklama
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

        public MoyaReklama(List<DAL.black_list> exclude)
        {
            m_Exclude = exclude;
        }

        /// <summary>
        /// Парсит страницу и формирует список объявлений
        /// </summary>
        /// <param name="pageListContent">Страница</param>
        /// <returns></returns>
        public List<Advert> GetAdvertList(string pageListContent)
        {
            List<Advert> result = new List<Advert>();
            if (!String.IsNullOrEmpty(pageListContent))
            {
                #region 1. Collect all links from page
                Match m = Regex.Match(pageListContent, @"<a[\s]href=""/single_ad_new[.]php[?]id=([\d]*)""", RegexOptions.Singleline);
                List<string> linklist = new List<string>();
                while (m.Success)
                {
                    if (onCheckCansel())
                        return result;
                    if (m.Groups.Count > 1)
                        linklist.Add("http://www.moyareklama.ru/single_ad_new.php?id=" + m.Groups[1]);
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
                        a.ImageIndex = (int)ImageMode.imMoyaReklama;
                        result.Add(a);
                        continue;
                    }
                    #endregion

                    Advert adv = GetAdvertByUrl(url);
                    if (adv != null)
                    {
                        IsBlocked(adv);
                        result.Add(adv);
                    }
                    m = m.NextMatch();
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
            string page = WebPage.LoadPage(url, Encoding.GetEncoding("utf-8"), out error);
            onSetPageCountLoaded(1);
            onSetUIProgress();

            if (string.IsNullOrEmpty(error))
            {
                result = GetAdvert(page);
                if (result != null)
                {
                    result.Link = url;
                    result.ImageIndex = (int)ImageMode.imMoyaReklama;
                    IsBlocked(result);
                }
            }
            return result;
        }

        public Advert GetAdvert(string content)
        {
            Advert result = null;
            if (!String.IsNullOrEmpty(content))
            {
                #region Phones



                #endregion


                var adv_content = Regex.Replace(content, ".*?pv1_description", " ", RegexOptions.Singleline).Trim();
                adv_content = Regex.Replace(adv_content, "pv1_map_block.*", "", RegexOptions.Singleline).Trim();
              
                adv_content = Regex.Replace(adv_content, "<.*?>|&nbsp", "").Trim();
                adv_content = Regex.Replace(adv_content, "[ ]{2,}", " ").Trim();
                adv_content = Regex.Replace(adv_content, "\n", " ", RegexOptions.Singleline);
                adv_content = Regex.Replace(adv_content, "\r\r", " ").Trim();
                adv_content = Regex.Replace(adv_content, @"[\r][\n][\s]*?[\r]", " ", RegexOptions.Singleline).Trim();
                
                List<string> phone_list = new List<string>();
                var m = Regex.Match(adv_content, @"([\d-)(+]{6,})");
                while (m.Success)
                {
                    string phone = m.Groups[1].ToString().Trim();
                    phone = phone.Replace("-", "");
                    phone = phone.Replace("(", "");
                    phone = phone.Replace(")", "");
                    phone = phone.Replace(" ", "");
                    phone_list.Add(phone);
                    m = m.NextMatch();
                }

                result = new Advert();
                result.Content = adv_content;
                result.Phones = phone_list;
                result.ImageIndex = (int)ImageMode.imMoyaReklama;
                return result;

            }
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

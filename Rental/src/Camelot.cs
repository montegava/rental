using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace Rental
{

    class IRR
    {
        public List<Advert> m_adverts = new List<Advert>();


        private List<StopWord> m_Exclude;
        /// <summary>
        /// Для обновления контролла с класса
        /// </summary>
        public event SetUIProgress onSetUIProgress;
        public event SetPageCountForLoad onSetPageCountForLoad;
        public event SetPageCountLoaded onSetPageCountLoaded;
        public event CheckCansel onCheckCansel;

        public IRR(List<StopWord> aExclude, List<Advert> aStars)
        {
            m_Exclude = aExclude;
            m_Stars = aStars;
        }

        private List<Advert> m_Stars;

        /// <summary>
        /// Парсит страницу и формирует список объявлений
        /// </summary>
        /// <param name="content">Страница</param>
        /// <returns></returns>
        public List<Advert> GetAdvertList(string content)
        {
            List<Advert> result = null;

            if (content != null && content.Length > 0)
            {
                Regex r = new Regex(@"<td[\s]class=""tdTxt"">.*?<a[\s]href=""(.*?)"">", RegexOptions.Singleline);
                Match m = r.Match(content);
                List<string> linklist = null;
                while (m.Success)
                {
                    if (onCheckCansel())
                        return result;

                    if (m.Groups.Count > 1)
                    {
                        if (linklist == null)
                            linklist = new List<string>();
                        linklist.Add(Const.CNT_IRR_DOMAIN + m.Groups[1].ToString());
                    }
                    m = m.NextMatch();
                }

                onSetPageCountForLoad(linklist.Count);
                onSetUIProgress();

                foreach (string url in linklist)
                {
                    if (onCheckCansel())
                        return result;

                    if (m_Stars != null)
                        foreach (Advert a in m_Stars)
                        {
                            if (a.link.ToUpper().Trim() == url.ToUpper().Trim())
                            {
                                if (result == null)
                                    result = new List<Advert>();
                                a.ImageIndex = Const.CNT_IMAGE_IRR;
                                result.Add(a);
                                continue;
                            }
                            //return new AdvIRR() { link = a.link, content = a.content, contacts = a.contacts, isStar = a.isStar };
                        }

                    string error;
                    string page = WebPage.LoadPage(url, Encoding.GetEncoding("utf-8"), out error);
                    onSetPageCountLoaded(1);
                    onSetUIProgress();
                    if (error == "")
                    {

                        AdvIRR adv = GetAdvert(page);

                        try
                        {
                            if (adv != null)
                            {
                                adv.link = url;
                                adv.ImageIndex = Const.CNT_IMAGE_IRR;
                                if (adv.contacts != null && m_Exclude != null)
                                {
                                    foreach (StopWord exc in m_Exclude)
                                    {
                                        switch (exc.Type)
                                        {
                                            case TYPE_ID.WORD:
                                                if (adv.content.Contains(exc.Value))
                                                {
                                                    adv.isBlocked = true;
                                                    break;
                                                }
                                                break;
                                            case TYPE_ID.PHONE:
                                                if (adv.contacts.Contains(exc.Value))
                                                {
                                                    adv.isBlocked = true;
                                                    break;
                                                }
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    string uuuu = url;
                                }
                                if (result == null)
                                    result = new List<Advert>();
                                result.Add(adv);

                            }
                        }
                        catch (Exception ex)
                        {
                            string m11 = ex.Message;

                        }
                    }
                }



            }
            return result;
        }

        public AdvIRR GetAdvert(string content)
        {
            if (content == null || content.Length == 0)
            {
                Log.Append("ERROR content is empty");
                return null;
            }

            AdvIRR adv = null;

            Match m = Regex.Match(content, @"<h1>(.*?)<", RegexOptions.Singleline);
            if (m.Success && m.Groups.Count > 1)
            {

                if (adv == null)
                    adv = new AdvIRR();
                adv.content += m.Groups[1].ToString() + "\r\n";
            }


            m = Regex.Match(content, @"<span[\s]class=""advert_text"">(.*?)</span>", RegexOptions.Singleline);
            if (m.Success && m.Groups.Count > 1)
            {

                if (adv == null)
                    adv = new AdvIRR();
                adv.content += m.Groups[1].ToString() + "\r\n";

            }


            string nameConpany = "";
            m = Regex.Match(content, @"<div[\s]class=""padB10[\s]nameConpany"">(.*?)</div>", RegexOptions.Singleline);

            if (m.Success && m.Groups.Count > 1)
            {
                nameConpany = m.Groups[1].ToString();
                if (adv == null)
                    adv = new AdvIRR();
                adv.content += "\r\n" + nameConpany + "\r\n";
            }

            //padB10 wrapMargin


            string phoneContext = "";
            // m = Regex.Match(content, @"<div[\s]class=""wrap"">.*?nameConpany(.*?)redMessage", RegexOptions.Singleline);
            m = Regex.Match(content, @"<div[\s]class=""wrap""[^>]*?>[\s]*<div[\s]class=""padB10[\s]nameConpany"">(.*?)redMessage", RegexOptions.Singleline);
            if (m.Success && m.Groups.Count > 1)
            {
                phoneContext = m.Groups[1].ToString();



            }
            else
            {
                m = Regex.Match(content, @"<div[\s]class=""wrap""[^>]*?>[\s]*<div[\s]class=""padB10[\s]wrapMargin"">(.*?)redMessage", RegexOptions.Singleline);
                if (m.Success && m.Groups.Count > 1)
                {
                    phoneContext = m.Groups[1].ToString();
                }


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

                if (adv == null)
                    adv = new AdvIRR();
                adv.contacts = phone_list;


                r = new Regex(@"<div[\s]class=""wrapIcons"">(.*?)</div>");
                m = r.Match(phoneContext);
                string data = "";
                while (m.Success)
                {
                    data += "\r\n" + m.Groups[0].ToString().Trim();
                    m = m.NextMatch();
                }

                r = new Regex("<.*?>|&nbsp", RegexOptions.Singleline);
                string adv_content = r.Replace(data, "").Trim();
                if (adv == null)
                    adv = new AdvIRR();
                adv.content += adv_content;
            }
            else
            {
                Log.Append("\tERROR on parse contacts");
            }

            if (adv != null && adv.content != null && adv.content.Length > 0)
            {
                Regex r = new Regex(@"<br[\s]*/>", RegexOptions.Singleline);
                adv.content = r.Replace(adv.content, "\r\n").Trim();
            }

            //<br />

            if (adv != null)
                adv.ImageIndex = Const.CNT_IMAGE_MOYAREKLAMA;
            return adv;

            return adv;

        }

        public void AddRegion(List<Advert> adverts)
        {
            if (adverts != null)
                m_adverts.AddRange(adverts);
        }
    }

    class MoyaReklama
    {

        public List<Advert> m_adverts = new List<Advert>();

        private List<StopWord> m_Exclude;

        public MoyaReklama(List<StopWord> aExclude, List<Advert> aStars)
        {
            m_Exclude = aExclude;
            m_Stars = aStars;
        }

        /// <summary>
        /// Парсит страницу и формирует список объявлений
        /// </summary>
        /// <param name="content">Страница</param>
        /// <returns></returns>
        public List<Advert> GetAdvertList(string content)
        {
            List<Advert> result = null;

            if (content != null && content.Length > 0)
            {
                string pattern = @"<a[\s]href=""/single_ad_new[.]php[?]id=([\d]*)"".*?<div.*?class=""sa_txt"">(.*?)sa_sign"">";
                // string pattern = @"<a[\s]href=""/single_ad[.]php[?]id=([\d]*)"".*?<td[\s]valign=""top""[\s]class=""oatext"">(.*?)</td>";
                Regex r = new Regex(pattern, RegexOptions.Singleline);
                Match m = r.Match(content);
                while (m.Success)
                {
                    AdvMoyaReklama adv = GetAdvert(m.Groups[0].ToString());
                    if (adv != null)
                    {
                        if (m_Exclude != null)
                        {
                            foreach (StopWord exc in m_Exclude)
                            {
                                switch (exc.Type)
                                {
                                    case TYPE_ID.WORD:
                                        if (adv.content.Contains(exc.Value))
                                        {
                                            adv.isBlocked = true;
                                            break;
                                        }
                                        break;
                                    case TYPE_ID.PHONE:
                                        if (adv.contacts.Contains(exc.Value))
                                        {
                                            adv.isBlocked = true;
                                            break;
                                        }
                                        break;
                                }
                            }
                        }

                        if (result == null)
                            result = new List<Advert>();
                        result.Add(adv);

                    }
                    m = m.NextMatch();
                }
            }
            return result;
        }
        private List<Advert> m_Stars;
        public AdvMoyaReklama GetAdvert(string content)
        {
            if (content == null)
            {
                Log.Append("ERROR content is empty");
                return null;
            }

            Regex r = new Regex(@"<a[\s]href=""(.*?)""", RegexOptions.Singleline);
            Match m = r.Match(content);
            if (m.Success && m.Groups.Count > 1)
            {
                string adv_link = Const.CNT_MOYAREKLAMA_DOMAIN + m.Groups[1].ToString().Trim();

                if (m_Stars != null)
                    foreach (Advert a in m_Stars)
                    {
                        if (a.link.ToUpper().Trim() == adv_link.ToUpper().Trim())
                            return new AdvMoyaReklama() { link = a.link, content = a.content, contacts = a.contacts, isStar = a.isStar, ImageIndex = Const.CNT_IMAGE_MOYAREKLAMA };
                    }


                r = new Regex("<.*?>|&nbsp");
                string adv_content = r.Replace(content, "").Trim();

                r = new Regex("[ ]{2,}");
                adv_content = r.Replace(adv_content, " ").Trim();


                List<string> phone_list = new List<string>();
                r = new Regex(@"([\d-)(+]{6,})");
                m = r.Match(adv_content);

                if (!m.Success)
                {
                    string error;
                    string advPage = WebPage.LoadPage(adv_link, Encoding.GetEncoding("utf-8"), out error);                
                    r = new Regex(@"pv1_phones.*?([\d-)(+]{6,})", RegexOptions.Singleline);
                    m = r.Match(advPage);

                }

                while (m.Success && m.Groups.Count > 1)
                {
                    string phone = m.Groups[1].ToString().Trim();
                    phone = phone.Replace("-", "");
                    phone = phone.Replace("(", "");
                    phone = phone.Replace(")", "");
                    phone = phone.Replace(" ", "");
                    phone_list.Add(phone);
                    m = m.NextMatch();
                }

                AdvMoyaReklama adv = new AdvMoyaReklama();
                adv.link = adv_link;
                adv.content = adv_content;
                adv.contacts = phone_list;
                adv.ImageIndex = Const.CNT_IMAGE_MOYAREKLAMA;
                return adv;
            }

            return null;

        }

        public void AddRegion(List<Advert> adverts)
        {
            if (adverts != null)
                m_adverts.AddRange(adverts);
        }
    }


    class Camelot
    {
        public List<Advert> m_adverts = new List<Advert>();

        public Advert this[int index]
        {
            get
            {
                if (m_adverts != null && m_adverts.Count > 0 && index >= 0 && index < m_adverts.Count)
                {
                    return m_adverts[index];
                }
                return null;
            }
            set
            {
                if (value != null && m_adverts != null && m_adverts.Count > 0 && index >= 0 && index < m_adverts.Count)
                    m_adverts[index] = value;
            }

        }

        public int Count
        {
            get
            {
                if (m_adverts != null)
                    return m_adverts.Count;
                return 0;
            }

        }

        private List<StopWord> m_Exclude;
        private List<Advert> m_Stars;

        public Camelot(List<StopWord> aExclude, List<Advert> aStars)
        {
            m_Exclude = aExclude;
            m_Stars = aStars;
        }

        public AdvCamelot GetAdvert(string content)
        {
            if (content == null)
            {
                Log.Append("ERROR content is empty");
                return null;
            }

            Regex r = new Regex(@"<a[\s]href=""(.*?)""", RegexOptions.Singleline);
            Match m = r.Match(content);
            if (m.Success && m.Groups.Count > 1)
            {
                string adv_link = Const.CNT_CAMELOT_DOMAIN + m.Groups[1].ToString().Trim();

                if (m_Stars != null)
                    foreach (Advert a in m_Stars)
                    {
                        if (a.link.ToUpper().Trim() == adv_link.ToUpper().Trim())
                            return new AdvCamelot() { link = a.link, content = a.content, contacts = a.contacts, isStar = a.isStar };
                    }


                r = new Regex("<.*?>|&nbsp");
                string adv_content = r.Replace(content, "").Trim();

                List<string> phone_list = new List<string>();
                r = new Regex(@"[\d-)(+]{6,}");
                m = r.Match(adv_content);
                while (m.Success)
                {
                    string phone = m.Groups[0].ToString().Trim();
                    phone = phone.Replace("-", "");
                    phone = phone.Replace("(", "");
                    phone = phone.Replace(")", "");
                    phone_list.Add(phone);
                    m = m.NextMatch();
                }

                AdvCamelot adv = new AdvCamelot();
                adv.link = adv_link;
                adv.content = adv_content;
                adv.contacts = phone_list;
                adv.ImageIndex = Const.CNT_IMAGE_CAMELOT;
                return adv;
            }

            return null;

        }

        /// <summary>
        /// Для обновления контролла с класса
        /// </summary>
        public event SetUIProgress onSetUIProgress;
        public event SetPageCountForLoad onSetPageCountForLoad;
        public event SetPageCountLoaded onSetPageCountLoaded;
        public event CheckCansel onCheckCansel;

        public List<Advert> GetAdvertList(string content, DateTime posterDate)
        {
            List<Advert> result = null;

            if (content != null && content.Length > 0)
            {
                Regex r = new Regex(@"<div[\s]class=""ads-content-div"">(.*?)</div>", RegexOptions.Singleline);
                Match m = r.Match(content);
                if (m.Success && m.Groups.Count > 1)
                {
                    while (m.Success)
                    {
                        AdvCamelot adv = GetAdvert(m.Groups[0].ToString());
                        if (adv != null)
                        {
                            if (!isOld(adv, posterDate))
                            {
                                isBlocked(adv);
                                if (!adv.isBlocked && !adv.isStar)
                                {
                                    if (onCheckCansel())
                                        return result;

                                    onSetPageCountForLoad(1);
                                    onSetUIProgress();
                                    //Надо грузиться вглубь
                                    string error;
                                    string page = WebPage.LoadPage(adv.link, Encoding.GetEncoding("windows-1251"), out error);
                                    onSetPageCountLoaded(1);
                                    onSetUIProgress();
                                    if (error == "")
                                    {
                                        adv.GetPhones(page);
                                        // повторно проверяем    
                                        isBlocked(adv);
                                    }
                                }

                                if (result == null)
                                    result = new List<Advert>();
                                result.Add(adv);
                            }
                        }
                        m = m.NextMatch();
                    }
                }
            }
            return result;
        }

        private bool isOld(AdvCamelot adv, DateTime posterDate)
        {
            if (posterDate != DateTime.MinValue && adv != null && !String.IsNullOrEmpty(adv.content))
            {
                Match m = Regex.Match(adv.content, "опубликовано в газете(.*)");
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

        private bool isBlocked(AdvCamelot adv)
        {
            if (m_Exclude != null && adv != null)
            {
                foreach (StopWord exc in m_Exclude)
                {
                    switch (exc.Type)
                    {
                        case TYPE_ID.WORD:
                            if (adv.content != null && adv.content.IndexOf(exc.Value) > 0)
                            {
                                adv.isBlocked = true;
                                return true;
                            }
                            break;
                        case TYPE_ID.PHONE:
                            if (adv.contacts.Contains(exc.Value))
                            {
                                adv.isBlocked = true;
                                return true;
                            }
                            break;
                    }
                }
            }
            return false;
        }

        public void AddRegion(List<Advert> adverts)
        {
            if (adverts != null)
                m_adverts.AddRange(adverts);
        }

        public static bool isExclude(Advert adv, List<string> aExclude)
        {

            if (aExclude != null)
            {
                if (adv != null && adv.contacts != null && adv.contacts.Count > 0)
                {
                    foreach (string exc in aExclude)
                    {
                        if (adv.contacts.Contains(exc))
                        {
                            return true;
                        }
                    }

                }

            }
            return false;

        }

    }
}

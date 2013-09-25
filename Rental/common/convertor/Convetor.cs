using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using  System.Windows.Forms;

namespace Rental
{
    public class KeyValue
    {
        public int Key { get; set; }
        public string Value { get; set; }

    }

    public static class Convetor
    {
        public static void AdvertToListView(List<Advert> advertList, ListView allAdvertsControl, ListView starredAdvertsControl)
        {
            if (allAdvertsControl != null)
            {

                #region Save current cursor position
                int cursorPossitionAll = 0;
                int cursorPossitionFiltered = 0;
                if (allAdvertsControl.SelectedItems.Count > 0)
                    cursorPossitionAll = allAdvertsControl.Items.IndexOf(allAdvertsControl.SelectedItems[0]);
                if (starredAdvertsControl.SelectedItems.Count > 0)
                    cursorPossitionFiltered = starredAdvertsControl.Items.IndexOf(starredAdvertsControl.SelectedItems[0]);
                #endregion

                allAdvertsControl.BeginUpdate();
                starredAdvertsControl.BeginUpdate();

                allAdvertsControl.Items.Clear();
                starredAdvertsControl.Items.Clear();

                try
                {
                    if (advertList != null)
                    {


                        for (int i = 0; i < advertList.Count; i++)
                        {
                            ListViewItem item = new ListViewItem((allAdvertsControl.Items.Count + 1).ToString());
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, advertList[i].PhonesAsString()));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, advertList[i].Content));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, advertList[i].Link));
                            item.Tag = advertList[i];


                            if (advertList[i].IsStar)
                                item.ImageIndex = (int)ImageMode.imStar;
                            if (advertList[i].IsBlocked)
                                item.StateImageIndex = advertList[i].ImageIndex + 1;
                            else
                                item.StateImageIndex = advertList[i].ImageIndex;

                            allAdvertsControl.Items.Add(item);

                            if (!advertList[i].IsBlocked && !advertList[i].IsStar)
                            {
                                ListViewItem itemFilter = new ListViewItem((starredAdvertsControl.Items.Count + 1).ToString());
                                itemFilter.SubItems.Add(new ListViewItem.ListViewSubItem(itemFilter, advertList[i].HasPhoto ? "+" : ""));


                                itemFilter.SubItems.Add(new ListViewItem.ListViewSubItem(itemFilter, advertList[i].PhonesAsString()));
                                itemFilter.SubItems.Add(new ListViewItem.ListViewSubItem(itemFilter, advertList[i].Content));
                                itemFilter.SubItems.Add(new ListViewItem.ListViewSubItem(itemFilter, advertList[i].Link));
                                itemFilter.Tag = advertList[i];

                                if (advertList[i].IsStar)
                                    itemFilter.ImageIndex = (int)ImageMode.imStar;
                                itemFilter.StateImageIndex = advertList[i].ImageIndex;
                                starredAdvertsControl.Items.Add(itemFilter);
                            }

                        }

                        if (cursorPossitionAll < allAdvertsControl.Items.Count && cursorPossitionAll >= 0)
                            allAdvertsControl.Items[cursorPossitionAll].Selected = true;
                        else
                        {
                            cursorPossitionAll--;
                            if (cursorPossitionAll < allAdvertsControl.Items.Count && cursorPossitionAll >= 0)
                                allAdvertsControl.Items[cursorPossitionAll].Selected = true;
                        }

                        if (cursorPossitionFiltered < starredAdvertsControl.Items.Count && cursorPossitionFiltered >= 0)
                            starredAdvertsControl.Items[cursorPossitionFiltered].Selected = true;
                        else
                        {
                            cursorPossitionFiltered--;
                            if (cursorPossitionFiltered < starredAdvertsControl.Items.Count && cursorPossitionFiltered >= 0)
                                starredAdvertsControl.Items[cursorPossitionFiltered].Selected = true;
                        }

                    }
                }
                finally
                {

                    allAdvertsControl.EndUpdate();
                    starredAdvertsControl.EndUpdate();
                }

            }

        }




        public static string ParcingModeToString(ParcingMode mode)
        {
            switch (mode)
            {
                case ParcingMode.All:
                    return "--все сайты--";

                case ParcingMode.CamelotFlat:
                    return "Камелот - квартиры";
                case ParcingMode.CamelotHouse:
                    return "Камелот - дома";

                case ParcingMode.MoyaReklamaFlat:
                    return "Моя реклама снять квартиру";
                case ParcingMode.MoyaReklamaRoom:
                    return "Моя реклама снять комнату";
                case ParcingMode.MoyaReklamaHouse:
                    return "Моя реклама снять дом";

                case ParcingMode.IRR:
                    return "Из рук в руки";

                case ParcingMode.AvitoFlat:
                    return "Avito - квартиры";
                case ParcingMode.pmAvitoHouse:
                    return "Avito - дома";

                case ParcingMode.SlandoFlat:
                    return "Slando - квартиры";
                case ParcingMode.SlandoHouse:
                    return "Slando - дома";
                default:
                    throw new Exception(string.Format("Неизвестный тип [ParcingMode] = {0}", (int)mode));
            }
        }

        public static List<KeyValue> GetParcingModeDataSource()
        {
            var result = new List<KeyValue>();

            foreach (ParcingMode item in Enum.GetValues(typeof(ParcingMode)))
            {
                result.Add(new KeyValue() { Key = (int)item, Value = ParcingModeToString(item) });
            }

            return result;
        }

        public static Advert Flat2Advert(DAL.flat_info flat)
        {
            Advert result = null;
            if (flat != null)
            {
                result = new Advert();
                result.Link = flat.LINK;
                result.Content = flat.CONTENT;
                result.Phones = (from ph in ((flat.PHONE.ToString()).Split(new Char[] { ';' })) where ph.Trim().Length > 0 select ph.Trim()).ToList<string>();
            }
            return result;
        }
    }
}

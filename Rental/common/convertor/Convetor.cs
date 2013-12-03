using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Windows.Forms;
using RentalCommon;

namespace Rental
{
    public class KeyValue
    {
        public int Key { get; set; }
        public string Value { get; set; }

    }

    public static class Convetor
    {
        public static string FieldToString(Fields field)
        {
            switch (field)
            {
                case Fields.ID:
                    return "№";
                case Fields.DATA:
                    return "ДАТА ДОБВЛЕНИЯ";
                case Fields.ROOM_COUNT:
                    return "КОЛИЧЕСТВО КОМНАТ";
                case Fields.ADDRESS:
                    return "АДРЕС";
                case Fields.FLOOR:
                    return "ЭТАЖ";
                case Fields.BATH_UNIT:
                    return "САНУЗЕЛ";
                case Fields.BUILD:
                    return "ВИД ДОМА";
                case Fields.FURNITURE:
                    return "МЕБЕЛЬ";
                case Fields.STATE:
                    return "СОСТОЯНИЕ";
                case Fields.NAME:
                    return "ФИО";
                case Fields.PRICE:
                    return "ЦЕНА";
                case Fields.PHONE:
                    return "ТЕЛЕФОН";
                case Fields.COMMENT:
                    return "КОММЕНТАРИЙ";
                case Fields.CONTENT:
                    return "СОДЕРЖАНИЕ";
                case Fields.LINK:
                    return "ССЫЛКА";
                case Fields.TERM:
                    return "СРОК СДАЧИ";
                case Fields.RENT_FROM:
                    return "СДАЕТСЯ С";
                case Fields.RENT_TO:
                    return "СДАЕТСЯ ПО";
                case Fields.LESSOR:
                    return "КТО СДАЛ";
                case Fields.FRIDGE:
                    return "ХОЛОДИЛЬНИК";
                case Fields.TV:
                    return "ТЕЛЕВИЗОР";
                case Fields.WASHER:
                    return "СТИРАЛЬНАЯ МАШИНА";
                case Fields.COOLER:
                    return "КОНДИЦИОНЕР";
                case Fields.REGION:
                    return "РЕГИОН";
                case Fields.EMAIL:
                    return "EMAIL";
                case Fields.CATEGORY:
                    return "КАТЕГОРИЯ";
                case Fields.TYPE:
                    return "ТИП";
                case Fields.PAYMENT:
                    return "ОПЛАТА";
                default:
                    throw new Exception("Unknown condition " + field.ToString());
            }
        }

        public static string ConditionToString(FilterConditions condition)
        {
            switch (condition)
            {
                case FilterConditions.MORE:
                    return "больше";
                case FilterConditions.LESS:
                    return "меньше";
                case FilterConditions.EQUAL:
                    return "равно";
                case FilterConditions.CONTAIN:
                    return "содержит";
                default:
                    throw new Exception("Unknown condition " + condition.ToString());
            }
        }

        public static FilterConditions[] GetConditions(Fields field)
        {

            var result = new List<FilterConditions>();
            switch (field)
            {
                case Fields.ID:
                case Fields.ROOM_COUNT:
                case Fields.FLOOR:
                case Fields.DATA:
                case Fields.RENT_FROM:
                case Fields.RENT_TO:
                    return new FilterConditions[] { FilterConditions.MORE, FilterConditions.LESS, FilterConditions.EQUAL };

                case Fields.FRIDGE:
                case Fields.TV:
                case Fields.WASHER:
                case Fields.COOLER:
                case Fields.BATH_UNIT:
                case Fields.BUILD:
                case Fields.STATE:
                case Fields.TERM:
                case Fields.LESSOR:
                case Fields.REGION:
                case Fields.CATEGORY:
                case Fields.TYPE:
                case Fields.PAYMENT:
                    return new FilterConditions[] { FilterConditions.EQUAL };


                case Fields.ADDRESS:
                case Fields.FURNITURE:
                case Fields.NAME:
                case Fields.PRICE:
                case Fields.PHONE:
                case Fields.COMMENT:
                case Fields.CONTENT:
                case Fields.LINK:
                case Fields.EMAIL:
                    return new FilterConditions[] { FilterConditions.CONTAIN, FilterConditions.EQUAL };

                default:
                    throw new Exception("Unknown condition " + field.ToString());
            }

        }


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

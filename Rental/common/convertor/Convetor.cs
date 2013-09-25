using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace Rental
{
    public class KeyValue
    {
        public int Key { get; set; }
        public string Value { get; set; }

    }

    public static class Convetor
    {
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

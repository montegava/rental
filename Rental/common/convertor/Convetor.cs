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
                case ParcingMode.pmAll:
                    return "--все сайты--";
                case ParcingMode.pmCamelotFlat:
                    return "Камелот - квартиры";
                case ParcingMode.pmCamelotHouse:
                    return "Камелот - дома";

                case ParcingMode.pmMoyaReklama1:
                    return "Моя реклама снять_квартиру";
                case ParcingMode.pmMoyaReklama2:
                    return "Моя реклама снять_комнату";
                case ParcingMode.pmMoyaReklama3:
                    return "Моя реклама снять_дом";

                case ParcingMode.pmIRR:
                    return "Из рук в руки";
                case ParcingMode.pmAvitoFlat:
                    return "Avito - квартиры";
                case ParcingMode.pmAvitoHouse:
                    return "Avito - дома";
                case ParcingMode.pmSlandoFlat:
                    return "Slando - квартиры";
                case ParcingMode.pmSlandoHouse:
                    return "Slando - дома";
            }

            return String.Empty;


        }

        public static List<KeyValue> GetParcingModeDataSource()
        {
            var result = new List<KeyValue>();

            foreach (ParcingMode item in Enum.GetValues(typeof(ParcingMode))  )
            {
                result.Add(new KeyValue (){ Key = (int)item, Value = ParcingModeToString(item) });
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public enum BlackWordType
    {
        PHONE = 0,
        WORD = 1
    }

    public enum Category
    {
        //-- неизвестно --
        unknown,
        //Квартира
        flat,
        //Комната
        room,
        //Дом
        house,
        //Помещение, офис
        office
    }

    public enum Region
    {
        // -- неизвестно --
        unknown,
        //Коминтерновский
        komintern,
        //Центральный
        central,
        //Ж/Д
        railway,
        //Левобережный
        leftside,
        //Советский
        soviet,
        //Ленинский
        lenin,
    }

    public enum Type
    {
        // -- неизвестно --
        unknown,
        //Сдам
        rent,
        //Сниму
        let,
    }

    public enum HouseType
    {
        // -- неизвестно --
        unknown,
        //кирпичный
        brick,
        //панельный
        prefabricated,
        //коттедж
        cottage,
        //часть дома
        part,
    }

    public enum RentType
    {
        // -- неизвестно --
        unknown,
        //на длительный срок
        longterm,
        //на сутки
        byday,
    }

    public enum Payment
    {
        //помесячно
        bymonth,
        //поквартально
        byquarter,
    }

    public static class EnumConvertor
    {
        public static string Convert(Enum e)
        {
            if (e is Category)
                return Convert((Category)e);
            else if (e is Region)
                return Convert((Region)e);
            else if (e is Type)
                return Convert((Type)e);
            else if (e is HouseType)
                return Convert((HouseType)e);
            else if (e is RentType)
                return Convert((RentType)e);
            else if (e is Payment)
                return Convert((Payment)e);
            else
                return e.ToString();
        }


        public static string Convert(Payment t)
        {
            switch (t)
            {
                case Payment.bymonth:
                    return "помесячно";
                case Payment.byquarter:
                    return "поквартально";
            }
            return string.Empty;
        }

        public static string Convert(RentType t)
        {
            switch (t)
            {
                case RentType.unknown:
                    return "-- неизвестно --";
                case RentType.longterm:
                    return "на длительный срок";
                case RentType.byday:
                    return "на сутки";
            }
            return string.Empty;
        }

        public static string Convert(HouseType t)
        {
            switch (t)
            {
                case HouseType.unknown:
                    return "-- неизвестно --";
                case HouseType.brick:
                    return "кирпичный";
                case HouseType.prefabricated:
                    return "панельный";
                case HouseType.cottage:
                    return "коттедж";
                case HouseType.part:
                    return "часть дома";
            }
            return string.Empty;
        }

        public static string Convert(Type t)
        {
            switch (t)
            {
                case Type.unknown:
                    return "-- неизвестно --";
                case Type.rent:
                    return "Сдам";
                case Type.let:
                    return "Сниму";
            }
            return string.Empty;
        }

        public static string Convert(Region r)
        {
            switch (r)
            {
                case Region.unknown:
                    return "-- неизвестно --";
                case Region.komintern:
                    return "Коминтерновский";
                case Region.central:
                    return "Центральный";
                case Region.railway:
                    return "Ж/Д";
                case Region.leftside:
                    return "Левобережный";
                case Region.soviet:
                    return "Советский";
                case Region.lenin:
                    return "Ленинский";
            }
            return string.Empty;
        }

        public static string Convert(Category c)
        {
            switch (c)
            {
                case Category.unknown:
                    return "-- неизвестно --";
                case Category.flat:
                    return "Квартира";
                case Category.room:
                    return "Комната";
                case Category.house:
                    return "Дом";
                case Category.office:
                    return "Помещение, офис";
            }
            return string.Empty;

        }
    }



}

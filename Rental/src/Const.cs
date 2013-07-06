using System;
using System.Collections.Generic;
using System.Text;

namespace Rental
{
    class Const
    {

        // Путь к лог-файлу
        public const string CNT_LOG_FILE = "rental.log";

        #region Camelot
        /// Объявления газеты + Камелот: Квартиры + Все за неделю + Любая длительность
        public const string CNT_CAMELOT_FLAT_URL = "http://www.cmlt.ru/ads?leasPeriod=4886&rubric=1942&type=%EF%F0%E5%E4%EB%EE%E6%E5%ED%E8%E5&source=paper&pub=12122112&order=abc";

        public const string CNT_CAMELOT_BUILD_URL = "http://www.cmlt.ru/ads?rubric=2103&type=%EF%F0%E5%E4%EB%EE%E6%E5%ED%E8%E5&viewType=list&source=all&period=0&order=date";

        /// <summary>
        /// Домен
        /// </summary>
        public const string CNT_CAMELOT_DOMAIN = "http://www.cmlt.ru";

        #endregion

        #region Moya reklama


        public const string CNT_MOYAREKLAMA_URL_1 = "http://www.moyareklama.ru/%D0%92%D0%BE%D1%80%D0%BE%D0%BD%D0%B5%D0%B6/%D1%81%D0%BD%D1%8F%D1%82%D1%8C_%D0%BA%D0%B2%D0%B0%D1%80%D1%82%D0%B8%D1%80%D1%83/%D0%B2%D1%81%D0%B5/8/{0}";

        public const string CNT_MOYAREKLAMA_URL_2 = "http://www.moyareklama.ru/%D0%92%D0%BE%D1%80%D0%BE%D0%BD%D0%B5%D0%B6/%D1%81%D0%BD%D1%8F%D1%82%D1%8C_%D0%BA%D0%BE%D0%BC%D0%BD%D0%B0%D1%82%D1%83/%D0%B2%D1%81%D0%B5/8/{0}";

        public const string CNT_MOYAREKLAMA_URL_3 = "http://www.moyareklama.ru/%D0%92%D0%BE%D1%80%D0%BE%D0%BD%D0%B5%D0%B6/%D1%81%D0%BD%D1%8F%D1%82%D1%8C_%D0%B4%D0%BE%D0%BC/%D0%B2%D1%81%D0%B5/8/{0}";

        /// <summary>
        /// Сайт moyareklama.ru г. Воронеж
        /// </summary>
        public const string CNT_MOYAREKLAMA_URL = "http://www.moyareklama.ru/Воронеж/снять_квартиру";

        /// <summary>
        /// Регионы на сайте моя реклама г. Воронеж. Необходимы для обходо ссылок
        /// </summary>
        public static string[] CNT_MOYAREKLAMA_REGIONS = new[] { "железнодорожный_район", "коминтерновский_район", "левобережный_р-н", "ленинский_р-н", "советский_р-н", "центральный_р-н" };

        /// <summary>
        /// Домен
        /// </summary>
        public const string CNT_MOYAREKLAMA_DOMAIN = "http://www.moyareklama.ru";
        #endregion

        #region IRR


        /// <summary>
        /// Сайт irr.ru г. Воронеж
        /// </summary>
        public const string CNT_IRR_URL = "http://voronezh.irr.ru/real-estate/rent/search/page_len80/";




        /// <summary>
        /// Домен
        /// </summary>
        public const string CNT_IRR_DOMAIN = "http://voronezh.irr.ru";
        #endregion


        #region Avito
        public const string CNT_AVITO_FLAT_URL = "http://www.avito.ru/catalog/kvartiry-24/voronezh-625810/params.201_1060.504_5256";
        public const string CNT_AVITO_BUILD_URL = "http://www.avito.ru/catalog/doma_dachi_kottedzhi-25/voronezh-625810/params.202_1065";
        #endregion

        #region Slando
        public const string CNT_SLANDO_FLAT_URL = "http://voronezh.vor.slando.ru/nedvizhimost/arenda-kvartir/dolgosrochnaya-arenda-kvartir/";
        public const string CNT_SLANDO_BUILD_URL = "http://voronezh.vor.slando.ru/nedvizhimost/arenda-domov/";
        #endregion

    }
}

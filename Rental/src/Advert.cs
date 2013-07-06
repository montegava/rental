using System;
using System.Collections.Generic;
using System.Text;

namespace Rental
{
    // Класс объявления
    public class Advert
    {
        /// <summary>
        /// Ссылка на реальное объявление
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Полное содержание объявления
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Заблокировано по телефону или содержанию
        /// </summary>
        public bool IsBlocked { get; set; }

        /// <summary>
        /// Добавлено в избранные
        /// </summary>
        public bool IsStar { get; set; }

        /// <summary>
        /// Список контактных телефонов
        /// </summary>
        public List<string> Phones { get; set; }

        public string PhonesAsString()
        {
            var result = String.Empty;
            if (Phones != null && Phones.Count > 0)
                result = String.Join(";", Phones.ToArray());
            return result;
        }

        public bool HasPhoto { get; set; }

        /// <summary>
        /// Изображение
        /// </summary>
        public int ImageIndex { get; set; }

        public Advert()
        {
            Phones = new List<string>();
        }

        public Advert(string link, string content, List<string> phones)
        {
            Link = link;
            Content = content;
            Phones = phones;
            HasPhoto = false;
        }
    }
}

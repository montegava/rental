using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using log4net;


namespace DAL
{
    public static class ImageManager
    {
        public static ILog errorLog = LogManager.GetLogger("ErrorLogger");

        public static List<image_list> ImagesByFlatId(int flatId)
        {
            var context = WcfOperationContext.Current.Context;
            return context.image_list.Where(im => im.FLAT_ID == flatId).ToList();
        }

        public static void ImageUpdate(image_list[] images, int flatId)
        {
            if (images != null && images.Any())
            {

                var context = WcfOperationContext.Current.Context;

               var pathes = images.Select(i => i.IMAGE_PATH).ToList();

                var oldImages = context.image_list.Where(i => i.FLAT_ID == flatId && !pathes.Contains(i.IMAGE_PATH));
                foreach (var item in oldImages)
                {
                    context.image_list.Remove(item);

                    //var im = pathes.Where(i => i == item.IMAGE_PATH).FirstOrDefault();
                    //if (im != null)
                    pathes.Remove(item.IMAGE_PATH);
                }
                foreach (var item in images.Where(i => pathes.Contains(i.IMAGE_PATH)))
                    context.image_list.Add(item);
                context.SaveChanges();
            }

        }
    }
}

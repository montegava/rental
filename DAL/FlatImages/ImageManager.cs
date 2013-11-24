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
            var context = WcfOperationContext.Current.Context;
            var imgIds = images.Select(i => i.ID).ToArray();
            var toDelete = context.image_list.Where(i => i.FLAT_ID == flatId && !imgIds.Contains(i.ID)).ToArray();
            foreach (image_list img in toDelete)
                context.image_list.Remove(img);
            foreach (var img in images)
                if (context.image_list.FirstOrDefault(i => i.ID == img.ID) == null)
                {
                    img.FLAT_ID = flatId;
                    context.image_list.Add(img);
                }
            context.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using log4net;


namespace DAL
{
    public static class FlatImageManager
    {
        public static ILog errorLog = LogManager.GetLogger("ErrorLogger");

        public static List<images> GetFlatImagesByFlatId(int flatId)
        {
            var context =  WcfOperationContext.Current.Context;
            return context.images.Where(im => im.FLAT_ID == flatId).ToList();
        }

        public static int AddFlatImage(Int32 flatId, string imagePath)
        {
            var context = WcfOperationContext.Current.Context;
            context.images.Add(new images() { ID = -1, FLAT_ID = flatId, IMAGE_PATH = imagePath });
            return context.SaveChanges();
        }

        public static int DeleteFlatImage(int imageId)
        {
            var context = WcfOperationContext.Current.Context;
            var img = context.images.FirstOrDefault(im=>im.ID == imageId);
            if (img != null)
            {
                context.images.Remove(img);
                return context.SaveChanges();
            }
            return 0;
        }

        public static int DeleteAllByFlatId(int flatId)
        {
            var context = WcfOperationContext.Current.Context;
            foreach (var item in context.images.Where(im => im.FLAT_ID == flatId))
                 context.images.Remove(item);
            return context.SaveChanges();
        }

    }
}

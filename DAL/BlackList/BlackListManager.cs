using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    public static class BlackListManager
    {

        public static  black_list[] BlackListAll()
        {
           return WcfOperationContext.Current.Context.black_list.ToArray();
        }

        public static black_list BlackListById(int id)
        {
            return WcfOperationContext.Current.Context.black_list.Where(b=>b.ID == id).FirstOrDefault();
        }

        public static void BlackListDelete(int id)
        {
            var context = WcfOperationContext.Current.Context;
            var bl = context.black_list.Where(b => b.ID == id).FirstOrDefault();
            if (bl != null)
                context.black_list.Remove(bl);
        }

        public static int BlackListAdd(black_list blackList)
        {
            var context = WcfOperationContext.Current.Context;
            var finded = context.black_list.Where(b => b.ID == blackList.ID).FirstOrDefault();
            if (finded != null)
            {
                finded.STOP = blackList.STOP;
                finded.COMMENT = blackList.COMMENT;
                finded.TYPE_ID = blackList.TYPE_ID;
            }
            else
                context.black_list.Add(blackList);
            context.SaveChanges();
            return blackList.ID;
        }
    }
}

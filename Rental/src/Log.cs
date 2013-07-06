using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Rental
{
    static class Log
    {
        private static string m_logpath = Const.CNT_LOG_FILE;

        public static void Append(string apath, string amsg)
        {
            if (apath == null)
                return;
            string msg = DateTime.Now + "\t" + amsg;
            try
            {
                if (!File.Exists(apath))
                {
                    using (StreamWriter sw = File.CreateText(apath))
                    {
                        sw.WriteLine(msg);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(apath))
                    {
                        sw.WriteLine(msg);
                    }
                }
            }
            catch (Exception)
            {
                return;
            }

        }


        public static void Append(string amsg)
        {
            Append(m_logpath,amsg);
        }

      

    }
}

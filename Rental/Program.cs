using System;
using System.Collections.Generic;
using System.Windows.Forms;


using log4net;
using log4net.Config;


namespace Rental
{
    static class Program
    {
        public static readonly ILog testLogger = LogManager.GetLogger("TestApplication"); 
            
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}

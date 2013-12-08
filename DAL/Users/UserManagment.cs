using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public static class UserManagment
    {
        public static bool Login(string login, string password)
        {
            return WcfOperationContext.Current.Context.users.Any(u => u.login == login && u.password == password);
        }

    }
}

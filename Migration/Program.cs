using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Migration
{
    class Program
    {
        private static string GetValue(string inputstring)
        {
            int value = -1;
            if (!int.TryParse(inputstring, out value))
            {
                var m = Regex.Match(inputstring, @".*?([\d]+)");
                if (m.Success && m.Groups.Count > 1)
                    int.TryParse(m.Groups[1].ToString(), out value);
            }

            return value > 0 ? value.ToString() : "";
        }


        static void Main(string[] args)
        {
            var context = new rentaMigration();

            foreach (var f in context.flat_info.ToArray())
            {
                f.ROOM_COUNT = GetValue(f.ROOM_COUNT);
                f.FLOOR = GetValue(f.FLOOR);
                f.PRICE = GetValue(f.PRICE);
            }

            context.SaveChanges();




        }
    }
}

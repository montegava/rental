using Omu.ValueInjecter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RentalCommon
{
    public static class ValueInjecterExtensions
    {
        public static IList<T> InjectInto<T>(this ValueInjecter injecter, IList<T> target, IEnumerable source) where T : new()
        {
            foreach (var sourceItem in source)
            {
                var targetItem = new T();
                targetItem.InjectFrom<NameConventionInjection>(sourceItem);
                target.Add(targetItem);
            }

            return target;
        }

        public static T InjectIntoNew<T>(this object o) where T : new()
        {
            return (T)new T().InjectFrom<NameConventionInjection>(o);
        }
    }
}

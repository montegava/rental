using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Omu.ValueInjecter;

namespace RentalCommon
{
    public class NameConventionInjection : ConventionInjection
    {
        protected override bool Match(ConventionInfo c)
        {
            return Normalize(c.SourceProp.Name) == Normalize(c.TargetProp.Name);
        }


        private string Normalize(string name)
        {
            return name.Replace("_", "").ToLower();
        }

        protected Type GetEnumerableItemType(Type source)
        {
            if (source.GetInterface("IEnumerable") != null)
            {
                Type targetEnumType = typeof(object);
                if (source.IsGenericType)
                {
                    if (source.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    {
                        return source.GetGenericArguments()[0];
                    }

                    var targetEnumInterface = source.GetInterface("IEnumerable`1");

                    if (targetEnumInterface != null)
                    {
                        targetEnumType = targetEnumInterface.GetGenericArguments()[0];
                    }
                }
                if (source.IsArray)
                {
                    return source.GetElementType();
                }
                return targetEnumType;
            }
            return null;
        }

        protected virtual object MapItem(object o, Type source, Type target)
        {
            if (o == null) return null;
            if (target == typeof(object)) return o;
            if (source == target) return o;

            var sourceEnum = GetEnumerableItemType(source);
            var targetEnum = GetEnumerableItemType(target);
            if (sourceEnum != null && targetEnum != null)
            {
                var listType = typeof(List<>).MakeGenericType(targetEnum);
                var list = (IList)Activator.CreateInstance(listType);

                foreach (var obj in ((IEnumerable)o))
                {
                    list.Add(MapItem(obj, sourceEnum, targetEnum));
                }

                if (target.IsAssignableFrom(listType))
                {
                    return list;
                }

                var arrayType = targetEnum.MakeArrayType();
                if (target.IsAssignableFrom(arrayType))
                {
                    return listType.GetMethod("ToArray").Invoke(list, null);
                }
            }

            if (!(source.IsValueType || target == typeof(string)))
            {
                return Activator.CreateInstance(target).InjectFrom<NameConventionInjection>(o);
            }

            // Try using converters
            var converter = TypeDescriptor.GetConverter(source);
            if (converter.CanConvertTo(target))
            {
                return converter.ConvertTo(o, target);
            }

            converter = TypeDescriptor.GetConverter(target);
            if (converter.CanConvertFrom(source))
            {
                return converter.ConvertFrom(o);
            }
            return o;
        }

        protected override object SetValue(ConventionInfo c)
        {
            return MapItem(c.SourceProp.Value, c.SourceProp.Type, c.TargetProp.Type);
        }

    }
}
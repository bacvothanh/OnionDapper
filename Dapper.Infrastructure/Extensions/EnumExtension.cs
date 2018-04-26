using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Dapper.Infrastructure.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            if (fi == null) return string.Empty;
            var attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        public static List<T> GetList<T>()
        {
            var result = ((T[])Enum.GetValues(typeof(T))).ToList();
            return result;
        }
        public static string GetDisplayName(this Enum val)
        {
            return val.GetType()
                       .GetMember(val.ToString())
                       .FirstOrDefault()
                       ?.GetCustomAttribute<DisplayAttribute>(false)
                       ?.Name
                   ?? val.ToString();
        }

        public static int GetNumberValue(this Enum val)
        {
            return (int)Enum.Parse(val.GetType(), val.ToString());
        }

        public static int GetId(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes =
                (IdAttribute[]) fi?.GetCustomAttributes(
                    typeof(IdAttribute),
                    false);

            return attributes?.Length > 0 ?  attributes[0].Value : 0;
        }
    }
}

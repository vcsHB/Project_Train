using System;
using System.Collections.Generic;
using System.Linq;
using Project_Train.Core.Attribute;

namespace Project_Train.Core
{
    public static class EnumExtensions
    {
        public static string ToDisplayString(this Enum value)
        {
            var type = value.GetType();
            List<string> parts = new List<string>();

            foreach (Enum flag in Enum.GetValues(type))
            {
                if (Convert.ToInt32(flag) != 0 && value.HasFlag(flag))
                {
                    var field = type.GetField(flag.ToString());
                    var attr = field.GetCustomAttributes(typeof(DisplayNameAttribute), false)
                                    .FirstOrDefault() as DisplayNameAttribute;
                    parts.Add(attr?.Name ?? flag.ToString());
                }
            }

            return string.Join("/", parts);
        }
    }
}
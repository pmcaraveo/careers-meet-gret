﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MyAlfaLive.Helpers
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Obtiene el valor Int32 del enumerable
        /// </summary>
        /// <param name="enu"></param>
        /// <returns></returns>
        public static int ToInt(this Enum enu)
        {
            return Convert.ToInt32(enu);
        }

        public static string GetDisplayName(this Enum enu)
        {
            var attr = GetDisplayAttribute(enu);
            return attr != null ? attr.Name : enu.ToString();
        }

        private static DisplayAttribute GetDisplayAttribute(object value)
        {
            Type type = value.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException(string.Format("Type {0} is not an enum", type));
            }

            // Get the enum field.
            var field = type.GetField(value.ToString());
            return field?.GetCustomAttribute<DisplayAttribute>();
        }
    }
}
using System;
using System.ComponentModel;

namespace TelegramBot.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static object GetAnonymousPropertyValue(this object obj, string propertyName)
        {
            var properties = TypeDescriptor.GetProperties(obj);
            var propertyDescriptor = properties.Find(propertyName, false);
            return propertyDescriptor.GetValue(obj);
        }
    }
}
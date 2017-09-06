using System;
using System.ComponentModel;
using System.Web;
using TelegramBot.Common.Cryptography;

namespace TelegramBot.Common.Cookies
{
    public class Cookie : ICookie
    {
        private readonly ICryptographer _cryptographer;

        public Cookie(ICryptographer cryptographer)
        {
            _cryptographer = cryptographer;
        }

        public static float DefaultExpireDurationInDays { get; set; } = 1;

        public static bool DefaultHttpOnly { get; set; } = true;

        public T GetValue<T>(string key)
        {
            return GetValue<T>(key, false);
        }

        public T GetValue<T>(string key, bool expireOnceRead)
        {
            var cookie = HttpContext.Current.Request.Cookies[key];
            T value = default(T);
            if (cookie != null)
            {
                if (!string.IsNullOrWhiteSpace(cookie.Value))
                {
                    var converter = TypeDescriptor.GetConverter(typeof (T));
                    try
                    {
                        value = (T) converter.ConvertFromString(_cryptographer.Decrypt(cookie.Value));
                    }
                    catch (NotSupportedException)
                    {
                        if (converter.CanConvertFrom(typeof (string)))
                        {
                            value = (T) converter.ConvertFrom(_cryptographer.Decrypt(cookie.Value));
                        }
                    }
                }

                if (expireOnceRead)
                {
                    cookie = HttpContext.Current.Response.Cookies[key];

                    if (cookie != null)
                    {
                        cookie.Expires = DateTime.Now.AddDays(-100d);
                    }
                }
            }

            return value;
        }

        public void Remove(string key)
        {
            var cookie = new HttpCookie(key, String.Empty) {Expires = DateTime.Now.AddDays(-1), HttpOnly = false};
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public void SetValue<T>(string key, T value)
        {
            SetValue(key, value, DefaultExpireDurationInDays, DefaultHttpOnly);
        }

        public void SetValue<T>(string key, T value, float expireDurationInDays)
        {
            SetValue(key, value, expireDurationInDays, DefaultHttpOnly);
        }

        public void SetValue<T>(string key, T value, bool httpOnly)
        {
            SetValue(key, value, DefaultExpireDurationInDays, httpOnly);
        }

        public void SetValue<T>(string key, T value, float expireDurationInDays, bool httpOnly)
        {
            var converter = TypeDescriptor.GetConverter(typeof (T));
            string cookieValue = string.Empty;
            try
            {
                cookieValue = converter.ConvertToString(value);
            }
            catch (NotSupportedException)
            {
                if (converter.CanConvertTo(typeof (string)))
                {
                    cookieValue = (string) converter.ConvertTo(value, typeof (string));
                }
            }

            if (!string.IsNullOrWhiteSpace(cookieValue))
            {
                var cookie = new HttpCookie(key, _cryptographer.Encrypt(cookieValue))
                {
                    Expires = DateTime.Now.AddDays(expireDurationInDays),
                    HttpOnly = httpOnly
                };
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
    }
}
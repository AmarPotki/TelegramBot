namespace TelegramBot.Common
{
    public static class Invariants
    {
        public class CookieKeys
        {
            public const string UserFirstName = "AUTHNAME";
            public const string UserHash = "AUTHHASH";
            public const string DeviceIds = "DEVICEHASH";
        }

        public class SessionKeys{
            public const string UserInfo = "USERSESSION";
        }

        public class Platforms
        {
            public const string UniversalWindowsPhone = "UniversalWindowsPhone";
            public const string IOS = "IOS";
            public const string Android = "Android";

            public class Settings
            {
                public class User
                {
                    public const string FirstName = "FirstName";
                    public const string LastName = "LastName";
                    public const string Hash = "Hash";
                    public const string DeviceId = "DeviceId";
                    public const string LockedByParent = "LockedByParent";
                    public const string Id = "UserId";
                }
            }
        }

        public class CacheKeys
        {
            public const string PlatformSettingKey = "PlatformSettingKey";
        }

        public class Servers
        {
            public const string WebServer = "http://www.vielit.com";
        }
        //web Url
        public class Urls{
            public const string ResetPasswordUrl = "";
            public const string ForgotPasswordUrl = "";
        }

        public class Redis{
            public const string Host = "vielitredis.redis.cache.windows.net";
            public const string Password = "z5Wz9AhDyn2/e0VeJSx4Iwad90TEFmodrBcT+RnxPr4=";
        }

        public class Email{
            public const string FromAddress = "support@Sepiateam.com";
        }
    }
}
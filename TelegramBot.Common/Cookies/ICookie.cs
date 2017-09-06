namespace TelegramBot.Common.Cookies{
    public interface ICookie{
        T GetValue<T>(string key);

        T GetValue<T>(string key, bool expireOnceRead);

        void Remove(string key);

        void SetValue<T>(string key, T value);

        void SetValue<T>(string key, T value, float expireDurationInDays);

        void SetValue<T>(string key, T value, bool httpOnly);

        void SetValue<T>(string key, T value, float expireDurationInDays, bool httpOnly);
    }
}
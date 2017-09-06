namespace TelegramBot.Common.Cache
{
    using System.Threading.Tasks;

    public interface ICache<T>
    {
        void Set(string key, T obj, int duration);
        T Get(string key);

        Task SetAsync(string key, T obj, int duration);
        Task<T> GetAsync(string key);
    }
}
namespace TelegramBot.Common.Cache
{
    public class CachInfo<T>
    {
        public long InsertTime { get; set; }
        public int Duration { get; set; }
        public T CachedObject { get; set; }
    }
}
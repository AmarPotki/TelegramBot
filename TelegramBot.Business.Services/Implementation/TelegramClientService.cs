using System.Runtime.Caching;
using Telegram.Bot;
using TelegramBot.Business.Services.Interfaces;
namespace TelegramBot.Business.Services.Implementation{
    public class TelegramClientService : ITelegramClientService
    {
        public TelegramBotClient GetTelegramBot()
        {
			//token
            return new TelegramBotClient("");

        }
        public MemoryCache GetCache()
        {
            return MemoryCache.Default;
        }
    }
}
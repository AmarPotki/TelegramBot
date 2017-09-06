using System.Runtime.Caching;
using Telegram.Bot;
namespace TelegramBot.Business.Services.Interfaces{
    public interface ITelegramClientService{
        TelegramBotClient GetTelegramBot();
        MemoryCache GetCache();
    }
}
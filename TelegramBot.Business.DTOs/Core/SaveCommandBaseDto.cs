using Telegram.Bot.Types;
namespace TelegramBot.Business.DTOs.Core
{
    public abstract class SaveCommandBaseDto:IDto{
        public Message Message { get; set; }
    
    }
}
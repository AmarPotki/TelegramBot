using Telegram.Bot.Types;
namespace TelegramBot.Business.DTOs.Core
{
    public abstract class BaseDto : IDto
    {
        public Message Message { get; set; }
    }
}
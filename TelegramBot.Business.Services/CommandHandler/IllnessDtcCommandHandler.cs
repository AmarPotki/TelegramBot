using System.Threading.Tasks;
using TelegramBot.Business.DTOs.Commands;
using TelegramBot.Business.Services.Command;
using TelegramBot.Business.Services.Interfaces;
using TelegramBot.DataAccess.Interfaces;

namespace TelegramBot.Business.Services.CommandHandler{
    public class IllnessDtcCommandHandler : CommandBase<IllnessDtc>
    {
        private readonly ITelegramClientService _telegramFactory;
        public IllnessDtcCommandHandler(ILogger logger, ITelegramClientService telegramFactory, ITelegramUserRepository telegramRepository)
            : base(logger, telegramFactory,telegramRepository)
        {
            _telegramFactory = telegramFactory;
        }
        protected override async Task<CommandResult> InternalExecute(IllnessDtc dtc)
        {
            var bot = _telegramFactory.GetTelegramBot();
            await
                bot.SendTextMessageAsync(dtc.Message.Chat.Id, "مشکل مورد نظرتان را وارد کنید",
                    replyMarkup: BackToMenu());
            return new CommandResult();
        }
    }
}
using System.Threading.Tasks;
using TelegramBot.Business.DTOs.Commands;
using TelegramBot.Business.Services.Command;
using TelegramBot.Business.Services.Interfaces;
using TelegramBot.DataAccess.Interfaces;

namespace TelegramBot.Business.Services.CommandHandler{
    public class MezajTypeDtcCommandHandler : CommandBase<MezajTypeDtc>
    {
        private readonly ITelegramClientService _telegramFactory;
        public MezajTypeDtcCommandHandler(ILogger logger, ITelegramClientService telegramFactory, ITelegramUserRepository telegramRepository)
            : base(logger, telegramFactory,telegramRepository)
        {
            _telegramFactory = telegramFactory;
        }
        protected override async Task<CommandResult> InternalExecute(MezajTypeDtc dtc)
        {
            var bot = _telegramFactory.GetTelegramBot();
            await
                bot.SendTextMessageAsync(dtc.Message.Chat.Id, "نام خوراکی مورد نظرتان را وارد کنید",
                    replyMarkup: BackToMenu());
            return new CommandResult();
        }
    }
}
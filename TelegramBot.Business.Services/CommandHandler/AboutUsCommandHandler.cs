using System.Threading.Tasks;
using TelegramBot.Business.DTOs.Commands;
using TelegramBot.Business.Services.Command;
using TelegramBot.Business.Services.Interfaces;
using TelegramBot.DataAccess.Interfaces;

namespace TelegramBot.Business.Services.CommandHandler{
    public class AboutUsCommandHandler : CommandBase<AboutUsDtc>{
        private readonly ITelegramClientService _telegramFactory;
        public AboutUsCommandHandler(ILogger logger, ITelegramClientService telegramFactory,ITelegramUserRepository telegramRepository)
            : base(logger, telegramFactory, telegramRepository)
        {
            _telegramFactory = telegramFactory;
        }
        protected override async Task<CommandResult> InternalExecute(AboutUsDtc dtc){
            var bot = _telegramFactory.GetTelegramBot();
            await
                bot.SendTextMessageAsync(dtc.Message.Chat.Id,
                    "این بات برای بالا بردن اطلاعات عمومی در مورد طب سنتی است و به مرور کامل خواهد شد خوشحال می شوم در این راه به من کمک کنید. @ars4m",
                    replyMarkup: BackToMenu());
            //await bot.SendTextMessageAsync(dtc.Message.Chat.Id, "آدرس ما",
            //    replyMarkup:  BackToMenu());
            //await bot.SendLocationAsync(dtc.Message.Chat.Id, 35.808138370388164f, 51.395888328552246f);
            //await bot.SendPhotoAsync(dtc.Message.Chat.Id, "AgADAgADtqcxG0MlCUiuLUO0e4doxQ0zSw0ABKfCIQPk-UuOlc4DAAEC");
            return new CommandResult();
        }
    }
}
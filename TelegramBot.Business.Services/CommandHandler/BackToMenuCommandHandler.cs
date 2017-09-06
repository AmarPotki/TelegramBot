using System.Threading.Tasks;

using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Business.DTOs.Commands;
using TelegramBot.Business.Services.Command;
using TelegramBot.Business.Services.Interfaces;
using TelegramBot.DataAccess.Interfaces;

namespace TelegramBot.Business.Services.CommandHandler{
    public class BackToMenuCommandHandler : CommandBase<BackToMenuDtc>{
        private readonly ITelegramClientService _telegramFactory;
        public BackToMenuCommandHandler(ILogger logger, ITelegramClientService telegramFactory, ITelegramUserRepository telegramRepository)
            : base(logger, telegramFactory,telegramRepository){
            _telegramFactory = telegramFactory;
        }
        protected override async Task<CommandResult> InternalExecute(BackToMenuDtc dtc){
            var cache = _telegramFactory.GetCache();
            var bot = _telegramFactory.GetTelegramBot();
            cache.Remove(dtc.Message.Chat.Id.ToString());
            var keyboard = new ReplyKeyboardMarkup(new[]
          {
                    new[] // first row
                    {
                        new KeyboardButton("\U0001F4A1 درباره ما")
                    },
                    new[] // last row
                    {
                        new KeyboardButton("شناخت طبع مواد خوراکی"),
                        new KeyboardButton("درمان بیماری ها با طب سنتی"),
                        //new KeyboardButton("\U0001F4F7 کد عکس"),
                    }
                }, resizeKeyboard: true);
            await bot.SendTextMessageAsync(dtc.Message.Chat.Id, "انتخاب کنید",
                replyMarkup: keyboard);
            return new CommandResult();
        }
    }
}
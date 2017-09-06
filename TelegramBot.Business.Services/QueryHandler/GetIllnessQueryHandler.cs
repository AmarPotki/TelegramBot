using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Business.DTOs.Queries;
using TelegramBot.Business.Services.Interfaces;
using TelegramBot.Business.Services.Query;
using TelegramBot.Common.Extensions;
using TelegramBot.DataAccess.Interfaces;
namespace TelegramBot.Business.Services.QueryHandler{
    public class GetIllnessQueryHandler : QueryBase<GetIllnessDtq>
    {
        private readonly ILogger _logger;
        private readonly IIllnessRepository _illnessRepository;
        private readonly ITelegramClientService _telegramClientService;
        public GetIllnessQueryHandler(ILogger logger, ITelegramClientService telegramClientService,
            IIllnessRepository illnessRepository)
            : base(logger)
        {
            _telegramClientService = telegramClientService;
            _illnessRepository = illnessRepository;
            _logger = logger;
        }
        protected override async Task<QueryResult> InternalExecute(GetIllnessDtq dtq)
        {
            var name = dtq.Message.Text.SafeFarsiStr();
            var count = await _illnessRepository.CountAsync( x=> x.Name == name);
            var bot = _telegramClientService.GetTelegramBot();
            if (count ==1)
            {
                var illness =
                    await
                        _illnessRepository.QueryAsync(
                            async f => await f.FirstOrDefaultAsync(x => x.Name == name));
                await
                    bot.SendChatActionAsync(dtq.Message.Chat.Id, ChatAction.Typing);
                await SendMessage(illness.Treatment.Trim() , dtq.Message.Chat.Id, bot);
            }
            else if (count > 1) {
                var illnesses =
                    await
                        _illnessRepository.QueryAsync(
                            async f => await f.Where(x => x.Name==name).ToListAsync());
                foreach (var illness in illnesses) {
                    await SendMessage(illness.Treatment.Trim(), dtq.Message.Chat.Id, bot);
                }
            }
            else
            {
                var illnesses =
                    await
                        _illnessRepository.QueryAsync(
                            async f => await f.Where(x => x.Name.Contains(name)).OrderBy(x => x.Name).ToListAsync());
                if (illnesses?.Count > 0)
                {
                    var buttons = new KeyboardButton[illnesses.Count][];
                    for (var i = 0; i < illnesses.Count; i++)
                    {
                        buttons[i] = new[] { new KeyboardButton(illnesses[i].Name) };
                    }
                    var keyboard = new ReplyKeyboardMarkup(buttons, true);
                    await
                        bot.SendTextMessageAsync(dtq.Message.Chat.Id, "انتخاب کنید",
                            replyMarkup: keyboard);
                }
                else
                {
                    _logger.Info(dtq.Message.Text);
                    await
                        bot.SendTextMessageAsync(dtq.Message.Chat.Id, "بیماری یا مشکلی  یافت نشد \n" +
                                                                      "با نوشتن بخشی از کلمه (بیش از دو حرف) هم می توانید مشکل مورد نظر را جستجو کنید " +
                                                                      "مثلا برای یافتن حساسيت فصلی می توانید عبارت (حسا) یا (فصل) را وارد کنید",
                            replyMarkup: BackToMenu());
                }
            }
            return new QueryResult();
        }
       
    }
}
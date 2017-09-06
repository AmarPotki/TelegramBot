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
    public class GetMezajTypeQueryHandler : QueryBase<GetMezajTypeDtq>{
        private readonly ILogger _logger;
        private readonly INutritionRepository _nutritionRepository;
        private readonly INutritionService _nutritionService;
        private readonly ITelegramClientService _telegramClientService;
        public GetMezajTypeQueryHandler(ILogger logger, ITelegramClientService telegramClientService,
            INutritionRepository nutritionRepository, INutritionService nutritionService, ILogger logger1)
            : base(logger){
            _telegramClientService = telegramClientService;
            _nutritionRepository = nutritionRepository;
            _nutritionService = nutritionService;
            _logger = logger1;
        }
        protected override async Task<QueryResult> InternalExecute(GetMezajTypeDtq dtq){
            var name = dtq.Message.Text.SafeFarsiStr();
            var flag = await _nutritionRepository.QueryAsync(async f => await f.AnyAsync(x => x.Name == name));
            var count = await _nutritionRepository.CountAsync(x => x.Name == name);

            var bot = _telegramClientService.GetTelegramBot();
            if (count==1)
            {
                #region Old
                //var nutrition =
                //    await
                //        _nutritionRepository.QueryAsync(
                //            async f => await f.Include("MezajType").FirstOrDefaultAsync(x => x.Name == name));
                //await
                //    bot.SendChatActionAsync(dtq.Message.Chat.Id, ChatAction.Typing);
                //await
                //    bot.SendTextMessageAsync(dtq.Message.Chat.Id, $"نام خوراکی : {nutrition.Name.Trim()}\n" +
                //                                                  $"مزاج : {nutrition.MezajType.Name.Trim()}\n" +
                //                                                  $"توضیحات : {nutrition.Description?.Trim()}\n" +
                //                                                  $"@HiDoctor_bot",
                //        replyMarkup: BackToMenu());
                #endregion
                var nutrition =
                    await
                        _nutritionRepository.QueryAsync(
                            async f => await f.Include("MezajType").FirstOrDefaultAsync(x => x.Name == name));
                await
                    bot.SendChatActionAsync(dtq.Message.Chat.Id, ChatAction.Typing);
                await SendMessage($"نام خوراکی : {nutrition.Name.Trim()}\n" +
                                                                $"مزاج : {nutrition.MezajType.Name.Trim()}\n" +
                                                                $"توضیحات : {nutrition.Description?.Trim()}\n", dtq.Message.Chat.Id, bot);
            }
            else if (count > 1){
                var nutritions =
                    await
                        _nutritionRepository.QueryAsync(
                            async f => await f.Include("MezajType").Where(x => x.Name == name).ToListAsync());
                foreach (var nutrition in nutritions)
                {
                    await SendMessage($"نام خوراکی : {nutrition.Name.Trim()}\n" +
                                      $"مزاج : {nutrition.MezajType.Name.Trim()}\n" +
                                      $"توضیحات : {nutrition.Description?.Trim()}\n", dtq.Message.Chat.Id, bot);
                }
            }
            else{
                var nutritions =
                    await
                        _nutritionRepository.QueryAsync(
                            async f => await f.Include("MezajType").Where(x => x.Name.Contains(name))
                                .OrderBy(x => x.Name).ToListAsync());
                if (nutritions?.Count > 0){
                    var buttons = new KeyboardButton[nutritions.Count][];
                    // var buttons = nutritions.Select(nutrition => new KeyboardButton(nutrition.Name)).ToArray();
                    for (var i = 0; i < nutritions.Count; i++){
                        buttons[i] = new[] {new KeyboardButton(nutritions[i].Name)};
                    }
                    var keyboard = new ReplyKeyboardMarkup(buttons, true);
                    await
                        bot.SendTextMessageAsync(dtq.Message.Chat.Id, "انتخاب کنید",
                            replyMarkup: keyboard);
                }
                else{
                    _logger.Info(dtq.Message.Text);
                    await
                        bot.SendTextMessageAsync(dtq.Message.Chat.Id, "خوراکی مورد نظر یافت نشد \n" +
                                                                      "با نوشتن بخشی از کلمه (بیش از دو حرف) هم می توانید خوارکی مورد نظر را جستجو کنید " +
                                                                      "مثلا برای یافتن کاکوتی می توانید عبارت (کا) یا (کو) را وارد کنید",
                            replyMarkup: BackToMenu());
                }
            }
            return new QueryResult();
        }
    }
}
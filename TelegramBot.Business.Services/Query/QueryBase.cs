using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Business.DTOs.Core;
using TelegramBot.Business.DTOs.Logs;
using TelegramBot.Business.Services.Interfaces;
using TelegramBot.Common.Extensions;
namespace TelegramBot.Business.Services.Query{
    public abstract class QueryBase<TDto> : IQuery<TDto>
        where TDto : IDto{
        private readonly ILogger _logger;
        const int MaxWidth = 4096;
        protected QueryBase(ILogger logger){
            _logger = logger;
        }
        public async Task<QueryResult> Execute(TDto dto){
            var queryResult = new QueryResult();
            try { queryResult = await InternalExecute(dto); }
            catch (Exception exp){
                _logger.Error(new FaultDto("TelegramQueryBase: ", exp.GetMessage(), exp.StackTrace, FaultSource.Telegram));
                queryResult.AddError(exp.GetMessage());
            }
            return queryResult;
        }
        protected abstract Task<QueryResult> InternalExecute(TDto tdDto);
        protected ReplyKeyboardMarkup BackToMenu(){
            return new ReplyKeyboardMarkup(new[]
            {
                new[] // first row
                {
                    new KeyboardButton("\U0001F446 بازگشت به منو")
                }
            }, true);
        }
        protected async Task SendMessage(string message, long id, TelegramBotClient bot){
            message += "\n @HiDoctor_bot";
            if (message.Length <= 4096){
                await bot.SendTextMessageAsync(id, message,
                    replyMarkup: BackToMenu());
                return;
            }
            var lines = SplitToLines(message, 4096);
            foreach (var line in lines) {
                await bot.SendTextMessageAsync(id, line,
                 replyMarkup: BackToMenu());
            }
        }
        IEnumerable<string> SplitToLines(string stringToSplit, int maximumLineLength)
        {
            var words = stringToSplit.Split(' ').Concat(new[] { "" });
            return
                words
                    .Skip(1)
                    .Aggregate(
                        words.Take(1).ToList(),
                        (a, w) =>
                        {
                            var last = a.Last();
                            while (last.Length > maximumLineLength)
                            {
                                a[a.Count() - 1] = last.Substring(0, maximumLineLength);
                                last = last.Substring(maximumLineLength);
                                a.Add(last);
                            }
                            var test = last + " " + w;
                            if (test.Length > maximumLineLength)
                            {
                                a.Add(w);
                            }
                            else
                            {
                                a[a.Count() - 1] = test;
                            }
                            return a;
                        });
        }
    }
}
using System;
using System.Data.Entity;
using System.Runtime.Caching;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Business.DTOs;
using TelegramBot.Business.DTOs.Core;
using TelegramBot.Business.DTOs.Logs;
using TelegramBot.Business.Services.Interfaces;
using TelegramBot.Common.Extensions;
using TelegramBot.DataAccess.Interfaces;

namespace TelegramBot.Business.Services.Command{
    public abstract class 
        CommandBase<TDto> : ICommand<TDto>
        where TDto : IDto
    {
        private readonly ILogger _logger;
        private readonly ITelegramClientService _telegramFactory;
        private readonly ITelegramUserRepository _telegramUserRepo; 
        protected CommandBase(ILogger logger, ITelegramClientService telegramFactory, ITelegramUserRepository telegramUserRepo)
        {
            _logger = logger;
            _telegramFactory = telegramFactory;
            _telegramUserRepo = telegramUserRepo;
        }
        public async Task<CommandResult> Execute(TDto dto){
            var commandResult = new CommandResult();
            var bot = _telegramFactory.GetTelegramBot();
            var cache = _telegramFactory.GetCache();
            await bot.SendChatActionAsync(dto.Message.Chat.Id, ChatAction.Typing);
            if (dto is SaveCommandBaseDto){
                var userMessageData = (UserMessageDataDto)cache.Get(dto.Message.Chat.Id.ToString());
                if(userMessageData == null)
                {
                    userMessageData = new UserMessageDataDto
                    {
                        ChatId = dto.Message.Chat.Id,
                        LastCommand = typeof(TDto).Name,
                        UserName = dto.Message.Chat.Username
                    };
                    var policy = new CacheItemPolicy { SlidingExpiration = TimeSpan.FromMinutes(5) };
                    cache.Add(dto.Message.Chat.Id.ToString(), userMessageData, policy);
                    
                }
                else{
                    userMessageData.LastCommand = typeof (TDto).Name;
                    var policy = new CacheItemPolicy { SlidingExpiration = TimeSpan.FromMinutes(5) };
                cache.Add(dto.Message.Chat.Id.ToString(), userMessageData, policy);  
                }
                var user =await _telegramUserRepo.QueryAsync(async f =>await f.FirstOrDefaultAsync(x => x.UserId == dto.Message.Chat.Id));

                user.LastCommand = typeof(TDto).Name;
                await _telegramUserRepo.SaveAsync(user);

            }
            try
            {

                commandResult = await InternalExecute(dto);
            }
            catch (Exception exp){
                 _logger.Error(new FaultDto("TelegramCommandBase", exp.GetMessage(), exp.StackTrace,FaultSource.Endpoint));
                commandResult.AddError(exp.GetMessage());
            }
            return commandResult;
        }
        protected ReplyKeyboardMarkup BackToMenu()
        {
            return new ReplyKeyboardMarkup(new[]
               {
                    new[] // first row
                    {
                        new KeyboardButton("\U0001F446 بازگشت به منو")
                    }
                },resizeKeyboard:true);
        }
        protected abstract Task<CommandResult> InternalExecute(TDto dtc);
    }
}
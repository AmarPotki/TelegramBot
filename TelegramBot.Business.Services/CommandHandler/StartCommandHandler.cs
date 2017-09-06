using System;
using System.Data.Entity;
using System.Runtime.Caching;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Business.DTOs;
using TelegramBot.Business.DTOs.Commands;
using TelegramBot.Business.Services.Command;
using TelegramBot.Business.Services.Interfaces;
using TelegramBot.DataAccess.Interfaces;

namespace TelegramBot.Business.Services.CommandHandler
{
    public class StartCommandHandler:CommandBase<StartDtc>{
        private readonly ITelegramClientService _telegramFactory;
        public StartCommandHandler(ILogger logger, ITelegramClientService telegramFactory, ITelegramUserRepository telegramRepository) 
            :base(logger, telegramFactory,telegramRepository)
        {
            _telegramFactory = telegramFactory;
        }
        protected override async  Task<CommandResult> InternalExecute(StartDtc tdDto){
            var bot = _telegramFactory.GetTelegramBot();
            var usage = @"به دکتر سلام خوش امدید";
            await bot.SendTextMessageAsync(tdDto.Message.Chat.Id, usage,
                replyMarkup: new ReplyKeyboardHide());
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
            await bot.SendTextMessageAsync(tdDto.Message.Chat.Id, "انتخاب کنید",
                replyMarkup: keyboard);
            return new CommandResult();
        }

    }
    public class CheckUserCommandHandler : CommandBase<CheckUserDtc>
    {
        private readonly ITelegramClientService _telegramFactory;
        private readonly ITelegramUserRepository _telegramUserRepository;
        public CheckUserCommandHandler(ILogger logger, ITelegramClientService telegramFactory,
            ITelegramUserRepository telegramRepository)
            : base(logger, telegramFactory, telegramRepository)
        {
            _telegramUserRepository = telegramRepository;
            _telegramFactory = telegramFactory;
        }
        protected override async Task<CommandResult> InternalExecute(CheckUserDtc tdDto)
        {
            var user =await _telegramUserRepository.QueryAsync(async f => await f.FirstOrDefaultAsync(x => x.UserId == tdDto.Message.Chat.Id));
            if (user != null)
            {
                var cache = _telegramFactory.GetCache();

              var  userMessageData = new UserMessageDataDto
                {
                    ChatId = tdDto.Message.Chat.Id,
                    LastCommand = user.LastCommand,
                    UserName = tdDto.Message.Chat.Username
                };
                var policy = new CacheItemPolicy { SlidingExpiration = TimeSpan.FromMinutes(5) };
                cache.Add(tdDto.Message.Chat.Id.ToString(), userMessageData, policy);
            }
            else
            {
                var bot = _telegramFactory.GetTelegramBot();
                var usage = @"به دکتر سلام خوش امدید";
                await bot.SendTextMessageAsync(tdDto.Message.Chat.Id, usage,
                    replyMarkup: new ReplyKeyboardHide());
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
                await bot.SendTextMessageAsync(tdDto.Message.Chat.Id, "انتخاب کنید",
                    replyMarkup: keyboard);
            }

            return new CommandResult();
        }

    }
}
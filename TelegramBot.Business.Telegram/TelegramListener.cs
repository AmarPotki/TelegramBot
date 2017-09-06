using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using Autofac;
using FluentValidation;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Business.DTOs;
using TelegramBot.Business.DTOs.Commands;
using TelegramBot.Business.DTOs.Core;
using TelegramBot.Business.DTOs.Logs;
using TelegramBot.Business.DTOs.Queries;
using TelegramBot.Business.Services.Command;
using TelegramBot.Business.Services.Interfaces;
using TelegramBot.Business.Services.Query;
using TelegramBot.Common.Exceptions;
using TelegramBot.Common.Extensions;
namespace TelegramBot.Business.Telegram
{
    public class TelegramListener
    {
        private readonly TelegramBotClient _bot;
        private readonly ITelegramClientService _telegramFactory;
        private readonly MemoryCache _cache;
        private readonly ILifetimeScope _scope;
        private readonly List<string> _allowUser;
      
        public TelegramListener(ILifetimeScope scope, ITelegramClientService telegramFactory)
        {
            _scope = scope;
            _telegramFactory = telegramFactory;
            _cache = _telegramFactory.GetCache();
            _bot = _telegramFactory.GetTelegramBot();
            _allowUser = new List<string> { "Ars4m", "eliakbari" };

        }
        public void Start()
        {
            _bot.OnCallbackQuery += BotOnCallbackQueryReceived;
            _bot.OnMessage += BotOnMessageReceived;
            _bot.OnMessageEdited += BotOnMessageReceived;
            _bot.OnInlineQuery += BotOnInlineQueryReceived;
            _bot.OnInlineResultChosen += BotOnChosenInlineResultReceived;
            _bot.OnReceiveError += BotOnReceiveError;
            var me = _bot.GetMeAsync().Result;

            //Console.Title = me.Username;
            _bot.StartReceiving();

            // Console.ReadLine();
            // Bot.StopReceiving();328216798
        }

        public async void Stop(){
            await _bot.SendTextMessageAsync(38596178, "HiDoctor service is stopping");
        }
        private void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Debugger.Break();
        }
        private void BotOnChosenInlineResultReceived(object sender,
            ChosenInlineResultEventArgs chosenInlineResultEventArgs)
        { }
        private async void BotOnInlineQueryReceived(object sender, InlineQueryEventArgs inlineQueryEventArgs) { }
        private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            try
            {
               
                if (message?.Type != MessageType.TextMessage) return;
                if (message.Text.ToLower().StartsWith("/start")) // send inline keyboard
                {
                    await ExecuteCommand(new AddTelegramUserDtc { Message = message });
                    await ExecuteCommand(new StartDtc { Message = message });
                }
                else if (message.Text.ToLower().StartsWith("\U0001F4A1 درباره ما"))
                {
                    await ExecuteCommand(new AboutUsDtc { Message = message });

                }
                else if (message.Text.ToLower().StartsWith("شناخت طبع مواد خوراکی"))
                {
                    await ExecuteCommand(new MezajTypeDtc { Message = message });

                }
                else if (message.Text.ToLower().StartsWith("درمان بیماری ها با طب سنتی"))
                {
                    await ExecuteCommand(new IllnessDtc { Message = message });

                }
                else if (message.Text.ToLower() == "\U0001F446 بازگشت به منو")
                {
                    await ExecuteCommand(new BackToMenuDtc { Message = message });
                }
                else
                {
                    if(message.Text.Length < 2) {
                        await _bot.SendTextMessageAsync(message.Chat.Id, "حداقل دو حرف",
replyMarkup: BackToMenu());
                        return;
                    }
                    if (!_cache.Contains(message.Chat.Id.ToString())) {
                        
                        await ExecuteCommand(new CheckUserDtc { Message = message }); 
                    }
                    var userMessageData = (UserMessageDataDto)_cache.Get(message.Chat.Id.ToString());
                    switch (userMessageData.LastCommand)
                    {
                        case "MezajTypeDtc":
                            {
                                await ExecuteQuery(new GetMezajTypeDtq { Message = message });
                                return;
                            }
                        case "IllnessDtc":
                            {
                                await ExecuteQuery(new GetIllnessDtq { Message = message });
                                return;
                            }
                        default:
                            {
                                await ExecuteCommand(new StartDtc { Message = message });
                                return;
                            }
                    }
                }
            }
            catch (Exception ex){
                var logger = _scope.Resolve<ILogger>();
                try
                {
                    await _bot.SendTextMessageAsync(message.Chat.Id, "خطاي پيش بيني نشده",
replyMarkup: BackToMenu());
                 
                    logger.Error(new FaultDto("TelegramListener", ex.Message, ex.StackTrace, FaultSource.Telegram));
                    await ExecuteCommand(new StartDtc { Message = message });
                }
                catch (Exception e){
                    logger.Error(new FaultDto("TelegramListener", e.Message, e.StackTrace, FaultSource.Telegram));
                   
                }

            }
           
        }
        private async void BotOnCallbackQueryReceived(object sender,
            CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var message = callbackQueryEventArgs.CallbackQuery.Message;
            var data = int.Parse(callbackQueryEventArgs.CallbackQuery.Data);
        }
        private ReplyKeyboardMarkup BackToMenu()
        {
            return new ReplyKeyboardMarkup(new[]
               {
                    new[] // first row
                    {
                        new KeyboardButton("\U0001F446 بازگشت به منو")
                    }
                }, resizeKeyboard: true);
        }

        private async Task ExecuteCommand<TDto>(TDto dto) where TDto : class, IDto
        {
            try
            {
                var validator = _scope.ResolveOptional<IValidator<TDto>>();
                if (validator != null)
                {
                    var validateResult = validator.Validate(dto);
                    if (!validateResult.IsValid)
                    {
                        var message = (new ValidationFault(
                            validateResult.Errors)).Message;
                        await _bot.SendTextMessageAsync(dto.Message.Chat.Id, message,
                        replyMarkup: BackToMenu());
                        return;
                    }
                }
                var commandBus = _scope.Resolve<ICommandBus>();
                var result = await commandBus.Submit(dto);
                if (result.Errors.Count > 0) await _bot.SendTextMessageAsync(dto.Message.Chat.Id, "خطاي پيش بيني نشده",
                         replyMarkup: BackToMenu());
            }
            catch (Exception ex)
            {

                await _bot.SendTextMessageAsync(dto.Message.Chat.Id, "خطاي پيش بيني نشده",
                         replyMarkup: BackToMenu());
            }

        }
        private async Task ExecuteQuery<TDto>(TDto dto) where TDto : class, IDto
        {
            try
            {
                var validator = _scope.Resolve<IValidator<TDto>>();

                if (validator != null)
                {
                    var validateResult = validator.Validate(dto);
                    if (!validateResult.IsValid)
                    {
                        var message = (new ValidationFault(
                            validateResult.Errors)).Message;
                        await _bot.SendTextMessageAsync(dto.Message.Chat.Id, message,
                        replyMarkup: BackToMenu());
                        return;
                    }
                }
                var commandBus = _scope.Resolve<IQueryBus>();
                var result = await commandBus.Submit(dto);
                if (result.Errors.Count > 0)
                {
                    await _bot.SendTextMessageAsync(dto.Message.Chat.Id, "خطاي پيش بيني نشده",
replyMarkup: BackToMenu());
                }
            }
            catch (Exception ex)
            {

                await _bot.SendTextMessageAsync(dto.Message.Chat.Id, "خطاي پيش بيني نشده",
                         replyMarkup: BackToMenu());
            }
        }
       
    }
}
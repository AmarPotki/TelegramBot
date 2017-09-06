using System.Data.Entity;
using System.Threading.Tasks;
using TelegramBot.Business.Domain.Entities.HiDoctor;
using TelegramBot.Business.DTOs.Commands;
using TelegramBot.Business.Services.Command;
using TelegramBot.Business.Services.Interfaces;
using TelegramBot.DataAccess.Interfaces;
namespace TelegramBot.Business.Services.CommandHandler{
    public class AddTelegramUserDtcCommandHandler : CommandBase<AddTelegramUserDtc>{
        private readonly ITelegramUserRepository _telegramUserRepository;
        public AddTelegramUserDtcCommandHandler(ILogger logger, ITelegramClientService telegramFactory,
            ITelegramUserRepository telegramUserRepository) : base(logger, telegramFactory,telegramUserRepository){
            _telegramUserRepository = telegramUserRepository;
        }
        protected override async Task<CommandResult> InternalExecute(AddTelegramUserDtc dtc){
            var chat = dtc.Message.Chat;
            if (await _telegramUserRepository.QueryAsync(f => f.AnyAsync(x => x.UserId == chat.Id))) return new CommandResult();
            var user = new TelegramUser
            {
                UserId = chat.Id,
                UserName = chat.Username,
                FirstName = chat.FirstName,
                LastName = chat.LastName,
                Title = chat.Title
            };
            await _telegramUserRepository.SaveAsync(user);
            return new CommandResult();
        }
    }
}
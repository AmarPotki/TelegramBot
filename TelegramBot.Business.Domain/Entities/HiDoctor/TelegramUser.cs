using TelegramBot.Business.Domain.Core;
namespace TelegramBot.Business.Domain.Entities.HiDoctor{
    public class TelegramUser : IEntity{
        public long Id { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string LastCommand { get; set; }
    }
}
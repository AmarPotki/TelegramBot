using TelegramBot.Business.Domain.Core;
namespace TelegramBot.Business.Domain.Entities.HiDoctor{
    public class Illness: IEntity{
        public long Id { get; set; }
        public string Name { get; set; }
        public string Treatment { get; set; }

    }
}
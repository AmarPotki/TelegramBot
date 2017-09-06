using TelegramBot.Business.Domain.Core;
namespace TelegramBot.Business.Domain.Entities.HiDoctor{
    public class NutritionGroup : IEntity{
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
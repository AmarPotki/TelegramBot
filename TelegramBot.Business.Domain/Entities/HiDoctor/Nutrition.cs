using TelegramBot.Business.Domain.Core;
namespace TelegramBot.Business.Domain.Entities.HiDoctor{
    public class Nutrition : IEntity{
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public MezajType MezajType { get; set; }
        public long MezajTypeId { get; set; }
        public NutritionGroup NutritionGroup { get; set; }
        public long NutritionGroupId { get; set; }

    }
}
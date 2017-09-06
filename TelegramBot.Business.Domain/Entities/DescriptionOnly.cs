namespace TelegramBot.Business.Domain.Entities
{
    public class DescriptionOnly : ItemDescription 
    {
        public DescriptionOnly(string description)
        {
            Description = description;
        }
        public string Description { get; set; }
    }
}
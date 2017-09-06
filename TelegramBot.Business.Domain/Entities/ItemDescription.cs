using System;
using TelegramBot.Business.Domain.Core;

namespace TelegramBot.Business.Domain.Entities
{
    public class ItemDescription :IEntity
    {
        public ItemDescription()
        {
            CreatedTime = DateTime.Now;
        }
        public long Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }
}
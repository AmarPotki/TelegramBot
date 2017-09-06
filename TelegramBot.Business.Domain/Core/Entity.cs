using System;
namespace TelegramBot.Business.Domain.Core{
    public abstract class Entity : IEntity{
        public long Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }
}
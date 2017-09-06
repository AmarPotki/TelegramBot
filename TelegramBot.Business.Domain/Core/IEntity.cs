using System;

namespace TelegramBot.Business.Domain.Core
{
    public interface IEntity
    {
        long Id { get; set; }
        //DateTime CreatedTime { get; set; }
        //DateTime? UpdatedTime { get; set; }
    }
}
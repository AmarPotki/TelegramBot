﻿using TelegramBot.Business.Domain.Entities.HiDoctor;
using TelegramBot.DataAccess.Core;
namespace TelegramBot.DataAccess.Interfaces{
    public interface INutritionRepository : IRepository<Nutrition>{}
}
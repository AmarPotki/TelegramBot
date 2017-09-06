

using TelegramBot.Business.Domain.Entities.HiDoctor;
using TelegramBot.DataAccess.Core;
using TelegramBot.DataAccess.Interfaces;
namespace TelegramBot.DataAccess.Implementation{
    public class NutritionRepository : RepositoryBase<Nutrition>, INutritionRepository{
        public NutritionRepository(IDataContextFactory databaseFactory) : base(databaseFactory){}
    }
}
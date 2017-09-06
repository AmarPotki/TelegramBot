

using TelegramBot.Business.Domain.Entities.HiDoctor;
using TelegramBot.DataAccess.Core;
using TelegramBot.DataAccess.Interfaces;
namespace TelegramBot.DataAccess.Implementation{
    public class NutritionGroupRepository : RepositoryBase<NutritionGroup>, INutritionGroupRepository{
        public NutritionGroupRepository(IDataContextFactory databaseFactory) : base(databaseFactory){}
    }
}
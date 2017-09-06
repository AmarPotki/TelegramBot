
using TelegramBot.Business.Domain.Entities.HiDoctor;
using TelegramBot.DataAccess.Core;
using TelegramBot.DataAccess.Interfaces;
namespace TelegramBot.DataAccess.Implementation{
    public class IllnessRepository : RepositoryBase<Illness>, IIllnessRepository{
        public IllnessRepository(IDataContextFactory databaseFactory) : base(databaseFactory){}
    }
}
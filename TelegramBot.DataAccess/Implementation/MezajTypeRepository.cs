

using TelegramBot.Business.Domain.Entities.HiDoctor;
using TelegramBot.DataAccess.Core;
using TelegramBot.DataAccess.Interfaces;
namespace TelegramBot.DataAccess.Implementation{
    public class MezajTypeRepository : RepositoryBase<MezajType>, IMezajTypeRepository{
        public MezajTypeRepository(IDataContextFactory databaseFactory) : base(databaseFactory){}
    }
}
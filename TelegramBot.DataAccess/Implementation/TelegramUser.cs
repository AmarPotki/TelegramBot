

using TelegramBot.Business.Domain.Entities.HiDoctor;
using TelegramBot.DataAccess.Core;
using TelegramBot.DataAccess.Interfaces;
namespace TelegramBot.DataAccess.Implementation{
    public class TelegramUserRepository : RepositoryBase<TelegramUser>, ITelegramUserRepository{
        public TelegramUserRepository(IDataContextFactory databaseFactory) : base(databaseFactory){}
    }
}
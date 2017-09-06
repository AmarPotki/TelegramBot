

using TelegramBot.Business.Domain.Entities.HiDoctor;
using TelegramBot.DataAccess.Core;
using TelegramBot.DataAccess.Interfaces;
namespace TelegramBot.DataAccess.Implementation{
    public class SynonymRepository : RepositoryBase<Synonym>, ISynonymRepository{
        public SynonymRepository(IDataContextFactory databaseFactory) : base(databaseFactory){}
    }
}
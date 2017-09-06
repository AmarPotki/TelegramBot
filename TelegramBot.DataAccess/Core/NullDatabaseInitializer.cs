using System.Data.Entity;

namespace TelegramBot.DataAccess.Core{
    public class NullDatabaseInitializer<TContext> :
        IDatabaseInitializer<TContext> where TContext : DbContext{
        public void InitializeDatabase(TContext context){
        }
    }
}
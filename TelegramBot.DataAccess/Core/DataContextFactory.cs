using System.Data.Entity.Infrastructure;
namespace TelegramBot.DataAccess.Core{
    public class DataContextFactory : IDbContextFactory<DataContext>, IDataContextFactory{
        private readonly DataContext _dataContext;
        public DataContextFactory(){
            ConnectionName = "HiDoctor";
        }
        public DataContextFactory(DataContext dataContext)
            : this(){
            _dataContext = dataContext;
        }
        public string ConnectionName { get; set; }
        public DataContext GetContext(){
            return Create();
        }
        public void Dispose()
        {
            _dataContext?.Dispose();
        }

        public DataContext Create(){
//            return _dataContext ?? (_dataContext = new DataContext(ConnectionName));
            return new DataContext(ConnectionName);
        }
    }
}
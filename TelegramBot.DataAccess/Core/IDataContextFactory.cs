using System;
namespace TelegramBot.DataAccess.Core{
    public interface IDataContextFactory : IDisposable{
        DataContext GetContext();
    }
}
using System.Threading.Tasks;
using Autofac;
using TelegramBot.Business.DTOs.Core;
namespace TelegramBot.Business.Services.Query
{
    public interface IQueryBus{
        Task<QueryResult> Submit<TDto>(TDto dto) where TDto : IDto;
    }

    public class QueryBus : IQueryBus{
        private readonly ILifetimeScope _scope;
        public QueryBus(ILifetimeScope scope){
            _scope = scope;
        }
        public async Task<QueryResult> Submit<TDto>(TDto dto)
            where TDto : IDto
        {
            var query = _scope.Resolve<IQuery<TDto>>();
            return await query.Execute(dto);
        }
    }
}
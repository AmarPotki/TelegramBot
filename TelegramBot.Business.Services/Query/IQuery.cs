using System.Threading.Tasks;
using TelegramBot.Business.DTOs.Core;
namespace TelegramBot.Business.Services.Query{
    public interface IQuery<in TDto>
        where TDto : IDto{        
        Task<QueryResult> Execute(TDto dto);
    }
}
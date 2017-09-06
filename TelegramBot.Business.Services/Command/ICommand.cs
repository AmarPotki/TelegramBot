using System.Threading.Tasks;
using TelegramBot.Business.DTOs.Core;
namespace TelegramBot.Business.Services.Command
{
    public interface ICommand<in TDto>
        where TDto : IDto{
       // CommandResult CommandResult { get; set; }
        Task<CommandResult> Execute(TDto dto);
    }
}
using System;
using System.Threading.Tasks;
using System.Web;
using Autofac;
using TelegramBot.Business.DTOs.Core;
namespace TelegramBot.Business.Services.Command
{
    public interface ICommandBus
    {
        Task<CommandResult> Submit<TDto>(TDto dto) where TDto : IDto;
        Task<CommandResult> Submit<TDto>(ICommand<TDto> command, TDto dto) where TDto : IDto;
        CommandResult SubmitNoWait<TDto>(TDto dto) where TDto : IDto;
    }

    public class CommandBus : ICommandBus{
        private readonly ILifetimeScope _container;
        public CommandBus(ILifetimeScope container){
            _container = container;
        }
        public async Task<CommandResult> Submit<TDto>(TDto dto)
            where TDto : IDto
        {
            var command =  _container.Resolve<ICommand<TDto>>();
            return await command.Execute(dto);
        }


        public async Task<CommandResult> Submit<TDto>(ICommand<TDto> command, TDto dto)
            where TDto : IDto
        {
            return await command.Execute(dto);
        }

        public CommandResult SubmitNoWait<TDto>(TDto dto)
          where TDto : IDto
        {
            var command = _container.Resolve<ICommand<TDto>>();
            if (command == null)
            {
                throw new InvalidOperationException(
                    string.Format("No command handler found for type {0}", typeof(ICommand<TDto>).Name));
            }
            //return command.Execute(dto);
            return new CommandResult();
        }


    }
}
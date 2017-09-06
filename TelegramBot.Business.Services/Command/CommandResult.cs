using System.Collections.Generic;
using TelegramBot.Common.Interfaces;
namespace TelegramBot.Business.Services.Command
{
    public class CommandResult
    {
        public CommandResult()
        {
            Errors = new List<ErrorInfo>();
        }
        public CommandResult(object model){
            SetModel(model);
        }

        public List<ErrorInfo> Errors { get; private set; }

        public IEventData EventData { get; set; }

        public object Model { get; set; }

        public CommandResult AddError(string message, ErrorType type = ErrorType.System, int code = 0)
        {
            Errors.Add(new ErrorInfo
            {
                Message = message,
                Code = code,
                Type = type
            });
            return this;
        }

        public CommandResult SetEventData(IEventData eventData)
        {
            EventData = eventData;
            return this;
        }

        public CommandResult SetModel(object model)
        {
            Model = model;
            return this;
        }
    }
}
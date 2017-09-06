using System.Collections.Generic;
using TelegramBot.Business.Services.Command;
namespace TelegramBot.Business.Services.Query
{
    using System.Net.Http;

    public class QueryResult{
        public QueryResult(){
            Errors = new List<ErrorInfo>();
        }

        public QueryResult(object model){
            Errors = new List<ErrorInfo>();
            SetModel(model);
        }

        public List<ErrorInfo> Errors { get; private set; }

        public object Model { get; set; }
        public dynamic DynamicModel { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }

        public QueryResult AddError(string message, ErrorType type = ErrorType.System, int code = 0){
            Errors.Add(new ErrorInfo{
                Message = message,
                Code = code,
                Type = type
            });
            return this;
        }


        public QueryResult SetModel(object model){
            Model = model;
            return this;
        }

        public QueryResult SetDynamicModel(dynamic model){
            DynamicModel = model;
            return this;
        }
    }
}
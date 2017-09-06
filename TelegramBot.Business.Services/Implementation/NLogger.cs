using TelegramBot.Business.DTOs.Logs;
using NLog;
namespace TelegramBot.Business.Services.Implementation
{
   

    public class NLogger : Interfaces.ILogger{

        private  readonly Logger _logger = LogManager.GetLogger("Nutrition");
    
        public void Error(FaultDto faultDto)
        {
           
            AddCallContextData(faultDto);
            _logger.Error(faultDto.ToJsonString());
        }
        public void Trace(FaultDto faultDto)
        {
            AddCallContextData(faultDto);
            _logger.Trace(faultDto.ToJsonString());
        }
        public void Info(FaultDto faultDto)
        {
            AddCallContextData(faultDto);
            _logger.Info(faultDto.ToJsonString());
        }
        public void Info(string message)
        {
           
            _logger.Info(message);
        }
        public void Fatal(FaultDto faultDto)
        {
            AddCallContextData(faultDto);
            _logger.Fatal(faultDto.ToJsonString());
        }
        public void Warn(FaultDto faultDto)
        {
            AddCallContextData(faultDto);
            _logger.Warn(faultDto.ToJsonString());
        }

        private void AddCallContextData(FaultDto faultDto)
        {
            //if (faultDto.FaultSource == FaultSource.Endpoint)
            //{
            //    var hostContext = CallContext.HostContext as HttpContext;
            //    if (hostContext != null)
            //    {
            //        faultDto.Endpoint = new EndpointCallContextData
            //        {
            //            Machine = hostContext.Server.MachineName,
            //            Url = hostContext.Request.Url.AbsoluteUri
            //        };
            //    }
            //}
        }
    }
}
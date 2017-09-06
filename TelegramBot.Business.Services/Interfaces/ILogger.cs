using TelegramBot.Business.DTOs.Logs;
namespace TelegramBot.Business.Services.Interfaces{
    public interface ILogger{
        void Error(FaultDto faultDto);
        void Trace(FaultDto faultDto);
        void Info(FaultDto faultDto);
        void Info(string message);
        void Fatal(FaultDto faultDto);
        void Warn(FaultDto faultDto);
    }
}
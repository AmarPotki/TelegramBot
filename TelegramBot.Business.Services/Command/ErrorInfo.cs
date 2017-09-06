namespace TelegramBot.Business.Services.Command
{
    public class ErrorInfo
    {
        public string Message { get; set; }
        public int Code { get; set; }
        public ErrorType Type { get; set; }         
    }

    public enum ErrorType
    {
        System = 1,
        Validation = 2,
        UserInput = 3
    }
}
using System;

namespace TelegramBot.Common.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetMessage(this Exception exception)
        {
            var message = exception.Message;
            while (exception.InnerException != null)
            {
                message += $" [InnerException : {exception.InnerException.Message}] ";
                exception = exception.InnerException;
            }
            return message;
        }
    }
}
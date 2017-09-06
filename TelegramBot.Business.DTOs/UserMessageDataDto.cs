namespace TelegramBot.Business.DTOs{
    public class UserMessageDataDto{
        public string UserName { get; set; }
        public long ChatId { get; set; }
        public string Message { get; set; }
        public string LastCommand { get; set; }
    }
}
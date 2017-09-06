using FluentValidation;

namespace TelegramBot.Common.Interfaces{
    public interface IValidatorFactory{
        IValidator<T> GetValidator<T>();
    }
}
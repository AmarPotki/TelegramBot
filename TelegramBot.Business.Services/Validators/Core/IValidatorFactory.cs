using FluentValidation;
namespace TelegramBot.Business.Services.Validators.Core{
    public interface IValidatorFactory{
        IValidator<T> GetValidator<T>();
    }
}
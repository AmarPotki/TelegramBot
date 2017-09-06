using System;
using Autofac;
using FluentValidation;
namespace TelegramBot.Business.Services.Validators.Core{
    public class ValidatorFactory : IValidatorFactory{
        private readonly IComponentContext _container;
        public ValidatorFactory(IComponentContext container){
            _container = container;
        }
        public IValidator<T> GetValidator<T>(){
            try { return _container.Resolve<IValidator<T>>(); }
            catch (Exception) {
                return null;
            }
        }
    }
}
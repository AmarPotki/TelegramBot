using System.Data.Entity;
using FluentValidation;
using TelegramBot.Business.DTOs.Queries;
using TelegramBot.Business.Services.Interfaces;
using TelegramBot.DataAccess.Interfaces;
namespace TelegramBot.Business.Services.Validators{
    public class GetMezajTypeDtqValidator : AbstractValidator<GetMezajTypeDtq>{
        public GetMezajTypeDtqValidator(INutritionService nutritionService){
            //RuleFor(x => x.Message.Text)
            //    .MustAsync(async (o, text, cancel) => await nutritionService.IsValidNutrition(text))
            //    .WithMessage("خوراکی مورد نظر یافت نشد");
        }
    }
}
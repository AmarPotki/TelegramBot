using System.Threading.Tasks;
namespace TelegramBot.Business.Services.Interfaces{
    public interface INutritionService{
        Task<bool> IsValidNutrition(string name);
    }
}
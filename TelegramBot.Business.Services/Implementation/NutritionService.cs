using System.Data.Entity;
using System.Threading.Tasks;
using TelegramBot.Business.DTOs.Logs;
using TelegramBot.Business.Services.Interfaces;
using TelegramBot.DataAccess.Interfaces;
namespace TelegramBot.Business.Services.Implementation{
    public class NutritionService:INutritionService{
        private readonly INutritionRepository _nutritionRepository;
        private readonly ILogger _logger;
        public NutritionService(INutritionRepository nutritionRepository, ILogger logger){
            _nutritionRepository = nutritionRepository;
            _logger = logger;
        }
        public async Task<bool> IsValidNutrition(string name){
            var flag = await _nutritionRepository.QueryAsync(async f => await f.AnyAsync(x => x.Name == name));
            if (flag) return true;
        _logger.Info(name);
            return false;
        }
    }
}
using System.Data.Entity.ModelConfiguration;
using TelegramBot.Business.Domain.Entities.HiDoctor;
namespace TelegramBot.DataAccess.Mappings{
    public class NutritionMap : EntityTypeConfiguration<Nutrition>{
        public NutritionMap(){
            Property(x => x.MezajTypeId).HasColumnName("MezajType_Id");
            Property(x => x.NutritionGroupId).HasColumnName("NutritionGroup_Id");
        }
    }
}
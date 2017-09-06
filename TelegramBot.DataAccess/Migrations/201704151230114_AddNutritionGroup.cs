namespace TelegramBot.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNutritionGroup : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Nutritions", "MezajType_Id", "dbo.MezajTypes");
            DropIndex("dbo.Nutritions", new[] { "MezajType_Id" });
            CreateTable(
                "dbo.NutritionGroups",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Nutritions", "NutritionGroup_Id", c => c.Long(nullable: false));
            AlterColumn("dbo.Nutritions", "MezajType_Id", c => c.Long(nullable: false));
            CreateIndex("dbo.Nutritions", "MezajType_Id");
            CreateIndex("dbo.Nutritions", "NutritionGroup_Id");
            AddForeignKey("dbo.Nutritions", "NutritionGroup_Id", "dbo.NutritionGroups", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Nutritions", "MezajType_Id", "dbo.MezajTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Nutritions", "MezajType_Id", "dbo.MezajTypes");
            DropForeignKey("dbo.Nutritions", "NutritionGroup_Id", "dbo.NutritionGroups");
            DropIndex("dbo.Nutritions", new[] { "NutritionGroup_Id" });
            DropIndex("dbo.Nutritions", new[] { "MezajType_Id" });
            AlterColumn("dbo.Nutritions", "MezajType_Id", c => c.Long());
            DropColumn("dbo.Nutritions", "NutritionGroup_Id");
            DropTable("dbo.NutritionGroups");
            CreateIndex("dbo.Nutritions", "MezajType_Id");
            AddForeignKey("dbo.Nutritions", "MezajType_Id", "dbo.MezajTypes", "Id");
        }
    }
}

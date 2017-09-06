namespace TelegramBot.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTelegramUserEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TelegramUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.String(),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TelegramUsers");
        }
    }
}

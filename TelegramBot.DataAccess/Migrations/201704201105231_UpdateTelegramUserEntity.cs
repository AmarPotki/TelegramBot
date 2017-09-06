namespace TelegramBot.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTelegramUserEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TelegramUsers", "FirstName", c => c.String());
            AddColumn("dbo.TelegramUsers", "LastName", c => c.String());
            AddColumn("dbo.TelegramUsers", "Title", c => c.String());
            AlterColumn("dbo.TelegramUsers", "UserId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TelegramUsers", "UserId", c => c.String());
            DropColumn("dbo.TelegramUsers", "Title");
            DropColumn("dbo.TelegramUsers", "LastName");
            DropColumn("dbo.TelegramUsers", "FirstName");
        }
    }
}

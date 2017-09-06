namespace TelegramBot.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLastCommand : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TelegramUsers", "LastCommand", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TelegramUsers", "LastCommand");
        }
    }
}

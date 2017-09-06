namespace TelegramBot.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MezajTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Nutritions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        MezajType_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MezajTypes", t => t.MezajType_Id)
                .Index(t => t.MezajType_Id);
            
            CreateTable(
                "dbo.Synonyms",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Nutritions", "MezajType_Id", "dbo.MezajTypes");
            DropIndex("dbo.Nutritions", new[] { "MezajType_Id" });
            DropTable("dbo.Synonyms");
            DropTable("dbo.Nutritions");
            DropTable("dbo.MezajTypes");
        }
    }
}

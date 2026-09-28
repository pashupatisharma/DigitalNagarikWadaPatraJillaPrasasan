namespace HelpDesk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kkkkk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DocumentNeeds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        SubModuleId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubModules", t => t.SubModuleId)
                .Index(t => t.SubModuleId);
            
            CreateTable(
                "dbo.SubModules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        MainModuleId = c.Int(),
                        TimeNeed = c.String(),
                        DasturNeed = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MainModules", t => t.MainModuleId)
                .Index(t => t.MainModuleId);
            
            CreateTable(
                "dbo.MainModules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Processes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        SubModuleId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubModules", t => t.SubModuleId)
                .Index(t => t.SubModuleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Processes", "SubModuleId", "dbo.SubModules");
            DropForeignKey("dbo.DocumentNeeds", "SubModuleId", "dbo.SubModules");
            DropForeignKey("dbo.SubModules", "MainModuleId", "dbo.MainModules");
            DropIndex("dbo.Processes", new[] { "SubModuleId" });
            DropIndex("dbo.SubModules", new[] { "MainModuleId" });
            DropIndex("dbo.DocumentNeeds", new[] { "SubModuleId" });
            DropTable("dbo.Processes");
            DropTable("dbo.MainModules");
            DropTable("dbo.SubModules");
            DropTable("dbo.DocumentNeeds");
        }
    }
}

namespace HelpDesk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xx : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DocumentNeeds", "SubModuleId", "dbo.SubModules");
            DropForeignKey("dbo.Processes", "SubModuleId", "dbo.SubModules");
            DropIndex("dbo.DocumentNeeds", new[] { "SubModuleId" });
            DropIndex("dbo.Processes", new[] { "SubModuleId" });
            AddColumn("dbo.SubModules", "DocumentNeeded", c => c.String());
            AddColumn("dbo.SubModules", "Process", c => c.String());
            DropTable("dbo.DocumentNeeds");
            DropTable("dbo.Processes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Processes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        SubModuleId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DocumentNeeds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        SubModuleId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.SubModules", "Process");
            DropColumn("dbo.SubModules", "DocumentNeeded");
            CreateIndex("dbo.Processes", "SubModuleId");
            CreateIndex("dbo.DocumentNeeds", "SubModuleId");
            AddForeignKey("dbo.Processes", "SubModuleId", "dbo.SubModules", "Id");
            AddForeignKey("dbo.DocumentNeeds", "SubModuleId", "dbo.SubModules", "Id");
        }
    }
}

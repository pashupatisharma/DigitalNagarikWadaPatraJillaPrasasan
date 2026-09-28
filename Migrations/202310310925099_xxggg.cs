namespace HelpDesk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xxggg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubModules", "FileName", c => c.String());
            AddColumn("dbo.SubModules", "FilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubModules", "FilePath");
            DropColumn("dbo.SubModules", "FileName");
        }
    }
}

namespace HelpDesk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class heelloo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notices", "ImageUrl", c => c.String());
            AlterColumn("dbo.Notices", "Image", c => c.String());
            DropColumn("dbo.Notices", "FileName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notices", "FileName", c => c.String());
            AlterColumn("dbo.Notices", "Image", c => c.Binary());
            DropColumn("dbo.Notices", "ImageUrl");
        }
    }
}

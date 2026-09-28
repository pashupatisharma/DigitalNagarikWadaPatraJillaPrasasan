namespace HelpDesk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kgh1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Offices", "Message", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Offices", "Message");
        }
    }
}

namespace HelpDesk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class majimakun : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdkshyaName = c.String(),
                        AdkshyaContactNo = c.String(),
                        AdkshyaMessage = c.String(),
                        AdkshyaPhoto = c.Binary(),
                        UpaAdkshyaName = c.String(),
                        UpaAdkshyaContactNo = c.String(),
                        UpaAdkshyaMessage = c.String(),
                        UpaAdkshyaPhoto = c.Binary(),
                        AdhikritName = c.String(),
                        AdhikritContactNo = c.String(),
                        AdhikritMessage = c.String(),
                        AdhikritPhoto = c.Binary(),
                        ITName = c.String(),
                        ITContactNo = c.String(),
                        ITMessage = c.String(),
                        ItPhoto = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}

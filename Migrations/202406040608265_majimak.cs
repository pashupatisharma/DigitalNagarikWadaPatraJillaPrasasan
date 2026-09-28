namespace HelpDesk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class majimak : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedBy = c.Int(),
                        DeletedDate = c.DateTime(),
                        EditBy = c.Int(),
                        EditedDate = c.DateTime(),
                        Password = c.String(),
                        Status = c.Int(nullable: false),
                        UserName = c.String(),
                        UserRoleId = c.Int(nullable: false),
                        PhotoByte = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRoles", t => t.UserRoleId, cascadeDelete: true)
                .Index(t => t.UserRoleId);
            
            CreateTable(
                "dbo.AppUserPasswords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppUserId = c.Int(nullable: false),
                        ChangedDate = c.DateTime(nullable: false),
                        IsReset = c.Boolean(nullable: false),
                        NewPassword = c.String(),
                        PreviousPassword = c.String(),
                        ResetBy = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId, cascadeDelete: true)
                .Index(t => t.AppUserId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployeeSetups",
                c => new
                    {
                        EmpId = c.Int(nullable: false, identity: true),
                        EmpName = c.String(),
                        EmpNameEng = c.String(),
                        Address = c.String(),
                        Contact = c.String(),
                        DateOfBirth = c.String(),
                        DateOfBirthEng = c.DateTime(),
                        AppointmentDate = c.String(),
                        AppointmentDateEng = c.DateTime(),
                        Image = c.String(),
                        ImageUrl = c.String(),
                        PostId = c.Int(),
                        EmpTypeId = c.Int(nullable: false),
                        Gender = c.Int(),
                        DisplayOrder = c.Int(),
                        EmployeeTypeSetup_EmployeeTypeID = c.Int(),
                    })
                .PrimaryKey(t => t.EmpId)
                .ForeignKey("dbo.EmployeeTypeSetups", t => t.EmployeeTypeSetup_EmployeeTypeID)
                .ForeignKey("dbo.PostSetups", t => t.PostId)
                .Index(t => t.PostId)
                .Index(t => t.EmployeeTypeSetup_EmployeeTypeID);
            
            CreateTable(
                "dbo.EmployeeTypeSetups",
                c => new
                    {
                        EmployeeTypeID = c.Int(nullable: false, identity: true),
                        EmployeeTypeName = c.String(),
                        EmployeeTypeNameEng = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeTypeID);
            
            CreateTable(
                "dbo.PostSetups",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        PostTitle = c.String(),
                        PostTitleEng = c.String(),
                    })
                .PrimaryKey(t => t.PostId);
            
            CreateTable(
                "dbo.MainModules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notices",
                c => new
                    {
                        NoticeId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Image = c.Binary(),
                        CreateDate = c.String(),
                        FileName = c.String(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.NoticeId);
            
            CreateTable(
                "dbo.Offices",
                c => new
                    {
                        Office_ID = c.Int(nullable: false, identity: true),
                        Office_Code = c.String(),
                        Office_Name = c.String(),
                        Office_Location = c.String(),
                        Office_Phone = c.String(),
                        Office_URL = c.String(),
                        Office_Email = c.String(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Office_ID);
            
            CreateTable(
                "dbo.PeopleRepresentatives",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameEng = c.String(),
                        Address = c.String(),
                        Contact = c.String(),
                        DateOfBirth = c.String(),
                        DateOfBirthEng = c.DateTime(),
                        AppointmentDate = c.String(),
                        AppointmentDateEng = c.DateTime(),
                        Image = c.String(),
                        ImageUrl = c.String(),
                        PostId = c.Int(),
                        EmpTypeId = c.Int(nullable: false),
                        Gender = c.Int(),
                        DisplayOrder = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployeeTypeSetups", t => t.EmpTypeId, cascadeDelete: true)
                .ForeignKey("dbo.PostSetups", t => t.PostId)
                .Index(t => t.PostId)
                .Index(t => t.EmpTypeId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SubModules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        MainModuleId = c.Int(),
                        TimeNeed = c.String(),
                        DasturNeed = c.String(),
                        DocumentNeeded = c.String(),
                        Process = c.String(),
                        FileName = c.String(),
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MainModules", t => t.MainModuleId)
                .Index(t => t.MainModuleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        VideoId = c.Int(nullable: false, identity: true),
                        VideoName = c.String(),
                        VideoUrl = c.String(),
                        VideoData = c.String(),
                        UploadedDate = c.DateTime(),
                        UploadedBy = c.String(),
                    })
                .PrimaryKey(t => t.VideoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SubModules", "MainModuleId", "dbo.MainModules");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PeopleRepresentatives", "PostId", "dbo.PostSetups");
            DropForeignKey("dbo.PeopleRepresentatives", "EmpTypeId", "dbo.EmployeeTypeSetups");
            DropForeignKey("dbo.EmployeeSetups", "PostId", "dbo.PostSetups");
            DropForeignKey("dbo.EmployeeSetups", "EmployeeTypeSetup_EmployeeTypeID", "dbo.EmployeeTypeSetups");
            DropForeignKey("dbo.AppUsers", "UserRoleId", "dbo.UserRoles");
            DropForeignKey("dbo.AppUserPasswords", "AppUserId", "dbo.AppUsers");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.SubModules", new[] { "MainModuleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PeopleRepresentatives", new[] { "EmpTypeId" });
            DropIndex("dbo.PeopleRepresentatives", new[] { "PostId" });
            DropIndex("dbo.EmployeeSetups", new[] { "EmployeeTypeSetup_EmployeeTypeID" });
            DropIndex("dbo.EmployeeSetups", new[] { "PostId" });
            DropIndex("dbo.AppUserPasswords", new[] { "AppUserId" });
            DropIndex("dbo.AppUsers", new[] { "UserRoleId" });
            DropTable("dbo.Videos");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SubModules");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PeopleRepresentatives");
            DropTable("dbo.Offices");
            DropTable("dbo.Notices");
            DropTable("dbo.MainModules");
            DropTable("dbo.PostSetups");
            DropTable("dbo.EmployeeTypeSetups");
            DropTable("dbo.EmployeeSetups");
            DropTable("dbo.UserRoles");
            DropTable("dbo.AppUserPasswords");
            DropTable("dbo.AppUsers");
        }
    }
}

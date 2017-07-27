namespace HelloBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigrate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssignmentName = c.String(),
                        DueDate = c.DateTime(nullable: false),
                        ModifiedPagesPerHour = c.Int(nullable: false),
                        Book_Id = c.Int(nullable: false),
                        ReadingDifficulty_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .ForeignKey("dbo.ReadingDifficulties", t => t.ReadingDifficulty_Id)
                .Index(t => t.Book_Id)
                .Index(t => t.ReadingDifficulty_Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        Title = c.String(nullable: false),
                        Isbn = c.String(),
                        TotalPageCount = c.Int(nullable: false),
                        DoneWithBook = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.BookCategoryPairs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Book_Id = c.Int(),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Book_Id)
                .ForeignKey("dbo.BookCategories", t => t.Category_Id)
                .Index(t => t.Book_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.BookCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        Category = c.String(),
                        BookCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookCategories", t => t.BookCategory_Id)
                .Index(t => t.BookCategory_Id);
            
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
                        AppUserAccountType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUserAccountTypes", t => t.AppUserAccountType_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.AppUserAccountType_Id);
            
            CreateTable(
                "dbo.AppUserAccountTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountTypeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.RequiredPages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartPage = c.Int(nullable: false),
                        LastPage = c.Int(nullable: false),
                        Assignment_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assignments", t => t.Assignment_Id, cascadeDelete: true)
                .Index(t => t.Assignment_Id);
            
            CreateTable(
                "dbo.ReadingProgresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartPage = c.Int(nullable: false),
                        FinishPage = c.Int(nullable: false),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(nullable: false),
                        Assignment_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assignments", t => t.Assignment_Id, cascadeDelete: true)
                .Index(t => t.Assignment_Id);
            
            CreateTable(
                "dbo.ReadingDifficulties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PagesPerHour = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
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
                "dbo.ReadingAvailabilities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReadingStartTime = c.DateTime(nullable: false),
                        ReadingEndTime = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        CalenderEventId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReadingAvailabilities", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Assignments", "ReadingDifficulty_Id", "dbo.ReadingDifficulties");
            DropForeignKey("dbo.ReadingDifficulties", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ReadingProgresses", "Assignment_Id", "dbo.Assignments");
            DropForeignKey("dbo.RequiredPages", "Assignment_Id", "dbo.Assignments");
            DropForeignKey("dbo.Assignments", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.Books", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "AppUserAccountType_Id", "dbo.AppUserAccountTypes");
            DropForeignKey("dbo.BookCategories", "BookCategory_Id", "dbo.BookCategories");
            DropForeignKey("dbo.BookCategoryPairs", "Category_Id", "dbo.BookCategories");
            DropForeignKey("dbo.BookCategoryPairs", "Book_Id", "dbo.Books");
            DropIndex("dbo.ReadingAvailabilities", new[] { "ApplicationUserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ReadingDifficulties", new[] { "User_Id" });
            DropIndex("dbo.ReadingProgresses", new[] { "Assignment_Id" });
            DropIndex("dbo.RequiredPages", new[] { "Assignment_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "AppUserAccountType_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.BookCategories", new[] { "BookCategory_Id" });
            DropIndex("dbo.BookCategoryPairs", new[] { "Category_Id" });
            DropIndex("dbo.BookCategoryPairs", new[] { "Book_Id" });
            DropIndex("dbo.Books", new[] { "ApplicationUserId" });
            DropIndex("dbo.Assignments", new[] { "ReadingDifficulty_Id" });
            DropIndex("dbo.Assignments", new[] { "Book_Id" });
            DropTable("dbo.ReadingAvailabilities");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ReadingDifficulties");
            DropTable("dbo.ReadingProgresses");
            DropTable("dbo.RequiredPages");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AppUserAccountTypes");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.BookCategories");
            DropTable("dbo.BookCategoryPairs");
            DropTable("dbo.Books");
            DropTable("dbo.Assignments");
        }
    }
}

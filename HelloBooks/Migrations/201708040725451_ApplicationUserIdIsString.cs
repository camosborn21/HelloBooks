namespace HelloBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationUserIdIsString : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ReadingDifficulties", new[] { "User_Id" });
            DropColumn("dbo.ReadingDifficulties", "ApplicationUserId");
            RenameColumn(table: "dbo.ReadingDifficulties", name: "User_Id", newName: "ApplicationUserId");
            AlterColumn("dbo.ReadingDifficulties", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ReadingDifficulties", "ApplicationUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ReadingDifficulties", new[] { "ApplicationUserId" });
            AlterColumn("dbo.ReadingDifficulties", "ApplicationUserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.ReadingDifficulties", name: "ApplicationUserId", newName: "User_Id");
            AddColumn("dbo.ReadingDifficulties", "ApplicationUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.ReadingDifficulties", "User_Id");
        }
    }
}

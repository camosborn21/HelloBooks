namespace HelloBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedParentIdToChildClasses : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Assignments", "ReadingDifficulty_Id", "dbo.ReadingDifficulties");
            DropIndex("dbo.Assignments", new[] { "ReadingDifficulty_Id" });
            RenameColumn(table: "dbo.Assignments", name: "Book_Id", newName: "BookId");
            RenameColumn(table: "dbo.RequiredPages", name: "Assignment_Id", newName: "AssignmentId");
            RenameColumn(table: "dbo.ReadingProgresses", name: "Assignment_Id", newName: "AssignmentId");
            RenameColumn(table: "dbo.Assignments", name: "ReadingDifficulty_Id", newName: "ReadingDifficultyId");
            RenameIndex(table: "dbo.Assignments", name: "IX_Book_Id", newName: "IX_BookId");
            RenameIndex(table: "dbo.RequiredPages", name: "IX_Assignment_Id", newName: "IX_AssignmentId");
            RenameIndex(table: "dbo.ReadingProgresses", name: "IX_Assignment_Id", newName: "IX_AssignmentId");
            AddColumn("dbo.ReadingDifficulties", "ApplicationUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Assignments", "ReadingDifficultyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Assignments", "ReadingDifficultyId");
            AddForeignKey("dbo.Assignments", "ReadingDifficultyId", "dbo.ReadingDifficulties", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "ReadingDifficultyId", "dbo.ReadingDifficulties");
            DropIndex("dbo.Assignments", new[] { "ReadingDifficultyId" });
            AlterColumn("dbo.Assignments", "ReadingDifficultyId", c => c.Int());
            DropColumn("dbo.ReadingDifficulties", "ApplicationUserId");
            RenameIndex(table: "dbo.ReadingProgresses", name: "IX_AssignmentId", newName: "IX_Assignment_Id");
            RenameIndex(table: "dbo.RequiredPages", name: "IX_AssignmentId", newName: "IX_Assignment_Id");
            RenameIndex(table: "dbo.Assignments", name: "IX_BookId", newName: "IX_Book_Id");
            RenameColumn(table: "dbo.Assignments", name: "ReadingDifficultyId", newName: "ReadingDifficulty_Id");
            RenameColumn(table: "dbo.ReadingProgresses", name: "AssignmentId", newName: "Assignment_Id");
            RenameColumn(table: "dbo.RequiredPages", name: "AssignmentId", newName: "Assignment_Id");
            RenameColumn(table: "dbo.Assignments", name: "BookId", newName: "Book_Id");
            CreateIndex("dbo.Assignments", "ReadingDifficulty_Id");
            AddForeignKey("dbo.Assignments", "ReadingDifficulty_Id", "dbo.ReadingDifficulties", "Id");
        }
    }
}

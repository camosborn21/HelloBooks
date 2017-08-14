namespace HelloBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDifficultyTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReadingDifficulties", "DifficultyTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReadingDifficulties", "DifficultyTitle");
        }
    }
}

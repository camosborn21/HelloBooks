namespace HelloBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedThumbnailLink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "ThumbnailLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "ThumbnailLink");
        }
    }
}

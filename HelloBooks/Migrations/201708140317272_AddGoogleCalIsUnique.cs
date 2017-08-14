namespace HelloBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGoogleCalIsUnique : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "GoogleCalendarIsUnique", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "GoogleCalendarIsUnique");
        }
    }
}

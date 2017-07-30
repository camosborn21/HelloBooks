namespace HelloBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBookProperties : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookProperties",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Book_Id = c.Int(),
                        PropertyType_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Books", t => t.Book_Id)
                .ForeignKey("dbo.BookPropertyTypes", t => t.PropertyType_id)
                .Index(t => t.Book_Id)
                .Index(t => t.PropertyType_id);
            
            CreateTable(
                "dbo.BookPropertyTypes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        PropertyName = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookProperties", "PropertyType_id", "dbo.BookPropertyTypes");
            DropForeignKey("dbo.BookProperties", "Book_Id", "dbo.Books");
            DropIndex("dbo.BookProperties", new[] { "PropertyType_id" });
            DropIndex("dbo.BookProperties", new[] { "Book_Id" });
            DropTable("dbo.BookPropertyTypes");
            DropTable("dbo.BookProperties");
        }
    }
}

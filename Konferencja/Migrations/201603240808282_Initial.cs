namespace Konferencja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conferences",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Publications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        File = c.String(),
                        Author_ID = c.Int(),
                        Conference_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Authors", t => t.Author_ID)
                .ForeignKey("dbo.Conferences", t => t.Conference_ID)
                .Index(t => t.Author_ID)
                .Index(t => t.Conference_ID);
            
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Reviewers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Publication_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Publications", t => t.Publication_ID)
                .Index(t => t.Publication_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Publications", "Conference_ID", "dbo.Conferences");
            DropForeignKey("dbo.Reviewers", "Publication_ID", "dbo.Publications");
            DropForeignKey("dbo.Publications", "Author_ID", "dbo.Authors");
            DropIndex("dbo.Reviewers", new[] { "Publication_ID" });
            DropIndex("dbo.Publications", new[] { "Conference_ID" });
            DropIndex("dbo.Publications", new[] { "Author_ID" });
            DropTable("dbo.Reviewers");
            DropTable("dbo.Authors");
            DropTable("dbo.Publications");
            DropTable("dbo.Conferences");
        }
    }
}

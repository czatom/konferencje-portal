namespace Konferencja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Publications", "Author_ID", "dbo.Authors");
            DropIndex("dbo.Publications", new[] { "Author_ID" });
            AddColumn("dbo.Conferences", "Theme", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.Authors", "ApplicationUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Authors", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Reviewers", "Specialisation", c => c.String());
            AddColumn("dbo.Reviewers", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Reviewers", "ApplicationUserId", c => c.Int());
            AddColumn("dbo.Reviewers", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Publications", "Title", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Publications", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.Publications", "File", c => c.String(nullable: false));
            AlterColumn("dbo.Publications", "Author_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Authors", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Authors", "Surname", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Reviewers", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Reviewers", "Surname", c => c.String(nullable: false));
            CreateIndex("dbo.Publications", "Author_ID");
            CreateIndex("dbo.Authors", "ApplicationUser_Id");
            CreateIndex("dbo.Reviewers", "ApplicationUser_Id");
            AddForeignKey("dbo.Authors", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Reviewers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Publications", "Author_ID", "dbo.Authors", "ID", cascadeDelete: true);
            DropColumn("dbo.Conferences", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conferences", "Title", c => c.String());
            DropForeignKey("dbo.Publications", "Author_ID", "dbo.Authors");
            DropForeignKey("dbo.Reviewers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Authors", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Reviewers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Authors", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Publications", new[] { "Author_ID" });
            AlterColumn("dbo.Reviewers", "Surname", c => c.String());
            AlterColumn("dbo.Reviewers", "Name", c => c.String());
            AlterColumn("dbo.Authors", "Surname", c => c.String());
            AlterColumn("dbo.Authors", "Name", c => c.String());
            AlterColumn("dbo.Publications", "Author_ID", c => c.Int());
            AlterColumn("dbo.Publications", "File", c => c.String());
            AlterColumn("dbo.Publications", "Description", c => c.String());
            AlterColumn("dbo.Publications", "Title", c => c.String());
            DropColumn("dbo.Reviewers", "ApplicationUser_Id");
            DropColumn("dbo.Reviewers", "ApplicationUserId");
            DropColumn("dbo.Reviewers", "Email");
            DropColumn("dbo.Reviewers", "Specialisation");
            DropColumn("dbo.Authors", "ApplicationUser_Id");
            DropColumn("dbo.Authors", "ApplicationUserId");
            DropColumn("dbo.Conferences", "Theme");
            CreateIndex("dbo.Publications", "Author_ID");
            AddForeignKey("dbo.Publications", "Author_ID", "dbo.Authors", "ID");
        }
    }
}

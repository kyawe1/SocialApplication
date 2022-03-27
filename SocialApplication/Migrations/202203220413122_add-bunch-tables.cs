namespace SocialApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addbunchtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        First_UserId = c.String(maxLength: 128),
                        Second_UserId = c.String(maxLength: 128),
                        friend = c.Boolean(nullable: false),
                        pending = c.Boolean(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.First_UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.Second_UserId)
                .Index(t => t.First_UserId)
                .Index(t => t.Second_UserId);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Date_Of_Birth = c.DateTime(nullable: false),
                        DisplayName = c.String(),
                        UserId = c.String(maxLength: 128),
                        address = c.String(),
                        Image = c.String(),
                        CreateAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Reactions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        BlogId = c.String(maxLength: 128),
                        UserId = c.String(maxLength: 128),
                        IsLike = c.Boolean(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blogs", t => t.BlogId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.BlogId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reactions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reactions", "BlogId", "dbo.Blogs");
            DropForeignKey("dbo.Profiles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "Second_UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "First_UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Reactions", new[] { "UserId" });
            DropIndex("dbo.Reactions", new[] { "BlogId" });
            DropIndex("dbo.Profiles", new[] { "UserId" });
            DropIndex("dbo.Friends", new[] { "Second_UserId" });
            DropIndex("dbo.Friends", new[] { "First_UserId" });
            DropTable("dbo.Reactions");
            DropTable("dbo.Profiles");
            DropTable("dbo.Friends");
        }
    }
}

namespace SocialApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addbunchtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Content = c.String(),
                        Image = c.String(),
                        UserId = c.String(maxLength: 128),
                        CreateAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
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
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
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
                "dbo.SaveBlogs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(maxLength: 128),
                        BlogId = c.String(maxLength: 128),
                        CreateAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blogs", t => t.BlogId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.BlogId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaveBlogs", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SaveBlogs", "BlogId", "dbo.Blogs");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reactions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reactions", "BlogId", "dbo.Blogs");
            DropForeignKey("dbo.Profiles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "Second_UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "First_UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Blogs", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.SaveBlogs", new[] { "BlogId" });
            DropIndex("dbo.SaveBlogs", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Reactions", new[] { "UserId" });
            DropIndex("dbo.Reactions", new[] { "BlogId" });
            DropIndex("dbo.Profiles", new[] { "UserId" });
            DropIndex("dbo.Friends", new[] { "Second_UserId" });
            DropIndex("dbo.Friends", new[] { "First_UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Blogs", new[] { "UserId" });
            DropTable("dbo.SaveBlogs");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Reactions");
            DropTable("dbo.Profiles");
            DropTable("dbo.Friends");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Blogs");
        }
    }
}

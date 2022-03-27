namespace SocialApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addimagecolumntoblogtable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blogs", "Image");
        }
    }
}

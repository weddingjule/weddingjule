namespace WeddingJule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Picture2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Pictures", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pictures", "Image", c => c.Binary());
        }
    }
}

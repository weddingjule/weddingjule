namespace WeddingJule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Place : DbMigration
    {
        public override void Up()
        {
            /*
            CreateTable(
                "dbo.Dollars",
                c => new
                    {
                        DollarID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.DollarID);
            
            CreateTable(
                "dbo.Liabilities",
                c => new
                    {
                        LiabilityID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.LiabilityID);
            */
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlaceName = c.String(),
                        Times = c.Int(nullable: false),
                        GeoLong = c.Double(nullable: false),
                        GeoLat = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            /*
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            */
            AlterColumn("dbo.Categories", "name", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Expenses", "name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Expenses", "name", c => c.String());
            AlterColumn("dbo.Categories", "name", c => c.String());
            DropTable("dbo.Users");
            DropTable("dbo.Places");
            DropTable("dbo.Liabilities");
            DropTable("dbo.Dollars");
        }
    }
}

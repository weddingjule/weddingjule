namespace DollarApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dollars",
                c => new
                    {
                        DollarID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.DollarID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Dollars");
        }
    }
}

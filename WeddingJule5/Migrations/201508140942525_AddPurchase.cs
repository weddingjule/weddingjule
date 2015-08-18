namespace WeddingJule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPurchase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        purchaseId = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.purchaseId);

            AlterColumn("dbo.Purchases", "purchaseId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Purchases", "name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Purchases");
            AlterColumn("dbo.Purchases", "name", c => c.String());
            AlterColumn("dbo.Targets", "purchaseId", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropTable("dbo.Purchases");
        }
    }
}

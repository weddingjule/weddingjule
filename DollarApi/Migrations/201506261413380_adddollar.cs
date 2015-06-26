namespace DollarApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddollar : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Dollars");
            AddColumn("dbo.Dollars", "id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Dollars", "id");
            DropColumn("dbo.Dollars", "DollarID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dollars", "DollarID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Dollars");
            DropColumn("dbo.Dollars", "id");
            AddPrimaryKey("dbo.Dollars", "DollarID");
        }
    }
}

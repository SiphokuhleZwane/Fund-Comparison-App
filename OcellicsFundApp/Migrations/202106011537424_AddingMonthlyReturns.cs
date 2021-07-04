namespace OcellicsFundApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingMonthlyReturns : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MonthlyReturns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MonthToDate = c.Double(nullable: false),
                        EffectiveDate = c.DateTime(nullable: false),
                        FundId = c.Int(nullable: false),
                        ThreeMonth = c.Double(),
                        SixMonth = c.Double(),
                        YearToDate = c.Double(),
                        OneYear = c.Double(),
                        TwoYear = c.Double(),
                        SinceInception = c.Double(),
                        ThreeMonthVolatility = c.Double(),
                        SixMonthVolatility = c.Double(),
                        YearToDateVolatility = c.Double(),
                        OneYearVolatility = c.Double(),
                        TwoYearVolatility = c.Double(),
                        SinceInceptionVolatility = c.Double(),
                        ThreeMonthRiskAdjusted = c.Double(),
                        SixMonthRiskAdjusted = c.Double(),
                        YearToDateRiskAdjusted = c.Double(),
                        OneYearRiskAdjusted = c.Double(),
                        TwoYearRiskAdjusted = c.Double(),
                        SinceInceptionRiskAdjusted = c.Double(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Funds", t => t.FundId, cascadeDelete: true)
                .Index(t => t.FundId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MonthlyReturns", "FundId", "dbo.Funds");
            DropIndex("dbo.MonthlyReturns", new[] { "FundId" });
            DropTable("dbo.MonthlyReturns");
        }
    }
}

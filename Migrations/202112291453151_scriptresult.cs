namespace E_Reviu_LKBUN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scriptresult : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OutputColumnLists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UniverseRiskID = c.Int(nullable: false),
                        ColumnName = c.String(),
                        isValueColumn = c.Boolean(nullable: false),
                        isAnomalyIdentifier = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UniverseRisks", t => t.UniverseRiskID, cascadeDelete: true)
                .Index(t => t.UniverseRiskID);
            
            CreateTable(
                "dbo.Assurances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ST = c.String(),
                        UniverseID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        SPANReportPeriod = c.String(),
                        SPANBeginReportPeriod = c.String(),
                        SPANPreviousReportPeriod = c.String(),
                        ErekonReportMonth = c.Int(nullable: false),
                        ErekonReportYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Universes", t => t.UniverseID, cascadeDelete: true)
                .Index(t => t.UniverseID);
            
            CreateTable(
                "dbo.AssuranceResults",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AssuranceID = c.Int(nullable: false),
                        RiskCode = c.String(),
                        Identifier1 = c.String(),
                        Identifier2 = c.String(),
                        Identifier3 = c.String(),
                        Identifier4 = c.String(),
                        ColumnName = c.String(),
                        ColumnValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExecutionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Assurances", t => t.AssuranceID, cascadeDelete: true)
                .Index(t => t.AssuranceID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assurances", "UniverseID", "dbo.Universes");
            DropForeignKey("dbo.AssuranceResults", "AssuranceID", "dbo.Assurances");
            DropForeignKey("dbo.OutputColumnLists", "UniverseRiskID", "dbo.UniverseRisks");
            DropIndex("dbo.AssuranceResults", new[] { "AssuranceID" });
            DropIndex("dbo.Assurances", new[] { "UniverseID" });
            DropIndex("dbo.OutputColumnLists", new[] { "UniverseRiskID" });
            DropTable("dbo.AssuranceResults");
            DropTable("dbo.Assurances");
            DropTable("dbo.OutputColumnLists");
        }
    }
}

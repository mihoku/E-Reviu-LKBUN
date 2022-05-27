namespace E_Reviu_LKBUN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class responses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UniverseDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UniverseID = c.Int(nullable: false),
                        Area = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Universes", t => t.UniverseID, cascadeDelete: true)
                .Index(t => t.UniverseID);
            
            CreateTable(
                "dbo.Responses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AssuranceID = c.Int(nullable: false),
                        UniverseDetailID = c.Int(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Assurances", t => t.AssuranceID, cascadeDelete: true)
                .ForeignKey("dbo.UniverseDetails", t => t.UniverseDetailID, cascadeDelete: false)
                .Index(t => t.AssuranceID)
                .Index(t => t.UniverseDetailID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UniverseDetails", "UniverseID", "dbo.Universes");
            DropForeignKey("dbo.Responses", "UniverseDetailID", "dbo.UniverseDetails");
            DropForeignKey("dbo.Responses", "AssuranceID", "dbo.Assurances");
            DropIndex("dbo.Responses", new[] { "UniverseDetailID" });
            DropIndex("dbo.Responses", new[] { "AssuranceID" });
            DropIndex("dbo.UniverseDetails", new[] { "UniverseID" });
            DropTable("dbo.Responses");
            DropTable("dbo.UniverseDetails");
        }
    }
}

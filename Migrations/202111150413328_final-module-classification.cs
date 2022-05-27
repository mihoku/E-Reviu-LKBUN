namespace E_Reviu_LKBUN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalmoduleclassification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Modules", "Module_ID", c => c.Int());
            CreateIndex("dbo.Modules", "Module_ID");
            CreateIndex("dbo.Risks", "ModuleID");
            AddForeignKey("dbo.Modules", "Module_ID", "dbo.Modules", "ID");
            AddForeignKey("dbo.Risks", "ModuleID", "dbo.Modules", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Risks", "ModuleID", "dbo.Modules");
            DropForeignKey("dbo.Modules", "Module_ID", "dbo.Modules");
            DropIndex("dbo.Risks", new[] { "ModuleID" });
            DropIndex("dbo.Modules", new[] { "Module_ID" });
            DropColumn("dbo.Modules", "Module_ID");
        }
    }
}

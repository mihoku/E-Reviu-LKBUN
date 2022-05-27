namespace E_Reviu_LKBUN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialmoduleclassification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ModuleName = c.String(),
                        abbreviation = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Risks", "ModuleID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Risks", "ModuleID");
            DropTable("dbo.Modules");
        }
    }
}

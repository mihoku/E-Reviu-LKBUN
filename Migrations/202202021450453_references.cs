namespace E_Reviu_LKBUN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class references : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KPPNs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        KodeKPPN = c.String(),
                        NamaKPPN = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Satkers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        KodeSatker = c.String(),
                        NamaSatker = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Satkers");
            DropTable("dbo.KPPNs");
        }
    }
}

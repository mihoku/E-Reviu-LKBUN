namespace E_Reviu_LKBUN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class riskcode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Risks", "Reference", c => c.String());
            AddColumn("dbo.Risks", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Risks", "Code");
            DropColumn("dbo.Risks", "Reference");
        }
    }
}

namespace E_Reviu_LKBUN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class langkahpengujian : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UniverseRisks", "LangkahPengujian", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UniverseRisks", "LangkahPengujian");
        }
    }
}

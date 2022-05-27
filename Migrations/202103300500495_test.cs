namespace E_Reviu_LKBUN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Risks", "Pengendalian", c => c.String());
            AddColumn("dbo.Risks", "Atribut", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Risks", "Atribut");
            DropColumn("dbo.Risks", "Pengendalian");
        }
    }
}

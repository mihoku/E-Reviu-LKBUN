namespace E_Reviu_LKBUN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datedropcance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assurances", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Assurances", "Date");
        }
    }
}

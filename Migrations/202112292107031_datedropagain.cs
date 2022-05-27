namespace E_Reviu_LKBUN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datedropagain : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Assurances", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Assurances", "Date", c => c.DateTime(nullable: false));
        }
    }
}

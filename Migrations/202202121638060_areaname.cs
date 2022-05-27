namespace E_Reviu_LKBUN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class areaname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UniverseDetails", "AreaName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UniverseDetails", "AreaName");
        }
    }
}

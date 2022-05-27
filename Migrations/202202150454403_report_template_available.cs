namespace E_Reviu_LKBUN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class report_template_available : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Universes", "ReportTemplateAvailable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Universes", "ReportTemplateAvailable");
        }
    }
}

namespace E_Reviu_LKBUN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIdentifierColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssuranceResults", "Identifier5", c => c.String());
            AddColumn("dbo.AssuranceResults", "Identifier6", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssuranceResults", "Identifier6");
            DropColumn("dbo.AssuranceResults", "Identifier5");
        }
    }
}

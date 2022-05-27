namespace E_Reviu_LKBUN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIdentifierColumn7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssuranceResults", "Identifier7", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssuranceResults", "Identifier7");
        }
    }
}

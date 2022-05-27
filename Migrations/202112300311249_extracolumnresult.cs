namespace E_Reviu_LKBUN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extracolumnresult : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssuranceResults", "ValueID", c => c.Int(nullable: false));
            CreateIndex("dbo.AssuranceResults", "ValueID");
            AddForeignKey("dbo.AssuranceResults", "ValueID", "dbo.OutputColumnLists", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssuranceResults", "ValueID", "dbo.OutputColumnLists");
            DropIndex("dbo.AssuranceResults", new[] { "ValueID" });
            DropColumn("dbo.AssuranceResults", "ValueID");
        }
    }
}

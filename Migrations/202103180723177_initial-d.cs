namespace E_Reviu_LKBUN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initiald : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActiveSessions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        STID = c.Int(nullable: false),
                        periodID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Periods", t => t.periodID, cascadeDelete: true)
                .Index(t => t.periodID);
            
            CreateTable(
                "dbo.Periods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        description = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RiskCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        category = c.String(),
                        abbreviation = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Risks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Tujuan = c.String(),
                        Risiko = c.String(),
                        Teknik = c.String(),
                        RiskCategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RiskCategories", t => t.RiskCategoryID, cascadeDelete: true)
                .Index(t => t.RiskCategoryID);
            
            CreateTable(
                "dbo.UniverseRisks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UniverseID = c.Int(nullable: false),
                        RiskID = c.Int(nullable: false),
                        Script = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Risks", t => t.RiskID, cascadeDelete: true)
                .ForeignKey("dbo.Universes", t => t.UniverseID, cascadeDelete: true)
                .Index(t => t.UniverseID)
                .Index(t => t.RiskID);
            
            CreateTable(
                "dbo.Universes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ShortName = c.String(),
                        LongName = c.String(),
                        Code = c.String(),
                        parentCode = c.String(),
                        parentID = c.Int(nullable: false),
                        unitID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Units", t => t.unitID, cascadeDelete: true)
                .Index(t => t.unitID);
            
            CreateTable(
                "dbo.STs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nomor = c.String(),
                        TanggalAwal = c.DateTime(nullable: false),
                        TanggalAkhir = c.DateTime(nullable: false),
                        UniverseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Universes", t => t.UniverseID, cascadeDelete: true)
                .Index(t => t.UniverseID);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LongName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CompleteName = c.String(),
                        isAdmin = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.UniverseRisks", "UniverseID", "dbo.Universes");
            DropForeignKey("dbo.Universes", "unitID", "dbo.Units");
            DropForeignKey("dbo.STs", "UniverseID", "dbo.Universes");
            DropForeignKey("dbo.UniverseRisks", "RiskID", "dbo.Risks");
            DropForeignKey("dbo.Risks", "RiskCategoryID", "dbo.RiskCategories");
            DropForeignKey("dbo.ActiveSessions", "periodID", "dbo.Periods");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.STs", new[] { "UniverseID" });
            DropIndex("dbo.Universes", new[] { "unitID" });
            DropIndex("dbo.UniverseRisks", new[] { "RiskID" });
            DropIndex("dbo.UniverseRisks", new[] { "UniverseID" });
            DropIndex("dbo.Risks", new[] { "RiskCategoryID" });
            DropIndex("dbo.ActiveSessions", new[] { "periodID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Units");
            DropTable("dbo.STs");
            DropTable("dbo.Universes");
            DropTable("dbo.UniverseRisks");
            DropTable("dbo.Risks");
            DropTable("dbo.RiskCategories");
            DropTable("dbo.Periods");
            DropTable("dbo.ActiveSessions");
        }
    }
}

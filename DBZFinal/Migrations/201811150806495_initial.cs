namespace DBZFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Health = c.Int(nullable: false),
                        AttackDamage = c.Int(nullable: false),
                        PowerCost = c.Int(nullable: false),
                        UpgradeCost = c.Int(nullable: false),
                        Upgradable = c.Boolean(nullable: false),
                        Form = c.Int(nullable: false),
                        UltDamage = c.Int(nullable: false),
                        CanUlt = c.Boolean(nullable: false),
                        UltCost = c.Int(nullable: false),
                        PassiveChance = c.Int(nullable: false),
                        PassiveDodge = c.Boolean(nullable: false),
                        PassiveDoubleDamage = c.Boolean(nullable: false),
                        PassiveHalfDamage = c.Boolean(nullable: false),
                        PassiveTriplePowerup = c.Boolean(nullable: false),
                        PassiveSteal = c.Boolean(nullable: false),
                        PassiveAbsorb = c.Boolean(nullable: false),
                        PassiveUnblockable = c.Boolean(nullable: false),
                        PassiveSkip = c.Boolean(nullable: false),
                        Portrait = c.String(),
                        PortraitLeft = c.String(),
                        PortraitRight = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Vloume = c.Single(nullable: false),
                        P1Attack = c.String(),
                        P1Block = c.String(),
                        P1PowerUp = c.String(),
                        P1Transform = c.String(),
                        P1Ultimate = c.String(),
                        P2Attack = c.String(),
                        P2Block = c.String(),
                        P2PowerUp = c.String(),
                        P2Transform = c.String(),
                        P2Ultimate = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Settings");
            DropTable("dbo.Characters");
        }
    }
}

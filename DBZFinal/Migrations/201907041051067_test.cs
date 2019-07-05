namespace DBZFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Characters", "PassiveActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Characters", "Passives_ID", c => c.Int());
            CreateIndex("dbo.Characters", "Passives_ID");
            AddForeignKey("dbo.Characters", "Passives_ID", "dbo.Passives", "ID");
            DropColumn("dbo.Characters", "PassiveDodge");
            DropColumn("dbo.Characters", "PassiveDoubleDamage");
            DropColumn("dbo.Characters", "PassiveHalfDamage");
            DropColumn("dbo.Characters", "PassiveTriplePowerup");
            DropColumn("dbo.Characters", "PassiveSteal");
            DropColumn("dbo.Characters", "PassiveAbsorb");
            DropColumn("dbo.Characters", "PassiveUnblockable");
            DropColumn("dbo.Characters", "PassiveSkip");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Characters", "PassiveSkip", c => c.Boolean(nullable: false));
            AddColumn("dbo.Characters", "PassiveUnblockable", c => c.Boolean(nullable: false));
            AddColumn("dbo.Characters", "PassiveAbsorb", c => c.Boolean(nullable: false));
            AddColumn("dbo.Characters", "PassiveSteal", c => c.Boolean(nullable: false));
            AddColumn("dbo.Characters", "PassiveTriplePowerup", c => c.Boolean(nullable: false));
            AddColumn("dbo.Characters", "PassiveHalfDamage", c => c.Boolean(nullable: false));
            AddColumn("dbo.Characters", "PassiveDoubleDamage", c => c.Boolean(nullable: false));
            AddColumn("dbo.Characters", "PassiveDodge", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Characters", "Passives_ID", "dbo.Passives");
            DropIndex("dbo.Characters", new[] { "Passives_ID" });
            DropColumn("dbo.Characters", "Passives_ID");
            DropColumn("dbo.Characters", "PassiveActive");
        }
    }
}

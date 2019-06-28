namespace DBZFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingWinPercentage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WinPercentages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Player = c.String(),
                        Wins = c.Int(nullable: false),
                        Loses = c.Int(nullable: false),
                        Percentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Character = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WinPercentages");
        }
    }
}

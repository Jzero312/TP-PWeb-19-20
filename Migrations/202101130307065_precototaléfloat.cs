namespace PWebTudoDeNovo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class precototaléfloat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ShoppingCars", "PrecoTotal", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShoppingCars", "PrecoTotal", c => c.Int(nullable: false));
        }
    }
}

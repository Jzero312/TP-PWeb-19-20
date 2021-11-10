namespace PWebTudoDeNovo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shoppingcardatanull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ShoppingCars", "DatadeCompra", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShoppingCars", "DatadeCompra", c => c.DateTime(nullable: false));
        }
    }
}

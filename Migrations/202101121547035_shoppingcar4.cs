namespace PWebTudoDeNovo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shoppingcar4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingCars",
                c => new
                    {
                        IdShoppingCar = c.Int(nullable: false, identity: true),
                        Id = c.Int(nullable: false),
                        PrecoTotal = c.Int(nullable: false),
                        Pago = c.Boolean(nullable: false),
                        DatadeCompra = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdShoppingCar)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingCars", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ShoppingCars", new[] { "User_Id" });
            DropTable("dbo.ShoppingCars");
        }
    }
}

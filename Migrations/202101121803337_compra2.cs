namespace PWebTudoDeNovo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class compra2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        IdPurchase = c.Int(nullable: false, identity: true),
                        Id = c.String(maxLength: 128),
                        IdShoppingCar = c.Int(nullable: false),
                        QuantidadeAComprar = c.Int(nullable: false),
                        preco = c.Single(nullable: false),
                        EstadoCompra = c.String(),
                        MetedoDeEntrega = c.String(),
                        idProduto = c.String(),
                        Product_idProduto = c.Int(),
                    })
                .PrimaryKey(t => t.IdPurchase)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_idProduto)
                .ForeignKey("dbo.ShoppingCars", t => t.IdShoppingCar, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.IdShoppingCar)
                .Index(t => t.Product_idProduto);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchases", "IdShoppingCar", "dbo.ShoppingCars");
            DropForeignKey("dbo.Purchases", "Product_idProduto", "dbo.Products");
            DropForeignKey("dbo.Purchases", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Purchases", new[] { "Product_idProduto" });
            DropIndex("dbo.Purchases", new[] { "IdShoppingCar" });
            DropIndex("dbo.Purchases", new[] { "Id" });
            DropTable("dbo.Purchases");
        }
    }
}

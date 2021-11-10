namespace PWebTudoDeNovo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class altereiumvalorqualquer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Purchases", "Product_idProduto", "dbo.Products");
            DropIndex("dbo.Purchases", new[] { "Product_idProduto" });
            DropColumn("dbo.Purchases", "idProduto");
            RenameColumn(table: "dbo.Purchases", name: "Product_idProduto", newName: "idProduto");
            AlterColumn("dbo.Purchases", "idProduto", c => c.Int(nullable: false));
            AlterColumn("dbo.Purchases", "idProduto", c => c.Int(nullable: false));
            CreateIndex("dbo.Purchases", "idProduto");
            AddForeignKey("dbo.Purchases", "idProduto", "dbo.Products", "idProduto", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchases", "idProduto", "dbo.Products");
            DropIndex("dbo.Purchases", new[] { "idProduto" });
            AlterColumn("dbo.Purchases", "idProduto", c => c.Int());
            AlterColumn("dbo.Purchases", "idProduto", c => c.String());
            RenameColumn(table: "dbo.Purchases", name: "idProduto", newName: "Product_idProduto");
            AddColumn("dbo.Purchases", "idProduto", c => c.String());
            CreateIndex("dbo.Purchases", "Product_idProduto");
            AddForeignKey("dbo.Purchases", "Product_idProduto", "dbo.Products", "idProduto");
        }
    }
}

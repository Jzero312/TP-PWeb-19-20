namespace PWebTudoDeNovo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class produtosOutraVezpararemoverprodutoes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Produtoes", "idCategoria", "dbo.Categorias");
            DropIndex("dbo.Produtoes", new[] { "idCategoria" });
            DropTable("dbo.Produtoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Produtoes",
                c => new
                    {
                        idProduto = c.Int(nullable: false, identity: true),
                        idCategoria = c.Int(nullable: false),
                        Nome = c.String(nullable: false),
                        Preco = c.Single(nullable: false),
                        Stock = c.Int(nullable: false),
                        Descricao = c.String(),
                        Empresa = c.String(),
                    })
                .PrimaryKey(t => t.idProduto);
            
            CreateIndex("dbo.Produtoes", "idCategoria");
            AddForeignKey("dbo.Produtoes", "idCategoria", "dbo.Categorias", "idCategoria", cascadeDelete: true);
        }
    }
}

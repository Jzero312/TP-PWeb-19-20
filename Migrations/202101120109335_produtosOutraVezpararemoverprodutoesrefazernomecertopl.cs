namespace PWebTudoDeNovo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class produtosOutraVezpararemoverprodutoesrefazernomecertopl : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Produtos", "idCategoria", "dbo.Categorias");
            DropIndex("dbo.Produtos", new[] { "idCategoria" });
            DropTable("dbo.Produtos");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Produtos",
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
            
            CreateIndex("dbo.Produtos", "idCategoria");
            AddForeignKey("dbo.Produtos", "idCategoria", "dbo.Categorias", "idCategoria", cascadeDelete: true);
        }
    }
}

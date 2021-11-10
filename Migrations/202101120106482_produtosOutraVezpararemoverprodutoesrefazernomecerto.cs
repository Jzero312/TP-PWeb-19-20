namespace PWebTudoDeNovo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class produtosOutraVezpararemoverprodutoesrefazernomecerto : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.idProduto)
                .ForeignKey("dbo.Categorias", t => t.idCategoria, cascadeDelete: true)
                .Index(t => t.idCategoria);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produtos", "idCategoria", "dbo.Categorias");
            DropIndex("dbo.Produtos", new[] { "idCategoria" });
            DropTable("dbo.Produtos");
        }
    }
}

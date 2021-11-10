namespace PWebTudoDeNovo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class promocao500 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "precoComDesconto", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Purchases", "precoComDesconto");
        }
    }
}

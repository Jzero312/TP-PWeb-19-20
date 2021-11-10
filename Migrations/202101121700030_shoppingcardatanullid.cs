namespace PWebTudoDeNovo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shoppingcardatanullid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShoppingCars", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ShoppingCars", new[] { "User_Id" });
            DropColumn("dbo.ShoppingCars", "Id");
            RenameColumn(table: "dbo.ShoppingCars", name: "User_Id", newName: "Id");
            AlterColumn("dbo.ShoppingCars", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ShoppingCars", "Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ShoppingCars", "Id");
            AddForeignKey("dbo.ShoppingCars", "Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingCars", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.ShoppingCars", new[] { "Id" });
            AlterColumn("dbo.ShoppingCars", "Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.ShoppingCars", "Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.ShoppingCars", name: "Id", newName: "User_Id");
            AddColumn("dbo.ShoppingCars", "Id", c => c.Int(nullable: false));
            CreateIndex("dbo.ShoppingCars", "User_Id");
            AddForeignKey("dbo.ShoppingCars", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}

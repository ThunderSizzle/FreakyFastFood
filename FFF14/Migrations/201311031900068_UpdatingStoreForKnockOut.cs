namespace FFF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingStoreForKnockOut : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShoppingCarts", "PaymentMethod_RID", "dbo.PaymentMethods");
            DropIndex("dbo.ShoppingCarts", new[] { "PaymentMethod_RID" });
            DropColumn("dbo.ShoppingCarts", "PaymentMethod_RID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingCarts", "PaymentMethod_RID", c => c.Guid());
            CreateIndex("dbo.ShoppingCarts", "PaymentMethod_RID");
            AddForeignKey("dbo.ShoppingCarts", "PaymentMethod_RID", "dbo.PaymentMethods", "RID");
        }
    }
}

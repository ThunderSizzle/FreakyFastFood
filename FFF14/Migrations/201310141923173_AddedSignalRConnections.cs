namespace FFF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSignalRConnections : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Connections",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        ConnectionID = c.String(),
                        UserAgent = c.String(),
                        Connected = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Objects", t => t.RID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.RID)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Connections", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Connections", "RID", "dbo.Objects");
            DropIndex("dbo.Connections", new[] { "User_Id" });
            DropIndex("dbo.Connections", new[] { "RID" });
            DropTable("dbo.Connections");
        }
    }
}

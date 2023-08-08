namespace EFSamples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class L1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.USER",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.USER");
        }
    }
}

namespace EFWithPostgreSQL.Migrations
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
                        NAME = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.USER");
        }
    }
}

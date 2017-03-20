namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTable_post_comment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 256),
                        email = c.String(),
                        content = c.String(),
                        created_at = c.DateTime(precision: 7, storeType: "datetime2"),
                        updated_at = c.DateTime(precision: 7, storeType: "datetime2"),
                        post_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Posts", t => t.post_id, cascadeDelete: true)
                .Index(t => t.post_id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(maxLength: 256),
                        sub_title = c.String(maxLength: 256),
                        content = c.String(),
                        is_feature = c.Boolean(nullable: false),
                        page_view = c.Int(nullable: false),
                        created_at = c.DateTime(precision: 7, storeType: "datetime2"),
                        updated_at = c.DateTime(precision: 7, storeType: "datetime2"),
                        user_id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.user_id)
                .Index(t => t.user_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "post_id", "dbo.Posts");
            DropForeignKey("dbo.Posts", "user_id", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "user_id" });
            DropIndex("dbo.Comments", new[] { "post_id" });
            DropTable("dbo.Posts");
            DropTable("dbo.Comments");
        }
    }
}

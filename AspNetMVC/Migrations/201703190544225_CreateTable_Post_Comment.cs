namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTable_Post_Comment : DbMigration
    {
        public override void Up()
        {

            CreateTable(
               "dbo.AspNetPost",
               c => new
               {
                   id = c.Int(nullable: false,identity:true),
                   title = c.String(nullable: false, maxLength: 256),
                   sub_title = c.String(nullable: false, maxLength: 256),
                   content = c.String(nullable: false, maxLength: 256),
                   is_feature = c.Boolean(nullable: false,defaultValue:false),
                   page_view = c.Int(nullable: false,defaultValue:0),
                   user_id = c.Guid(nullable: false)
               })
               .PrimaryKey(t => t.id);

            CreateTable(
              "dbo.AspNetComment",
              c => new
              {
                  id = c.Int(nullable: false,identity:true),
                  name = c.String(nullable: false),
                  email = c.String(nullable: false),
                  content = c.String(nullable: false),
                  post_id = c.Int()
              })

              .PrimaryKey(t => t.id);
    }
        
        public override void Down()
        {
            DropTable ("dbo.AspNetPost");
            DropTable("dbo.AspNetComment");
        }
    }
}

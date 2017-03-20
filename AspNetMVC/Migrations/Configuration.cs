namespace AspNetMVC.Migrations
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    /*
     * 建出來的Seed資料怪怪的(Update-Database)，只好再手動調整...
     */
    internal sealed class Configuration : DbMigrationsConfiguration<AspNetMVC.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            //AutomaticMigrationDataLossAllowed = false; //自動移轉-不管資料是否遺失(刪除欄位、長度縮小...等)
            ContextKey = "AspNetMVC.Models.ApplicationDbContext";
        }

        protected override void Seed(AspNetMVC.Models.ApplicationDbContext context)
        {
            //刪除Table資料，並將自動給號歸0
            DbConnection cn = context.Database.Connection;
            DbCommand cmd = cn.CreateCommand();
            cn.Open();
            cmd.CommandText = "delete dbo.Comments;dbcc checkident('dbo.Comments',reseed,0);";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "delete dbo.Posts;dbcc checkident('dbo.Posts',reseed,0);";
            cmd.ExecuteNonQuery();
            cn.Close();

            var passwordHash = new PasswordHasher();
            //user
            context.Users.AddOrUpdate(
                u => u.UserName,
                new Models.ApplicationUser
                {
                    UserName = "zzzajing@gmail.com",
                    PasswordHash = passwordHash.HashPassword("Password@123"),
                    PhoneNumber = "1234567",
                    Id = "f07024e3-af53-4fa0-bf60-935e933b933c",
                },
                new Models.ApplicationUser
                {
                    UserName = "mobilesleepz@gmail.com",
                    PasswordHash = passwordHash.HashPassword("Password@123"),
                    PhoneNumber = "7654321",
                    Id = "f9f77cc3-3460-4f14-8dd9-3ecb51914304",
                });


            Bogus.Faker faker = new Bogus.Faker(locale: "en");
            string[] userId = { "f07024e3-af53-4fa0-bf60-935e933b933c", "f9f77cc3-3460-4f14-8dd9-3ecb51914304" };

            //posts
            for (var i = 1; i <= 20; i++)
            {
                context.Posts.AddOrUpdate(
                    p => p.id,
                    new Models.Post
                    {
                        title = faker.Lorem.Word(),
                        sub_title = faker.Lorem.Word(),
                        content = faker.Lorem.Text(),
                        is_feature = faker.Random.Bool(),
                        page_view = faker.Random.Int(0, 200),
                        created_at = faker.Date.Between(DateTime.Parse("2013/1/1"), DateTime.Parse("2015/1/1")),
                        updated_at = faker.Date.Between(DateTime.Parse("2015/1/1"), DateTime.Parse("2017/1/1")),
                        user_id = userId[faker.Random.Int(0, 1)],
                    }
                );
            }
            context.SaveChanges();

            //comment
            for (var j = 1; j <= 20; j++)
            {
                context.Comments.AddOrUpdate(
                p => p.id,
                new Models.Comment
                {
                    name = faker.Name.FirstName() + faker.Name.LastName(),
                    email = faker.Internet.Email(faker.Name.FirstName(), faker.Name.LastName()),
                    content = faker.Lorem.Text(),
                    created_at = faker.Date.Between(DateTime.Parse("2015/1/1"), DateTime.Parse("2016/1/1")),
                    updated_at = faker.Date.Between(DateTime.Parse("2016/1/1"), DateTime.Parse("2017/1/1")),
                    post_id = faker.Random.Int(2, 5),
                }
             );
            }

        }
    }
}

namespace SelfHost.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SelfHost.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Unity;

    internal sealed class Configuration : DbMigrationsConfiguration<SelfHost.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SelfHost.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            PasswordHasher hasher = new PasswordHasher();
            var user = new ApplicationUser
            {
                UserName = "admin",
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword("123456"),
            };
            context.Set<ApplicationUser>().AddOrUpdate(x => x.UserName, user);

            var role = new IdentityRole { Name = "π‹¿Ì‘±" };
            context.Set<IdentityRole>().AddOrUpdate(x => x.Name, role);

            context.Set<IdentityUserRole>().AddOrUpdate(x => new { x.UserId, x.RoleId }, new IdentityUserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            });
        }
    }
}

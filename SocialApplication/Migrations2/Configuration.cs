namespace SocialApplication.Migrations
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SocialApplication.Models.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SocialApplication.Models.Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            String userId = Guid.NewGuid().ToString();
            String roleId = Guid.NewGuid().ToString();
            IPasswordHasher hasher = new PasswordHasher();
            context.Users.AddOrUpdate(new Models.Entity.ApplicationUser()
            {
                Id=userId,
                Email="one@one.com",
                UserName="one@one.com",
                PasswordHash=hasher.HashPassword("123456"),
                SecurityStamp=Guid.NewGuid().ToString(),
            });
            context.profiles.AddOrUpdate(new Models.Entity.Profile()
            {
                DisplayName = "Mya ya",
                Date_Of_Birth = new DateTime(2001, 8, 17),
                address = "MIngalar taung yangon",
                UserId=userId
            });
            context.Roles.AddOrUpdate(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
            {
                Id=roleId,
                Name = "Admin"
            });
            context.Set<Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole>().AddOrUpdate(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole
            {
                RoleId = roleId,
                UserId = userId
            });
        }
    }
}

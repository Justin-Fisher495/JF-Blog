namespace JF_Blog.Migrations
{
    using JF_Blog.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JF_Blog.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(JF_Blog.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Moderator"))
            {
                roleManager.Create(new IdentityRole { Name = "Moderator" });
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "Justin.Fisher495@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Justin.Fisher495@gmail.com",
                    Email = "Justin.Fisher495@gmail.com",
                    FirstName = "Justin",
                    LastName = "Fisher",
                    DisplayName = "Fish",
                }, "Abc!123@");
            }

            if (!context.Users.Any(u => u.Email == "Moderator@CF.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Moderator@CF.com",
                    Email = "Moderator@CF.com",
                    FirstName = "Moderator",
                    LastName = "CoderFoundry",
                    DisplayName = "CFMod",
                }, "Abc!123@");
            }

            var adminId = userManager.FindByEmail ("Justin.Fisher495@gmail.com").Id;
            userManager.AddToRole(adminId, "Admin");

            var modId = userManager.FindByEmail("Moderator@CF.com").Id;
            userManager.AddToRole(modId, "Moderator");

        }       
    }
}

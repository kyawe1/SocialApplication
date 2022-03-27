using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialApplication.Models.Entity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace SocialApplication.Models
{
    public class Context:IdentityDbContext<ApplicationUser>
    {
        public Context() : base("DefaultString")
        {

        }
        public static Context Create(IdentityFactoryOptions<Context> options,IOwinContext context)
        {
            return new Context();
        }

        public DbSet<Blog> blogs { set; get; }
        public DbSet<SaveBlog> saved_blogs { set; get; }
        public DbSet<Profile> profiles { set; get; }
        public DbSet<Friend> friends { set; get; }
        public DbSet<Reaction> reactions { set; get; }

        //public DbSet<Blog> saved_blogs { set; get; }
    }
}
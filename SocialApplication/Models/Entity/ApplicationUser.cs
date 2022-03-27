using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SocialApplication.Models.Entity
{
    public class ApplicationUser:IdentityUser
    {
        public ApplicationUser() : base()
        {

        }
    }
}
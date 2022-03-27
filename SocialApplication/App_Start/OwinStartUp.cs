using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using SocialApplication.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(SocialApplication.OwinStartUp))]

namespace SocialApplication
{
    public class OwinStartUp
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<Context>(Context.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);


            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                CookieHttpOnly = true,
                CookieSameSite = SameSiteMode.Strict,
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                CookieSecure = CookieSecureOption.SameAsRequest,
                LoginPath = new PathString("/identity/login"),
                LogoutPath = new PathString("/identity/logout")
            }); 
            
        }
    }
}

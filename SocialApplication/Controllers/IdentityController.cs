using SocialApplication.Models;
using SocialApplication.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using SocialApplication.Models.ViewModels;
using System.Threading.Tasks;
using SocialApplication.Models.Filters;

namespace SocialApplication.Controllers
{
    public class IdentityController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        public ApplicationUserManager userManger { set { this._userManager = value; } get { return this._userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); } }
        public ApplicationSignInManager signInManager { set { this._signInManager = value; } get { return this._signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();  } }
        [NoAuthenticatedOnly]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [NoAuthenticatedOnly]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result=signInManager.PasswordSignIn(model.Email, model.password, model.IsRemember, false);
                if (result == SignInStatus.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("error", "Your account is something worng");
            }
            return View(model);
        }
        [HttpGet]
        [NoAuthenticatedOnly]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [NoAuthenticatedOnly]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName=model.Email,
                    Email=model.Email,
                };
                var result=await userManger.CreateAsync(user,model.password);
                if (result.Succeeded)
                {
                    using (Context context = new Context())
                    {
                        Profile profile = new Profile()
                        {
                            UserId = user.Id,
                            DisplayName = Guid.NewGuid().ToString(),
                            Date_Of_Birth=DateTime.Now
                        };
                        context.profiles.Add(profile);
                        context.SaveChanges();
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("error", "Your account is something worng");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult LogOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
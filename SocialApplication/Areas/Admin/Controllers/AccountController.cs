using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialApplication.Models;
using System.Data.Entity;
using SocialApplication.Areas.Admin.Data;
using SocialApplication.Models.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace SocialApplication.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Admin/Account
        public ActionResult Index()
        {
            IEnumerable<AccountViewModel> profiles;
            using (Context context = new Context())
            {
                profiles = context.profiles.Include(p => p.User).Select(p => new AccountViewModel
                {
                    applicationUser = p.User,
                    UserId = p.UserId,
                    address = p.address,
                    Date_Of_Birth = p.Date_Of_Birth,
                    DisplayName = p.DisplayName,
                    CreateAt = p.CreateAt

                }).OrderByDescending(p => p.CreateAt).ToList();
            }
            return View(profiles);
        }
        public ActionResult Detail(string id)
        {
            AccountViewModel profile;
            using (Context context = new Context())
            {
                profile = context.profiles.Include(p => p.User).Select(p => new AccountViewModel
                {
                    applicationUser = p.User,
                    UserId = p.UserId,
                    
                        address = p.address,
                        Date_Of_Birth = p.Date_Of_Birth,
                        DisplayName = p.DisplayName,
                        CreateAt = p.CreateAt
                    
                }).OrderByDescending(p => p.CreateAt).SingleOrDefault(q => q.UserId == id);
            }
            return View(profile);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Email,UserName,Password,Address,ConfirmPassword,Role,DateOfBirth")] AccountCreateViewModel account)
        {
            if (ModelState.IsValid)
            {
                using (Context context = new Context())
                {
                    ApplicationUser user = new ApplicationUser
                    {
                        UserName = account.Email,
                        Email = account.Email
                    };
                    using (ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context)))
                    {
                        var result = await manager.CreateAsync(user, account.Password);
                        if (result.Succeeded)
                        {
                            context.profiles.Add(new Models.Entity.Profile
                            {
                                UserId = user.Id,
                                address = account.Address,
                                Date_Of_Birth = account.DateOfBirth,
                                DisplayName = account.UserName,
                            });
                            context.SaveChanges();
                            if (account.Role != null)
                            {
                                await manager.AddToRoleAsync(user.Id, account.Role);
                            }
                            
                            return RedirectToAction("Index", "Account", new { area = "Admin" });
                        }
                    }
                }
            }
            return View(account);
        }
    }
}
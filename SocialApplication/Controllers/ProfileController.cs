using SocialApplication.Models;
using SocialApplication.Models.Entity;
using SocialApplication.Models.ViewModels;
using System.Web;
using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SocialApplication.Services;

namespace SocialApplication.Controllers
{
    public class ProfileController : Controller
    {

        /// <summary>
        ///     0 is not friend
        ///     1 is friend
        ///     2 is pending
        ///     3 is requested
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(string id)
        {
            ProfileViewModel profile1 = null;
            using (Context _context = new Context())
            {
                ProfileViewModel profile = null;

                if (id != null)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        string user = User.Identity.GetUserId();

                        Friend friend = _context.friends.Where(p => p.First_UserId == user || p.Second_UserId == user).FirstOrDefault();
                        if (friend == null)
                        {
                            ViewData["friend"] = 0;
                        }
                        else
                        {
                            if (friend.pending == false && friend.friend == true)
                            {
                                ViewData["friend"] = 1;
                            }
                            else
                            {
                                if (friend.First_UserId == user && friend.pending == true)
                                {
                                    ViewData["friend"] = 2;
                                }
                                else
                                {
                                    ViewData["friend"] = 3;
                                }
                            }
                        }
                    }
                    profile = _context.profiles.Include("User").Where(p => p.Id.Equals(id)).Select(i => new ProfileViewModel
                    {
                        Id=i.Id,
                        UserId = i.UserId,
                        DisplayName = i.DisplayName,
                        address = i.address,
                        Date_Of_Birth = i.Date_Of_Birth,
                        Email = i.User.Email,
                        PhoneNumber = i.User.PhoneNumber,
                        Image = i.Image
                    }).FirstOrDefault();
                    if (profile == null)
                    {
                        return RedirectToAction("Index", "Blog", new { id = id });
                    }
                    return View(profile);
                }
                string userid = User.Identity.GetUserId();
                profile1 = _context.profiles.Where(p => p.User.Id.Equals(userid)).Include(p => p.User).Select(i => new ProfileViewModel
                {
                    Id=i.Id,
                    DisplayName = i.DisplayName,
                    address = i.address,
                    Date_Of_Birth = i.Date_Of_Birth,
                    Email = i.User.Email,
                    PhoneNumber = i.User.PhoneNumber,
                    Image = i.Image

                }).FirstOrDefault();
                if (profile1 == null)
                {
                    return RedirectToAction("Create", "Profile");
                }
            }
            return View(profile1);
        }
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            string Id = User.Identity.GetUserId();
            using (Context _context = new Context())
            {
                Profile profile = _context.profiles.Where(p => p.UserId.Equals(Id)).FirstOrDefault();
                if (profile != null)
                {
                    return RedirectToAction("Index", "Profile", new { id = profile.Id });
                }
            }
            return View();
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Date_Of_Birth, DisplayName, address, Image")] ProfileCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string UserId = User.Identity.GetUserId();
                Profile profile = null;
                using (Context _context = new Context())
                {
                    //string imageFile = _imageService.SaveImage(model.Image, $"Upload/profilepics/{UserId}/", $"{Guid.NewGuid().ToString()}{Path.GetExtension(model.Image.FileName)}");
                    profile = new Profile
                    {
                        Date_Of_Birth = model.Date_Of_Birth,
                        DisplayName = model.DisplayName,
                        address = model.address,
                        UserId = UserId,
                        //Image = imageFile
                    };
                    _context.profiles.Add(profile);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index", "Profile", new { id = profile.Id });
            }
            return View(model);
        }
        [HttpGet]
        [Authorize]
        public ActionResult Update()
        {
            string Id = User.Identity.GetUserId();
            ProfileCreateViewModel profile = null;
            using (Context _context = new Context())
            {
                profile = _context.profiles.Where(p => p.UserId.Equals(Id)).Select(i => new ProfileCreateViewModel
                {
                    Id=i.Id,
                    Date_Of_Birth = i.Date_Of_Birth,
                    DisplayName = i.DisplayName,
                    address = i.address,
                    ImageName=i.Image
                }).FirstOrDefault();
                if (profile == null)
                {
                    return RedirectToAction("Create", "Profile");
                }
            }
            return View(profile);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "Date_Of_Birth, DisplayName, address, Image")] ProfileCreateViewModel model)
        {
            if (model.Image == null)
            {
                model.Image = null;
            }
            if (ModelState.IsValid)
            {
                string Id = User.Identity.GetUserId();
                Profile profile = null;
                using (Context _context = new Context())
                {
                    profile = _context.profiles.Where(p => p.UserId.Equals(Id)).First();
                    if (model.Image != null)
                    {
                        if (profile.Image != model.Image.FileName)
                        {
                            profile.Image = FileSystem.Savefile(model.Image, $"profile/{profile.Id}", $"{Guid.NewGuid().ToString()}", FileSystem.GetExtension(model.Image.FileName));
                        }
                        profile.DisplayName = model.DisplayName;
                        profile.Date_Of_Birth = model.Date_Of_Birth;
                        profile.address = model.address;
                        _context.Entry(profile).State = EntityState.Modified;
                        _context.SaveChanges();
                    }
                    return RedirectToAction("Index", "Profile", new { id = profile.Id });
                }
            }
            return View(model);

        }
        [HttpGet]
        public ActionResult ResetPassword(){
            return View();
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryKey]
        public async Task<ActionResult> ResetPassword([Bind(Include="OldPassword,NewPassword,NewConfirmPassword")] ResetViewModel model){
            if(ModelState.IsValid){
                //check password is correct
                
            }
            return View(model);
        }
    }
}


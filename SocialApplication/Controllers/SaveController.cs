using SocialApplication.Models;
using SocialApplication.Models.Entity;
using SocialApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace SocialApplication.Controllers
{
    public class SaveController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var UserId = User.Identity.GetUserId();
            IEnumerable<SaveViewModel> saved_lists = null;
            using (Context context = new Context())
            {
                saved_lists = context.saved_blogs.AsNoTracking().Where(p => p.UserId == UserId).Include(p => p.blog).Include(p=>p.blog.User).OrderByDescending(p => p.blog.CreateAt).Select(p => new SaveViewModel
                {
                    BlogCreatedAt = p.blog.CreateAt,
                    BlogId = p.BlogId,
                    UserId = p.UserId,
                    Title = p.blog.Title,
                }).ToList();

                foreach (var item in saved_lists)
                {
                    var profile = context.profiles.AsNoTracking().Where(p => p.UserId == item.UserId).Select(p => new { id = p.Id, Name = p.DisplayName }).FirstOrDefault();
                    item.ProfileId = profile.id;
                    item.Name = profile.Name;
                }
            }
            return View(saved_lists);
        }

        [HttpGet]
        [Authorize]
        public ActionResult UnSave(string id)
        {
            var UserId = User.Identity.GetUserId();
            using (Context context = new Context())
            {
                var saved_object = context.saved_blogs.Where(p => p.UserId == UserId && p.BlogId == id).SingleOrDefault();
                if (saved_object == null)
                {
                    TempData["error"] = "There's no save blog";
                    return RedirectToAction("Index");
                }
                context.saved_blogs.Remove(saved_object);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}


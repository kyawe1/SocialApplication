using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialApplication.Models;
using SocialApplication.Areas.Admin.Data;
using SocialApplication.Models.Entity;
using SocialApplication.Models.ViewModels;
using Microsoft.AspNet.Identity;
using SocialApplication.Services;
using System.IO;

namespace SocialApplication.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        [HttpGet]
        // GET: Admin/Blog
        public ActionResult Index()
        {
            IEnumerable<SocialApplication.Areas.Admin.Data.BlogViewModel> blogs;
            using (Context context = new Context())
            {
                blogs = context.blogs.Select(p => new SocialApplication.Areas.Admin.Data.BlogViewModel
                {
                    AuthorEmail = p.User.Email,
                    CreatedAt = p.CreateAt,
                    AuthorId = p.User.Id,
                    reactionCount = context.reactions.Count(),
                    Title = p.Title,
                    Content = p.Content,
                    ImageUrl = p.Image,
                    Id = p.Id,
                }).ToList();
                foreach (var i in blogs)
                {
                    Profile p = context.profiles.SingleOrDefault(q => q.UserId == i.AuthorId);
                    i.Name = p.DisplayName;
                    i.ProfileId = p.Id;
                }
            }
            return View(blogs);
        }
        [HttpGet]
        // GET: Admin/Blog
        public ActionResult Detail(string id)
        {
            SocialApplication.Areas.Admin.Data.BlogViewModel blog;
            using (Context context = new Context())
            {
                blog = context.blogs.Select(p => new SocialApplication.Areas.Admin.Data.BlogViewModel
                {
                    AuthorEmail = p.User.Email,
                    CreatedAt = p.CreateAt,
                    AuthorId = p.User.Id,
                    reactionCount = context.reactions.Count(),
                    Title = p.Title,
                    Content = p.Content,
                    ImageUrl = p.Image,
                    Id = p.Id,
                }).SingleOrDefault(q => q.Id == id);
                if (blog == null)
                {
                    return View();
                }
                Profile profile = context.profiles.SingleOrDefault(q => q.UserId == blog.AuthorId);
                blog.Name = profile.DisplayName;
                blog.ProfileId = profile.Id;
            }
            return View(blog);
        }
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Title,Content,Image")] BlogCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string filename = FileSystem.Savefile(model.Image, "blogs", $"{model.Title}{Guid.NewGuid().ToString()}", Path.GetExtension(model.Image.FileName));
                    string UserId = User.Identity.GetUserId();
                    Blog blog = new Blog()
                    {
                        Title = model.Title,
                        Content = model.Content,
                        UserId = UserId,
                        Image = filename
                    };
                    using (Context context = new Context())
                    {
                        context.blogs.Add(blog);
                        context.SaveChanges();
                    }
                    return RedirectToAction("Index", "Blog");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return View(model);
        }
        [HttpGet]
        [Authorize]
        public ActionResult Update(string id)
        {
            Blog blog;
            using (Context context = new Context())
            {
                blog = context.blogs.SingleOrDefault(p => p.Id == id);
            }
            if (blog == null)
            {
                return RedirectToAction("Index");
            }
            return View(blog);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Update(string id, [Bind(Include = "Title,Content")] BlogCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (Context context = new Context())
                    {
                        string filename = FileSystem.Savefile(model.Image, "blogs", $"{model.Title}{Guid.NewGuid().ToString()}", Path.GetExtension(model.Image.FileName));
                        Blog blog = context.blogs.SingleOrDefault(p => p.Id == id);
                        if (blog == null)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        string UserId = User.Identity.GetUserId();
                        
                            blog.Title = model.Title;
                            blog.Content = model.Content;
                            context.Entry(blog).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                        
                        
                    }
                    return RedirectToAction("Index", "Blog");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Delete(String id)
        {
            try
            {
                string UserId = User.Identity.GetUserId();
                using (Context context = new Context())
                {
                    var blog = context.blogs.Single(p => p.Id == id);
                    if (blog != null)
                    {
                        context.Entry(blog).State = System.Data.Entity.EntityState.Deleted;
                        context.SaveChanges();
                    }
                }
            }
            catch
            {
                TempData["error"] = "No Id Found";
            }
            return RedirectToAction("Index", "Blog");
        }
    }
}
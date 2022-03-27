using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using SocialApplication.Models;
using SocialApplication.Models.Entity;
using SocialApplication.Models.ViewModels;
using Microsoft.AspNet.Identity;
using SocialApplication.Services;

namespace SocialApplication.Controllers
{
    public class BlogController : Controller
    {
        [HttpGet]
        // GET: Blog
        public ActionResult Index()
        {
            IEnumerable<BlogViewModel> blogs=null;
            using (Context context=new Context())
            {
                string userId = User.Identity.GetUserId();
                blogs =context.blogs.Include(p=> p.User).Select(p=>new BlogViewModel
                {
                    AuthorEmail=p.User.Email,
                    AuthorId=p.User.Id,
                    Content=p.Content,
                    Title=p.Title,
                    reactionCount=context.reactions.Where(q=> q.BlogId==p.Id).Count(),
                    CreatedAt=p.CreateAt,
                    Id=p.Id
                }).ToList();
                foreach( var i in blogs)
                {
                    Profile profile = context.profiles.Single(p => p.UserId == i.AuthorId);
                    i.Name = profile.DisplayName;
                    i.ProfileId = profile.Id;
                    if (User.Identity.IsAuthenticated)
                    {
                        i.liked = context.reactions.FirstOrDefault(q => q.UserId == userId && q.BlogId==i.Id) != null ? true : false;
                    }
                }
            }
            return View(blogs);
        }
        [HttpGet]
        public ActionResult Detail(string id)
        {
            BlogViewModel blog = null;
            using (Context context=new Context())
            {
                
                blog = context.blogs.Include(p=> p.User).Select(p=> new BlogViewModel
                {
                    AuthorEmail = p.User.Email,
                    AuthorId = p.User.Id,
                    Content = p.Content,
                    Title = p.Title,
                    reactionCount = context.reactions.Where(q=> q.BlogId==p.Id).Count(),
                    CreatedAt = p.CreateAt,
                    Id = p.Id
                }).SingleOrDefault(b => b.Id == id);
                if (blog == null)
                {
                    return RedirectToAction("Index","Blog");
                }
                Profile profile = context.profiles.SingleOrDefault(p => p.UserId == blog.AuthorId);
                blog.Name =profile.DisplayName;
                blog.ProfileId =profile.Id;
                if (User.Identity.IsAuthenticated)
                {
                    string UserId = User.Identity.GetUserId();
                    blog.liked = context.reactions.FirstOrDefault(q => q.UserId ==UserId && blog.Id==q.BlogId) != null ? true : false;
                }
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
        public ActionResult Create([Bind(Include ="Title,Content,Image")]BlogCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string filename=FileSystem.Savefile(model.Image, "blogs",$"{model.Title}{Guid.NewGuid().ToString()}",Path.GetExtension(model.Image.FileName));
                    string UserId=User.Identity.GetUserId();
                    Blog blog = new Blog()
                    {
                        Title=model.Title,
                        Content=model.Content,
                        UserId=UserId
                    };
                    using (Context context = new Context())
                    {
                        context.blogs.Add(blog);
                        context.SaveChanges();
                    }
                    return RedirectToAction("Index", "Blog");
                }catch(Exception ex)
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
                blog=context.blogs.SingleOrDefault(p=> p.Id==id);
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
        public ActionResult Update(string id,[Bind(Include ="Title,Content")]BlogCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (Context context = new Context())
                    {
                        Blog blog=context.blogs.SingleOrDefault(p=> p.Id==id);
                        if (blog == null)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        string UserId = User.Identity.GetUserId();
                        if (blog.UserId == UserId)
                        {
                            blog.Title = model.Title;
                            blog.Content = model.Content;
                            context.Entry(blog).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                        }
                        else
                        {
                            
                        }
                    }
                    return RedirectToAction("Index", "Blog");
                }catch(Exception ex)
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
                string UserId=User.Identity.GetUserId();
                using (Context context = new Context())
                {
                    var blog = context.blogs.Single(p => p.Id == id);
                    if(blog!= null || blog.UserId == UserId)
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
        [HttpGet]
        [Authorize]
        public ActionResult Save(string id)
        {
            using (Context context = new Context())
            {
                var blog = context.blogs.Where(p => p.Id == id).FirstOrDefault();
                if (blog == null)
                {
                    return RedirectToAction("Index", "Blog");
                }
                SaveBlog save = new SaveBlog()
                {
                    BlogId = id,
                    UserId = User.Identity.GetUserId()
                };
                context.saved_blogs.Add(save);
                context.SaveChanges();
            }
            return RedirectToAction("Index","Blog");
        }
    }
}
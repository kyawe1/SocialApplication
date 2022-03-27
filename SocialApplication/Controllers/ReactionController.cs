using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SocialApplication.Models.Entity;
using SocialApplication.Models.ViewModels;
using SocialApplication.Models;


namespace SocialApplication.Controllers
{
    public class ReactionController : Controller
    {
        // [HttpPost]
        [Authorize]
        // [ValidateAntiForgeryToken]
        [HttpGet]
        public ActionResult Like(string id)
        {
            var userId = User.Identity.GetUserId();
            using (Context _context = new Context())
            {
                if (_context.reactions.Where(p => p.BlogId == id && p.UserId == userId).Count() != 0)
                {
                    return RedirectToAction("Index", "Blog");
                }
                var reaction = new Reaction()
                {
                    BlogId = id,
                    UserId = userId,
                    IsLike = true
                };

                _context.reactions.Add(reaction);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Blog");
            //recalculate likes
        }
        // [HttpPost]
        [Authorize]
        // [ValidateAntiForgeryToken]
        [HttpGet]
        public ActionResult UnLike(string id)
        {
            var userId = User.Identity.GetUserId();
            using (Context _context = new Context())
            {
                var reaction = _context.reactions.FirstOrDefault(r => r.BlogId == id && r.UserId == userId);
                if (reaction == null)
                {
                    //return BadRequest();
                }
                _context.Entry(reaction).State=System.Data.Entity.EntityState.Deleted;
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Blog");
        }
    }
}


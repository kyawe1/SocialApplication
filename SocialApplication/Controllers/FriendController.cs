using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SocialApplication.Models;
using SocialApplication.Models.Entity;
using SocialApplication.Models.ViewModels;

namespace SocialApplication.Controllers
{
    public class FriendController : Controller
    {

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            IEnumerable<FriendViewModel> friends2 = null;
            string id = User.Identity.GetUserId();
            //all Friends
            // IEnumerable<Friend> friends = _context.friends.Where(f => f.First_UserId == id || f.Second_UserId==id && f.friend==true).Include(p => p.Second_User).Include(p=> p.First_User).ToList();
            // List
            //khan ya tae kaung
            using (Context _context = new Context())
            {
                friends2 = _context.friends.Where(f => f.Second_UserId == id && f.friend == false).Include(p => p.First_User).Select(p => new FriendViewModel
                {
                    Id = p.Id,
                    Sender_UserId = p.First_UserId,
                }).ToList();
                foreach (var i in friends2)
                {
                    var profile_small = _context.profiles.Where(p => p.UserId == i.Sender_UserId).Select(p => new
                    {
                        Profile_Name = p.DisplayName,
                        Profile_Id = p.Id,
                        // Profile_Pic=p.Profile_Pic
                    }).FirstOrDefault();
                    if (profile_small != null)
                    {
                        i.Profile_Name = (string)profile_small.Profile_Name;
                        i.Profile_Id = profile_small.Profile_Id;
                    }
                }
                // foreach(var q in friends){
                //     if(q.First_UserId==id){
                //         var profile_small = _context.profiles.Where(p => p.UserId == i.Sender_UserId).Select(p => new
                //         {
                //             Profile_Name = p.DisplayName,
                //             Profile_Id = p.Id,
                //             // Profile_Pic=p.Profile_Pic
                //         }).FirstOrDefault();

                //         if (profile_small != null)
                //         {
                //             var tempFriendView=new FriendViewModel(){
                //                 Id=q.Id,
                //                 Sender_UserId=q.Second_UserId,
                //                 Profile_Name=profile_small.Profile_Name,
                //                 Profile_Id=profile_small.Profile_Id,
                //                 // Profile_Pic=profile_small.Profile_Pic
                //             };

                //         }
                //     }
                // }
                // IEnumerable<Friend> friends3 = friends.Concat(friends2);
            }
            return View(friends2);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Make(string id)
        {
            string Id = User.Identity.GetUserId();
            Friend temp = null;
            Profile profile1 = null;
            using (Context _context = new Context())
            {
                temp = _context.friends.Where(f => f.First_UserId == id && f.Second_UserId == Id || f.First_UserId == Id && f.Second_UserId == id).FirstOrDefault();
                if (temp == null)
                {
                    Friend friend = new Friend();
                    friend.First_UserId = Id;
                    friend.Second_UserId = id;
                    _context.friends.Add(friend);
                    _context.SaveChanges();
                    return View();
                }
                profile1 = _context.profiles.Where(p => p.User.Id.Equals(Id)).FirstOrDefault();
                if (profile1 == null)
                {
                    //return NotFound();
                }
            }
            return View(profile1.Id);
        }
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        [HttpGet]
        public ActionResult Confirm(string id)
        {
            string Id = User.Identity.GetUserId();
            using (Context _context = new Context())
            {
                Friend temp = _context.friends.Where(f => f.Second_UserId == Id && f.First_UserId == id).FirstOrDefault();
                if (temp != null)
                {
                    temp.friend = true;
                    temp.pending = false;
                    _context.SaveChanges();
                    var profile1 = _context.profiles.Where(p => p.User.Id.Equals(Id)).FirstOrDefault();
                    if (profile1 == null)
                    {
                        //return NotFound();
                    }
                    return RedirectToAction("Index", "Profile", new { id = profile1.Id });

                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Cencel(string id)
        {
            using (Context _context = new Context())
            {
                Friend temp = _context.friends.Find(id);
                if (temp != null)
                {
                    _context.Entry(temp).State = EntityState.Deleted;
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize]
        public ActionResult ShowFriends()
        {
            string Id = User.Identity.GetUserId();
            IEnumerable<FriendViewModel> friends = null;
            using (Context _context = new Context())
            {
                friends = _context.friends.Where(p => p.First_UserId == Id && p.friend == true).Include(p => p.Second_User).Select(p => new FriendViewModel
                {
                    Id = p.Id,
                    Sender_UserId = p.Second_UserId,
                }).ToList();
                IEnumerable<FriendViewModel> friends2 = _context.friends.Where(p => p.Second_UserId == Id && p.friend == true).Include(p => p.Second_User).Select(p => new FriendViewModel
                {
                    Id = p.Id,
                    Sender_UserId = p.First_UserId,
                }).ToList();
                foreach (var i in friends)
                {
                    var friend = _context.profiles.Where(p => p.UserId == i.Sender_UserId).Select(p => new
                    {
                        Profile_Name = p.DisplayName,
                        Profile_Id = p.Id
                    }).First();
                    i.Profile_Name = friend.Profile_Name;
                    i.Profile_Id = friend.Profile_Id;
                }
                foreach (var j in friends2)
                {
                    var friend = _context.profiles.Where(p => p.UserId == j.Sender_UserId).Select(p => new
                    {
                        Profile_Name = p.DisplayName,
                        Profile_Id = p.Id
                    }).First();
                    j.Profile_Name = friend.Profile_Name;
                    j.Profile_Id = friend.Profile_Id;
                }
                friends = friends.Concat(friends2);
            }
            return View(friends);
        }
    }
}




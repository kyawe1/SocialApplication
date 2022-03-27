using System;
using System.Web.Mvc;
using SocialApplication.Services;



namespace SocialApplication.Controllers
{
    [Route("[controller]/[action]/{fileName}")]
    public class UploadController : Controller
    {
        [HttpGet]
        public ActionResult Index(string fileName)
        {
            throw new NotImplementedException();
        }
        
        //[HttpGet]
        //public ActionResult Profile(string fileName)
        //{
        //    var Id = userManager.GetUserId(User);
        //    try
        //    {
        //        if (fileName == "default.jpg")
        //        {
        //            return new FileStreamResult(_imageHandler.GetImage($"wwwroot/img/", "default.jpg"), "image/jpg");
        //        }
        //        return new FileStreamResult(_imageHandler.GetImage($"Upload/profilepics/{Id}/", fileName), "image/png");
        //    }
        //    catch
        //    {
        //        //return NotFound();
        //    }

        //}
    }
}


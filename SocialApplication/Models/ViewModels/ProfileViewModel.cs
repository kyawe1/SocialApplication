using System.ComponentModel.DataAnnotations;
using System;
using System.Web;

namespace SocialApplication.Models.ViewModels
{
    public class ProfileViewModel
    {
        public string DisplayName { set; get; }
        public string address { set; get; }
        public string Email { set; get; }
        public string PhoneNumber { set; get; }
        public string UserId { set; get; }
        public string Image { set; get; }
        // public string Sex{set;get;}
        public DateTime Date_Of_Birth { set; get; }

        public string GetUrl()
        {
            return $"/upload/profile/{Image}";
        }
    }
    public class ProfileCreateViewModel
    {
        [Display(Name ="UserName")]
        public string DisplayName { set; get; }
        [Display(Name ="Address")]
        public string address { set; get; }
        [DataType(DataType.DateTime)]
        [Display(Name ="Date Of Birth")]
        public DateTime Date_Of_Birth { set; get; }
        [DataType(DataType.Upload)]
        [Display(Name ="Profile Picture")]
        public HttpPostedFile Image { set; get; }
    }
}


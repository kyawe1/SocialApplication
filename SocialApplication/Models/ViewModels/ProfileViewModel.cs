using System.ComponentModel.DataAnnotations;
using System;
using System.Web;

namespace SocialApplication.Models.ViewModels
{
    public class ProfileViewModel
    {
        public string Id { set; get; }
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
            if (Image == "default.jpg")
                return $"uploads/profile/{Image}";
            return $"/uploads/profile/{this.Id}/{Image}";
        }
    }
    public class ProfileCreateViewModel
    {
        public virtual string Id { set; get; }
        [Display(Name ="UserName")]
        public string DisplayName { set; get; }
        [Display(Name ="Address")]
        public string address { set; get; }
        [DataType(DataType.Date)]
        [Display(Name ="Date Of Birth")]
        public DateTime Date_Of_Birth { set; get; }
        [DataType(DataType.Upload)]
        [Display(Name ="Profile Picture")]
        public virtual HttpPostedFileBase Image { set; get; }
        public virtual string ImageName { set; get; } = "default.jpg";
        public string GetUrl()
        {
            if (ImageName == "default.jpg")
                return null;
            return $"/uploads/profile/{this.Id}/{ImageName}";
        }
    }
}


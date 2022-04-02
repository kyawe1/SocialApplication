using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SocialApplication.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string password { set; get; }
        [Required]
        [Display(Name ="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("password",ErrorMessage ="confirm password and password are not same......")]
        public string ConfirmPassword { set; get; }
        [Required]
        [EmailAddress]
        public string Email { set; get; }
    }
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { set; get; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { set; get; }
        [Required]
        public bool IsRemember { set; get; }
        public LoginViewModel()
        {
            IsRemember = false;
        }

    }
    public class  ResetViewModel{
        [Display(Name="Old Password")]
        [DataType(DataType.Password)]
        public string OldPassword{set;get;}
        [Display(Name="New Password")]
        [DataType(DataType.Password)]
        public string NewPassword{set;get;}
        [DataType(DataType.Password)]
        [Display(Name="New Confirm Password")]
        [Compare("NewPassword")]
        public string NewConfirmPassword{set;get;}
    }
}
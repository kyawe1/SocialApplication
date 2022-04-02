using Microsoft.AspNet.Identity.EntityFramework;
using SocialApplication.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialApplication.Areas.Admin.Data
{
    public class AccountViewModel
    {
        public string UserId { set; get; }
        public string address { set; get; }
        [DataType(DataType.Date)]
        public DateTime Date_Of_Birth { set; get; }
        public string DisplayName { set; get; }
        public DateTime CreateAt { set; get; }
        public ApplicationUser applicationUser { set; get; }
    }
    public class AccountCreateViewModel
    {
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { set; get; }
        public string UserName { set; get; }
        public string Address { set; get; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { set; get; }
        [DataType(DataType.Password)]
        public string Password { set; get; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { set; get; }
        public virtual string Role { set; get; }
        public virtual SelectList roles { set; get; }
    }
}
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
namespace SocialApplication.Models.Entity
{
    public class Profile : Base
    {
        [DataType(DataType.DateTime)]
        public DateTime Date_Of_Birth { set; get; }
        public string DisplayName { set; get; }
        public ApplicationUser User { set; get; }
        [ForeignKey("User")]
        public string UserId { set; get; }
        public string address { set; get; }
        public string Image { set; get; }

        public Profile() : base()
        {
            Image = "default.png";
        }
    }
}


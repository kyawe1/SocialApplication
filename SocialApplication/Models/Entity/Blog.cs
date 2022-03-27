using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SocialApplication.Models.Entity
{
    public class Blog:Base
    {
        public string Title { set; get; }
        public string Content { set; get; }
        [ForeignKey("User")]
        public string UserId { set; get; }
        public ApplicationUser User { set; get; }
        public Blog() : base()
        {

        }
    }
}
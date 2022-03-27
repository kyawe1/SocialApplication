using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialApplication.Models.Entity
{
    public class SaveBlog:Base
    {
        [ForeignKey("User")]
        public string UserId { set; get; }
        public ApplicationUser User { set; get; }
        [ForeignKey("blog")]
        public string BlogId { set; get; }
        public Blog blog { set; get; }
        public SaveBlog() : base()
        {

        }
    }
}
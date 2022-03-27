using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace SocialApplication.Models.Entity
{
    public class Reaction : Base
    {
        [ForeignKey("blog")]
        public string BlogId { set; get; }
        public Blog blog { set; get; }
        [ForeignKey("User")]
        public string UserId { set; get; }
        public ApplicationUser User { set; get; }

        public bool IsLike { set; get; }
        public Reaction() : base()
        {
            IsLike = true;
        }
    }
}



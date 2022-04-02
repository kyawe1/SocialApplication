using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialApplication.Areas.Admin.Data
{
    public class BlogViewModel
    {
        public string Id { set; get; }
        public string AuthorId { set; get; }
        public string Title { set; get; }
        public string Content { set; get; }
        public string AuthorEmail { set; get; }
        public DateTime CreatedAt { set; get; }
        public string Name { set; get; }
        public string ProfileId { set; get; }
        public long reactionCount { set; get; } = 0;
        public string ImageUrl { set; get; }

        public string GetUrl()
        {
            return $"~/uploads/blogs/{ ImageUrl }";
        }

    }
}
using System.ComponentModel.DataAnnotations;
using System;
using System.Web;

namespace SocialApplication.Models.ViewModels
{
    public class BlogCreateViewModel
    {
        public string Title { set; get; }
        [DataType(DataType.MultilineText)]
        public string Content { set; get; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Image { set; get; }
        public string oldimgurl { set; get; }
    }
    public class BlogViewModel
    {
        public string Id { set; get; }
        public string Title { set; get; }
        public string Content { set; get; }
        public string AuthorId { set; get; }
        public string AuthorEmail { set; get; }
        public DateTime CreatedAt { set; get; }
        public string Name { set; get; }
        public string ProfileId { set; get; }
        public long reactionCount { set; get; } = 0;
        public string ImageUrl { set; get; }
        public bool liked { set; get; } = false;

        public string GetUrl()
        {
            return $"~/uploads/blogs/{ ImageUrl }";
        }

    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialApplication.Models.Entity
{
    public class Base
    {
        public virtual string Id { set; get; }
        public virtual DateTime CreateAt { set; get; }
        public virtual DateTime UpdateAt { set; get; }

        public Base()
        {
            Id = Guid.NewGuid().ToString();
            CreateAt = DateTime.Now;
            UpdateAt = DateTime.Now;
        }
    }
}
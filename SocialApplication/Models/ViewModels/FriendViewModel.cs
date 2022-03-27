using System;
using System.Collections.Generic;

namespace SocialApplication.Models.ViewModels
{
    public class FriendViewModel
    {
        public string Id { set; get; }
        public string Sender_UserId { set; get; }
        public string Profile_Name { set; get; }
        public string Profile_Id { set; get; }
        public string Profile_Pic { set; get; }
        public DateTime RequestedAt { set; get; }
    }

    public class IndexFriendViewModel
    {
        public IEnumerable<FriendViewModel> Pending { set; get; }
        public IEnumerable<FriendViewModel> Accepted { set; get; }
    }
}


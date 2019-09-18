using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collective.Models
{
    public class UserFriend
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string FriendId { get; set; }
        public ApplicationUser Friend { get; set; }
        public bool Pending { get; set; }
        public DateTime DateAdded { get; set; }
    }
}

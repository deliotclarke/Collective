using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collective.Models
{
    public class NewsItem
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int RecordId { get; set; }
        public Record Record { get; set; }
        public string Title { get; set; }
        public string MemoryBody { get; set; }
        public bool TopFive { get; set; }
        public bool NeedList { get; set; }
        public string FriendId { get; set; }
        public ApplicationUser Friend { get; set; }
        public DateTime DateAdded { get; set; }
    }
}

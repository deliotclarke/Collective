using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collective.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int RecordId { get; set; }
        public Record Record { get; set; }
        public DateTime DateAdded { get; set; }
        public bool NeedList { get; set; }
        public bool TopFive { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collective.Models.ProfileViewModel
{
    public class ProfileViewModel
    {
        public ApplicationUser User { get; set; }
        public List<Collection> Collection { get; set; }
        public Record Record { get; set; }
        public List<Memory> Memories { get; set; }
    }
}

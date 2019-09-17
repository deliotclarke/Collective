using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collective.Models.HomeViewModel
{
    public class HomeViewModel
    {
        public List<Collection> Collections { get; set; }
        public List<Memory> Memories { get; set; }
        public Record Record { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}

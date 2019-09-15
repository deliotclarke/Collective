using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collective.Models.CollectionViewModel
{
    public class CollectionDetailMemoryViewModel
    {
        public Collection Collection { get; set; }
        public Record Record { get; set; }
        public List<Memory> Memories { get; set; }
    }
}

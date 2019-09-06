using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collective.Models
{
    public class DiscogsSearch
    {
        public class Pagination
        {
            public int Per_page { get; set; }
            public int Pages { get; set; }
            public int Page { get; set; }
            //public List<Urls> Urls { get; set; }
            public int Items { get; set; }
        }

        public class DiscogsPageResponse
        {
            public Pagination Pagination { get; set; }
            public IEnumerable<Record> Results { get; set; }
        }

    }
}

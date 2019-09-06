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
            public Urls Urls { get; set; }
            public int Items { get; set; }
        }

        public class Urls
        {
            public string Next { get; set; }
            public string Prev { get; set; }
            public string Last { get; set; }
            public string First { get; set; }
        }


        public class DiscogsPageResponse
        {
            public Pagination Pagination { get; set; }
            public IEnumerable<Record> Results { get; set; }
        }

    }
}

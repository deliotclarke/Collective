using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Collective.Models.DiscogsSearch;

namespace Collective.Models.SearchViewModel
{
    public class SearchIndexViewModel
    {
        public List<Record> Records { get; set; }
        public Record Record { get; set; }
        public Pagination Pagination { get; set; }
    }
}

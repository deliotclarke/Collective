using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collective.Models
{
    public class Record
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string RecordCompany { get; set; }
        public string Condition { get; set; }
        public string TrackList { get; set; }
        public string Barcode { get; set; }
    }
}

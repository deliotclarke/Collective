using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Collective.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [Display(Name = "User")]
        public ApplicationUser ApplicationUser { get; set; }
        public int RecordId { get; set; }
        public Record Record { get; set; }
        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }
        public bool NeedList { get; set; }
        [Display(Name = "Top Five")]
        public bool TopFive { get; set; }
    }
}

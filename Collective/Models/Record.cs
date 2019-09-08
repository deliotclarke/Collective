using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Collective.Models
{
    public class Record
    {
        public int Id { get; set; }
        [Display(Name = "Category Number")]
        public string Catno { get; set; }
        public string Thumb { get; set; }
        public string Cover_Image { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        [Display(Name = "Release Year")]
        public string Year { get; set; }
        [NotMapped]
        [Display(Name = "Label(s)")]
        public List<string> Label { get; set; }
        [NotMapped]
        public List<string> Style { get; set; }
        public Community Community { get; set; }
        public string Condition { get; set; }
        [Display(Name = "Lowest Price Available")]
        public string Lowest_Price { get; set; }
        [Display(Name = "Track List")]
        public string TrackList { get; set; }
        [NotMapped]
        [Display(Name = "Barcode(s)")]
        public List<string> Barcode { get; set; }
        public string Master_Url { get; set; }
        public bool Owned { get; set; }
        public bool TopFive { get; set; }
    }

    public class Barcode
    {
        public string SingleBarcode { get; set; }
    }

    public class Label
    {
        public string LabelName { get; set; }
    }

    public class Style
    {
        public string Id { get; set; }
        public string StyleName { get; set; }
    }

    public class Community
    {
        public int Id { get; set; }
        public int want { get; set; }
        public int have { get; set; }
    }
}

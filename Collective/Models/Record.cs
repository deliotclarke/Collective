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
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required]
        public int Master_Id { get; set; }
        [Required]
        [Display(Name = "Category Number")]
        public string Catno { get; set; }
        [Required]
        public string Thumb { get; set; }
        [Required]
        public string Cover_Image { get; set; }
        [Required]
        public string Artist { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Release Year")]
        public int Year { get; set; }
        [Required]
        public string Condition { get; set; }
        [Required]
        public string Master_Url { get; set; }

        [NotMapped]
        [Display(Name = "Label(s)")]
        public List<string> Label { get; set; }

        [NotMapped]
        public List<string> Style { get; set; }

        [NotMapped]
        public Community Community { get; set; }

        [NotMapped]
        [Display(Name = "Lowest Price Available")]
        public string Lowest_Price { get; set; }

        [NotMapped]
        [Display(Name = "Track List")]
        public List<Track> TrackList { get; set; }

        [NotMapped]
        [Display(Name = "Barcode(s)")]
        public List<string> Barcode { get; set; }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collective.Models
{
    public class Record
    {
        public int Id { get; set; }
        public string catno { get; set; }
        public string Thumb { get; set; }
        public string Cover_Image { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public List<Label> Label { get; set; }
        public List<Style> Style { get; set; }
        public List<Community> Community { get; set; }
        public string Condition { get; set; }
        public string LowestPrice { get; set; }
        public string TrackList { get; set; }
        public List<Barcode> Barcode { get; set; }
        public string Master_Url { get; set; }
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
        public string StyleName { get; set; }
    }

    public class Community
    {
        public int want { get; set; }
        public int have { get; set; }
    }
}

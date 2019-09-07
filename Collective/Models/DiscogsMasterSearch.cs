﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collective.Models
{
    public class DiscogsMasterSearch
    {
        public int Id { get; set; }
        public List<string> Styles { get; set; }
        public List<string> Genres { get; set; }
        public int Num_For_Sale { get; set; }
        public string Title { get; set; }
        public string Main_Release_Url { get; set; }
        public List<Artist> Artists { get; set; }
        public int Year { get; set; }
        public string Resource_Url { get; set; }
        public List<Track> Tracklist { get; set; }
    }

    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Track { get; set; }
    }

    public class Track
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
    }
}

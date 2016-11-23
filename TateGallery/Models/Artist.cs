using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TateGallery.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public int ActivePlaceCount { get; set; }

        public virtual PlaceTime Birth { get; set; }

        public virtual PlaceTime Death { get; set; }

        public int BirthYear { get; set; }

        public string Date { get; set; }

        public string fc { get; set; }

        public string Gender { get; set; }

        public string mda { get; set; }

        public virtual ICollection<Movement> Movements { get; set; }

        public string StartLetter { get; set; }

        public int TotalWorks { get; set; }

        public string Url { get; set; }
    }
}
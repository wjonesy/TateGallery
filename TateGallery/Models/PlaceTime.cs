using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TateGallery.Models
{
    public class PlaceTime
    {
        public int Id { get; set; }
        public Place Place { get; set; }
        public Time Time { get; set; }
    }

    public class Place
    {
        [Key]
        public string Name { get; set; }
        public string PlaceName { get; set; }
        public string PlaceType { get; set; }
    }

    public class Time
    {
        [Key]
        public int StartYear { get; set; }
    }
}
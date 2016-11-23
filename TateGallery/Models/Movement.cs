using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TateGallery.Models;

namespace TateGallery.Models
{
    public class Movement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Era Era { get; set; }
    }

    public class Era
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
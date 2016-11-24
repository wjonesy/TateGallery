using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TateGallery.Models
{
    public class CatalogueGroup
    {
        public int Id { get; set; }
        public string AccessionRanges { get; set; }
        public string CompleteStatus { get; set; }
        public int? GroupType { get; set; }
        public string ShortTitle { get; set; }
    }
}
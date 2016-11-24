using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TateGallery.Models
{
    public class Artwork
    {
        public int Id { get; set; }
        public string acno { get; set; }
        public int AcquisitionYear { get; set; }
        public string All_Artists { get; set; }
        public virtual CatalogueGroup CatalougeGroup { get; set; }
        public string Classification { get; set; }
        public int ContributorCount { get; set; }
        public virtual ICollection<Artist> Contributors{ get; set; }
        public string CreditLine { get; set; }
        public DateRange DateRange { get; set; }
        public string DateText { get; set; }
        public string Dept{ get; set; }
        public string Dimensions { get; set; }
        public string ForeignTitle { get; set; }
        public string GroupTitle { get; set; }
        public string Height { get; set; }
        public string Inscription { get; set; }
        public string Medium { get; set; }
        public int MovementCount { get; set; }
        public int SubjectCount { get; set; }
        //public ICollection<Children> Subjects { get; set; }
        public string ThumbnailCopyright { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Title { get; set; }
        public string Units { get; set; }
        public string Url { get; set; }
        public string Width { get; set; }
    }
}
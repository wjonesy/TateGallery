using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using TateGallery.Models;

namespace TateGallery.Persistence
{
    public class TateGalleryDbContext : DbContext
    {

        public TateGalleryDbContext() : base("TateGallery")
        {

        }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Place> Places { get; set; }

        public DbSet<Time> Times { get; set; }

        public DbSet<Movement> Movements { get; set; }
        public DbSet<Era> Eras { get; set; }

        public DbSet<Artwork> Artworks { get; set; }

        public DbSet<CatalogueGroup> CatalogueGroups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Artist>().HasMany(s => s.Movements);

            modelBuilder.Entity<Artwork>()
                .HasOptional(s => s.CatalougeGroup);



        }
    }
}


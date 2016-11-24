using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TateGallery.Models;
using TateGallery.Persistence;
using TateGallery.ViewModels;

namespace TateGallery.Controllers
{
    public class DataController : ApiController
    {
        //public IHttpActionResult Get()
        //{
        //    GetArtists();

        //    GetArtworks();

        //    return Ok("Done");
        //}

        public IHttpActionResult GetCumulative()
        {

            using (var _dbContext = new TateGalleryDbContext())
            {
                var sums = _dbContext.Database.SqlQuery<AquisitionsSumViewModel>(@"DECLARE @CountTable TABLE(ID INT IDENTITY(1, 1) primary key ,Aquisitions int, AcquisitionYear int)

insert into @CountTable 
SELECT 
SUM(count(AcquisitionYear)) OVER(PARTITION BY AcquisitionYear ORDER BY AcquisitionYear ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS Aquisitions,
 AcquisitionYear
FROM Artworks
where AcquisitionYear > 0
group by AcquisitionYear
ORDER BY AcquisitionYear



Select c1.AcquisitionYear, Sum(c2.Aquisitions) Acquisitions
From @CountTable c1,  @CountTable c2
Where c1.id >= c2.ID
Group By c1.ID, c1.Aquisitions,c1.AcquisitionYear
Order By c1.AcquisitionYear Asc").ToList();

                return Ok(sums);
            }
        }

        private void GetArtists()
        {

            var mappedPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Data/artists");

            var fileNames = Directory.GetFiles(mappedPath, "*.*", SearchOption.AllDirectories);

            foreach (var filePath in fileNames)
            {
                var text = File.ReadAllText(filePath);

                var artist = Newtonsoft.Json.JsonConvert.DeserializeObject<Artist>(text);

                using (var _dbContext = new TateGalleryDbContext())
                {
                    if (artist.Birth != null)
                    {
                        // time
                        var birth = artist.Birth;
                        if (birth.Time != null)
                        {
                            var dbTime = _dbContext.Times.Where(x => x.StartYear == birth.Time.StartYear).FirstOrDefault();
                            if (dbTime == null)
                            {
                                dbTime = new Time() { StartYear = birth.Time.StartYear };
                                _dbContext.Times.Add(dbTime);
                            }

                            artist.Birth.Time = dbTime;

                        }


                        // place
                        if (birth.Place != null)
                        {
                            var dbPlace = _dbContext.Places.Where(x => x.Name == birth.Place.Name).FirstOrDefault();
                            if (dbPlace == null)
                            {
                                dbPlace = new Place() { Name = birth.Place.Name, PlaceName = birth.Place.PlaceName, PlaceType = birth.Place.PlaceType };
                                _dbContext.Places.Add(dbPlace);

                            }

                            artist.Birth.Place = dbPlace;

                        }

                    }


                    if (artist.Death != null)
                    {
                        // time
                        var death = artist.Death;
                        if (death.Time != null)
                        {
                            var dbTime = _dbContext.Times.Where(x => x.StartYear == death.Time.StartYear).FirstOrDefault();
                            if (dbTime == null)
                            {
                                dbTime = new Time() { StartYear = death.Time.StartYear };
                                _dbContext.Times.Add(dbTime);


                            }

                            artist.Death.Time = dbTime;

                        }


                        // place
                        if (death.Place != null)
                        {
                            var dbPlace = _dbContext.Places.Where(x => x.Name == death.Place.Name).FirstOrDefault();
                            if (dbPlace == null)
                            {
                                if (artist.Birth != null && artist.Birth.Place != null && artist.Birth.Place.Name == death.Place.Name)
                                {
                                    artist.Death.Place = artist.Birth.Place;
                                }
                                else
                                {
                                    dbPlace = new Place() { Name = death.Place.Name, PlaceName = death.Place.PlaceName, PlaceType = death.Place.PlaceType };
                                    _dbContext.Places.Add(dbPlace);

                                }
                            }
                            artist.Death.Place = dbPlace;

                        }

                    }

                    if (artist.Movements.Any())
                    {
                        var movements = artist.Movements;
                        artist.Movements = new List<Movement>();
                        foreach (var movement in movements)
                        {
                            var dbMovement = _dbContext.Movements.Where(x => x.Id == movement.Id).FirstOrDefault();
                            if (dbMovement == null)
                            {
                                // check for eras 



                                dbMovement = new Movement()
                                {
                                    Id = movement.Id,
                                    Name = movement.Name
                                };
                            }

                            var dbEra = _dbContext.Eras.Where(x => x.Id == movement.Era.Id).FirstOrDefault();
                            if (dbEra == null)
                            {
                                // add to db
                                dbEra = new Era()
                                {
                                    Id = movement.Era.Id,
                                    Name = movement.Era.Name
                                };
                            }
                            dbMovement.Era = dbEra;


                            artist.Movements.Add(dbMovement);
                        }
                    }





                    _dbContext.Artists.Add(artist);
                    _dbContext.SaveChanges();

                }
            }
        }

        private void GetArtworks()
        {
            var mappedPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Data/artworks");

            var fileNames = Directory.GetFiles(mappedPath, "*.*", SearchOption.AllDirectories);

            foreach (var filePath in fileNames)
            {
                var text = File.ReadAllText(filePath);


                var artwork = Newtonsoft.Json.JsonConvert.DeserializeObject<Artwork>(text);

                using (var _dbContext = new TateGalleryDbContext())
                {
                    if (artwork.CatalougeGroup != null)
                    {
                        var dbCatalougeGroup = _dbContext.CatalogueGroups.Where(x => x.Id == artwork.CatalougeGroup.Id).FirstOrDefault();

                        if (dbCatalougeGroup == null)
                        {
                            var catGroup = artwork.CatalougeGroup;
                            _dbContext.CatalogueGroups.Add(catGroup);
                            artwork.CatalougeGroup = catGroup;
                        }
                        else
                        {
                            artwork.CatalougeGroup = dbCatalougeGroup;
                        }
                    }
                    if (artwork.DateRange == null)
                    {
                        artwork.DateRange = new DateRange();
                    }

                    if (artwork.Contributors.Any())
                    {
                        artwork.Contributors = new List<Artist>();
                        foreach (var contributor in artwork.Contributors)
                        {
                            var dbContributor = _dbContext.Artists.Where(x => x.Id == contributor.Id).FirstOrDefault();
                            if (dbContributor != null)
                            {
                                artwork.Contributors.Add(dbContributor);
                            }
                        }
                    }

                    _dbContext.Artworks.Add(artwork);
                    _dbContext.SaveChanges();
                }




            }
        }
    }
}

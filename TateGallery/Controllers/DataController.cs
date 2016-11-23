using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TateGallery.Models;
using TateGallery.Persistence;

namespace TateGallery.Controllers
{
    public class DataController : ApiController
    {
        public IHttpActionResult Get()
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
                                var time = new Time() { StartYear = birth.Time.StartYear };
                                _dbContext.Times.Add(time);
                                artist.Birth.Time = time;

                            }
                            else
                            {
                                artist.Birth.Time = dbTime;
                            }
                        }


                        // place
                        if (birth.Place != null)
                        {
                            var dbPlace = _dbContext.Places.Where(x => x.Name == birth.Place.Name).FirstOrDefault();
                            if (dbPlace == null)
                            {
                                var place = new Place() { Name = birth.Place.Name, PlaceName = birth.Place.PlaceName, PlaceType = birth.Place.PlaceType };
                                _dbContext.Places.Add(place);
                                artist.Birth.Place = place;
                            }
                            else
                            {
                                artist.Birth.Place = dbPlace;
                            }
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
                                var time = new Time() { StartYear = death.Time.StartYear };
                                _dbContext.Times.Add(time);
                                artist.Death.Time = time;

                            }
                            else
                            {
                                artist.Death.Time = dbTime;
                            }
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
                                    var place = new Place() { Name = death.Place.Name, PlaceName = death.Place.PlaceName, PlaceType = death.Place.PlaceType };
                                    _dbContext.Places.Add(place);
                                    artist.Death.Place = place;
                                }
                            }
                            else
                            {

                                artist.Death.Place = dbPlace;
                            }
                        }

                    }

                    if (artist.Movements.Any())
                    {
                        var movements = artist.Movements;
                        artist.Movements =  new List<Movement>();
                        foreach(var movement in movements)
                        {
                            var dbMovement = _dbContext.Movements.Where(x => x.Id == movement.Id).FirstOrDefault();
                            if(dbMovement == null)
                            {
                                // check for eras 

                                var m = new Movement() {
                                    Name = m.Name
                                };



                            }else
                            {
                                // check for eras
                                artist.Movements.Add(dbMovement);
                            }
                        }
                    }





                    _dbContext.Artists.Add(artist);
                    _dbContext.SaveChanges();

                }
            }

            return Ok("Done");
        }
    }
}

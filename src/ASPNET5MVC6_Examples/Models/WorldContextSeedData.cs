using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNET5MVC6_Examples.Models
{
    public class WorldContextSeedData
    {
        private WorldContext _context;

        public WorldContextSeedData(WorldContext context)
        {
            _context = context;
        }
        public void EnsureSeedData()
        {
            if (!_context.Trips.Any())
            {
                // Insertando Data
                var LimaTrip = new Trip()
                {
                    Name = "Viaje a Lima",
                    Created = DateTime.UtcNow,
                    UserName = "",
                    Stops = new List<Stop>()
                    {
                        new Stop() {  Name= "Tumbes", Arrival = new DateTime(2016,6,4),Latitude = 33.7,Longitude = -84.38,Order = 1 },
                        new Stop() {  Name= "Piura", Arrival = new DateTime(2016,6,4),Latitude = 33.7,Longitude = -84.38,Order = 1 },
                        new Stop() {  Name= "Lambayeque", Arrival = new DateTime(2016,6,4),Latitude = 33.7,Longitude = -84.38,Order = 1 },
                        new Stop() {  Name= "Arequipa", Arrival = new DateTime(2016,6,4),Latitude = 33.7,Longitude = -84.38,Order = 1 },
                        
                    }

                };

                _context.Trips.Add(LimaTrip);
                _context.Stops.AddRange(LimaTrip.Stops);
                    
                 
                var CuzcoTrip = new Trip()
                {
                    Name = "Viaje a Cuzco",
                    Created = DateTime.UtcNow,
                    UserName = "",
                    Stops = new List<Stop>()
                    {
                        new Stop() {  Name= "Tacna", Arrival = new DateTime(2016,6,4),Latitude = 33.7,Longitude = -84.38,Order = 1 },
                        new Stop() {  Name= "Puno", Arrival = new DateTime(2016,6,4),Latitude = 33.7,Longitude = -84.38,Order = 1 },
                        new Stop() {  Name= "Ucayali", Arrival = new DateTime(2016,6,4),Latitude = 33.7,Longitude = -84.38,Order = 1 },
                        new Stop() {  Name= "Moquegua", Arrival = new DateTime(2016,6,4),Latitude = 33.7,Longitude = -84.38,Order = 1 }
                    }

                };

                _context.Trips.Add(CuzcoTrip);
                _context.Stops.AddRange(CuzcoTrip.Stops);

                _context.SaveChanges();

            }
        }
    }
}

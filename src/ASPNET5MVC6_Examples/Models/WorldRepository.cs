﻿using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNET5MVC6_Examples.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;
        private ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context,ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddStop(string tripName, Stop newStop)
        {
            var theTrip = GetTripByName(tripName);
            newStop.Order = theTrip.Stops.Max(s=>s.Order)+1;
            _context.Stops.Add(newStop);
        }

        public void AddTrip(Trip newTrip)
        {
            _context.Add(newTrip);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            try
            {
                return _context.Trips.OrderBy(t => t.Name).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("No se pudo obtener los viajes de la BD", ex);
                return null;
            }
            
        }

        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            try
            {
                return _context.Trips.Include(t => t.Stops).OrderBy(t => t.Name).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("No se pudo obtener los viajes y paradas de la BD", ex);
                return null;
            }
            
        }

        public Trip GetTripByName(string tripName)
        {
            return _context.Trips.Include(t => t.Stops).Where(t=>t.Name == tripName).FirstOrDefault();
         }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Roamler.Domain.Interfaces;
using Roamler.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Roamler.Data
{
    public class EfLocationRepository : ILocationRepository
    {
        private readonly DbSet<Entities.Location> _locations;

        public EfLocationRepository(ApplicationContext applicationContext)
        {
            _locations = applicationContext.Set<Entities.Location>();
        }

        public IEnumerable<Location> GetLocations(Location location, int maxDistanceInMeters, int maxNumberOfResults)
        {
            return _locations.Where(x => location.CalculateDistanceInMeters(x.Latitude, x.Longitude) <= maxDistanceInMeters)
                .OrderBy(x => location.CalculateDistanceInMeters(x.Latitude, x.Longitude))
                .Take(maxNumberOfResults)
                .Select(x => new Location(x.Address, x.Latitude, x.Longitude))
                .ToList();
        }
    }
}

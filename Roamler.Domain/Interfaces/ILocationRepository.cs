using Roamler.Domain.Models;
using System.Collections.Generic;

namespace Roamler.Domain.Interfaces
{
    public interface ILocationRepository
    {
        IEnumerable<Location> GetLocations(Location location, int maxDistanceInMeters, int maxNumberOfResults);
    }
}

using Roamler.Application.ViewModels;
using Roamler.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Roamler.Application.Extensions
{
    public static class LocationExtensions
    {
        public static SearchResultViewModel MapToSearchResultViewModel(this IEnumerable<Location> locations, Location sourceLocation)
        {
            return locations == null
                ? null
                : new SearchResultViewModel
                {
                    NumberOfLocations = locations.Count(),
                    Locations = locations.Select(x => new LocationViewModel
                    {
                        Address = x.Address,
                        Latitude = x.Latitude,
                        Longitude = x.Longitude,
                        DistanceInMeters = sourceLocation.CalculateDistanceInMeters(x.Latitude, x.Longitude)
                    })
                        .OrderBy(x => x.DistanceInMeters)
                        .ToList()
                };
        }
    }
}

using Roamler.Application.Extensions;
using Roamler.Domain.Models;
using System.Collections.Generic;
using Xunit;

namespace Roamler.Application.Tests.Extensions
{
    public class LocationExtensionsTests
    {
        [Fact]
        public void MapToSearchResultViewModel_ValidCollection_MapsToSearchResultViewModel()
        {
            var sourceLocation = new Location("Porto Airport", 41.23720273839443, -8.670448786460488);

            var locations = new List<Location>
            {
                new Location("Westerdoksdijk 411 1013 AD Amsterdam", 52.38532820808234, 4.8930486868251)
            };

            var searchViewModel = locations.MapToSearchResultViewModel(sourceLocation);

            Assert.Equal(locations.Count, searchViewModel.NumberOfLocations);
            Assert.Collection(searchViewModel.Locations,
                item =>
                {
                    Assert.Equal("Westerdoksdijk 411 1013 AD Amsterdam", item.Address);
                    Assert.Equal(52.38532820808234, item.Latitude);
                    Assert.Equal(4.8930486868251, item.Longitude);
                    Assert.Equal(sourceLocation.CalculateDistanceInMeters(item.Latitude, item.Longitude), item.DistanceInMeters);
                });
        }

        [Fact]
        public void MapToSearchResultViewModel2_NullCollection_ReturnsNull()
        {
            var sourceLocation = new Location("Porto Airport", 41.23720273839443, -8.670448786460488);

            IEnumerable<Location> locations = null;

            var searchViewModel = locations.MapToSearchResultViewModel(sourceLocation);

            Assert.Null(searchViewModel);
        }
    }
}

using Microsoft.Extensions.Logging;
using Moq;
using Roamler.Application.Services;
using Roamler.Domain.Interfaces;
using Roamler.Domain.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Roamler.Application.Tests.Services
{
    public class LocationAppServiceTests
    {
        private readonly Mock<ILocationRepository> _locationRepositoryMock;
        private readonly Mock<ILogger<LocationAppService>> _loggerMock;

        private readonly LocationAppService _locationAppService;

        public LocationAppServiceTests()
        {
            _locationRepositoryMock = new Mock<ILocationRepository>();
            _loggerMock = new Mock<ILogger<LocationAppService>>();

            _locationAppService = new LocationAppService(_locationRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void GetLocations_ValidCoordinates_ReturnsSearchResultViewModel()
        {
            var address = "Westerdoksdijk 411 1013 AD Amsterdam";
            var latitude = 41.23720273839443;
            var longitude = -8.670448786460488;
            var maxDistanceInMeters = 50;
            var maxNumberOfResults = 10;

            var locations = new List<Location>
            {
                new Location(address, latitude, longitude)
            };

            _locationRepositoryMock.Setup(x => x.GetLocations(It.IsAny<Location>(), maxDistanceInMeters, maxNumberOfResults))
                .Returns(locations);

            var searchResult = _locationAppService.GetLocations(latitude, longitude, maxDistanceInMeters, maxNumberOfResults);

            Assert.NotNull(searchResult);
            Assert.Equal(locations.Count, searchResult.NumberOfLocations);
            Assert.Collection(searchResult.Locations,
                item =>
                {
                    Assert.Equal(address, item.Address);
                    Assert.Equal(latitude, item.Latitude);
                    Assert.Equal(longitude, item.Longitude);
                    Assert.Equal(0, item.DistanceInMeters);
                });
        }

        [Fact]
        public void GetLocations_InvalidCoordinates_LogsAndThrowsException()
        {
            var latitude = 91;
            var longitude = -8.670448786460488;
            var maxDistanceInMeters = 50;
            var maxNumberOfResults = 10;

            Assert.Throws<ArgumentOutOfRangeException>(() => _locationAppService.GetLocations(latitude, longitude, maxDistanceInMeters, maxNumberOfResults));

            _loggerMock.Verify(l =>
                l.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);
        }
    }
}

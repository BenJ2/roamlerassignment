using Microsoft.AspNetCore.Mvc;
using Moq;
using Roamler.Application.Interfaces;
using Roamler.WebApi.Controllers;
using Xunit;

namespace Roamler.WebApi.Tests
{
    public class LocationsControllerTests
    {
        private readonly Mock<ILocationAppService> _locationAppService;
        private readonly LocationsController _locationsController;

        public LocationsControllerTests()
        {
            _locationAppService = new Mock<ILocationAppService>();

            _locationsController = new LocationsController(_locationAppService.Object);
        }

        [Fact]
        public void GetLocations_CallsAppServiceOnce()
        {
            const double latitude = 41.23720273839443;
            const double longitude = -8.670448786460488;
            const int maxDistanceInMeters = 50;
            const int maxNumberOfResults = 100;

            var result = _locationsController.GetLocations(latitude, longitude, maxDistanceInMeters, maxNumberOfResults);

            Assert.IsType<OkObjectResult>(result);
            _locationAppService.Verify(x => x.GetLocations(latitude, longitude, maxDistanceInMeters, maxNumberOfResults), Times.Once);
        }
    }
}

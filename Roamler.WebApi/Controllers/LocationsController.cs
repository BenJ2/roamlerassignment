using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roamler.Application.Interfaces;
using Roamler.Application.ViewModels;

namespace Roamler.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationAppService _locationAppService;

        public LocationsController(ILocationAppService locationAppService)
        {
            _locationAppService = locationAppService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(SearchResultViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetLocations(
            [FromQuery] double latitude,
            [FromQuery] double longitude,
            [FromQuery] int maxDistanceInMeters,
            [FromQuery] int maxNumberOfResults)
        {
            var locations = _locationAppService.GetLocations(latitude, longitude, maxDistanceInMeters, maxNumberOfResults);

            return Ok(locations);
        }
    }
}

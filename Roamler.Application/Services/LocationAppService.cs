using Microsoft.Extensions.Logging;
using Roamler.Application.Extensions;
using Roamler.Application.Interfaces;
using Roamler.Application.ViewModels;
using Roamler.Domain.Interfaces;
using Roamler.Domain.Models;
using System;

namespace Roamler.Application.Services
{
    public class LocationAppService : ILocationAppService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ILogger _logger;

        public LocationAppService(ILocationRepository locationRepository, ILogger<LocationAppService> logger)
        {
            _locationRepository = locationRepository;
            _logger = logger;
        }

        public SearchResultViewModel GetLocations(double latitude, double longitude, int maxDistanceInMeters, int maxNumberOfResults)
        {
            try
            {
                var sourceLocation = new Location(latitude, longitude);

                var locations = _locationRepository.GetLocations(
                    sourceLocation,
                    maxDistanceInMeters,
                    maxNumberOfResults);

                return locations.MapToSearchResultViewModel(sourceLocation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }
    }
}

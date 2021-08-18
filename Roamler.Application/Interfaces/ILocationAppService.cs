using Roamler.Application.ViewModels;

namespace Roamler.Application.Interfaces
{
    public interface ILocationAppService
    {
        SearchResultViewModel GetLocations(double latitude, double longitude, int maxDistanceInMeters, int maxNumberOfResults);
    }
}

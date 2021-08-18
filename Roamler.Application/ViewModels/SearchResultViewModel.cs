using System.Collections.Generic;

namespace Roamler.Application.ViewModels
{
    public class SearchResultViewModel
    {
        public int NumberOfLocations { get; set; }

        public IEnumerable<LocationViewModel> Locations { get; set; }
    }
}

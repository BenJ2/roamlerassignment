using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using CsvHelper;
using Roamler.Domain.Interfaces;

namespace Roamler.Data
{
    public class LocationRepository : ILocationRepository
    {
        private const string CsvFileName = "locations.csv";

        public IEnumerable<Domain.Models.Location> GetLocations(Domain.Models.Location location, int maxDistanceInMeters, int maxNumberOfResults)
        {
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var csvFilePath = Path.Combine(assemblyPath, CsvFileName);

            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var anonymousTypeDefinition = new
                {
                    Address = string.Empty,
                    Latitude = default(double),
                    Longitude = default(double)
                };

                var records = csv.GetRecords(anonymousTypeDefinition)
                    .Where(x => location.CalculateDistanceInMeters(x.Latitude, x.Longitude) <= maxDistanceInMeters)
                    .Select(x => new Domain.Models.Location(x.Address, x.Latitude, x.Longitude))
                    .OrderBy(x => location.CalculateDistanceInMeters(x.Latitude, x.Longitude))
                    .Take(maxNumberOfResults)
                    .OrderBy(x => x.Address)
                    .ToList();

                return records;
            }
        }
    }
}

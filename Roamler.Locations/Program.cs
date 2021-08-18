using CsvHelper;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
//using System.Drawing;

namespace Roamler.Locations
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var maxDistance = 1000;

            var currentLocation = new Location(52.2165425, 5.4778534);
            var locations = new List<Location>();

            using (var reader = new StreamReader("locations.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var anonymousTypeDefinition = new
                {
                    Address = string.Empty,
                    Latitude = default(double),
                    Longitude = default(double)
                };

                var records = csv.GetRecords(anonymousTypeDefinition)                    
                    .Select(x => new
                    {
                        x.Address,
                        x.Latitude,
                        x.Longitude,                        
                        DistanceInMeters = currentLocation.CalculateDistance(x.Latitude, x.Longitude)
                    })
                    .Where(x => x.DistanceInMeters < maxDistance)
                    .OrderBy(x => x.DistanceInMeters)
                    .ToList();

                foreach (var record in records)
                {
                    var distance = currentLocation.CalculateDistance(record.Latitude, record.Longitude);

                    if (distance < maxDistance)
                        locations.Add(currentLocation.Add(record.Latitude, record.Longitude));
                }

            }
        }
    }
}

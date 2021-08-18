using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Roamler.Data.Entities;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Roamler.Data.Extensions
{
    internal static class ModelBuilderExtensions
    {
        private const string CsvFileName = "locations.csv";

        internal static void SeedLocations(this ModelBuilder modelBuilder)
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
                    .Select(x =>
                        new Location
                        {
                            Id = Guid.NewGuid(),
                            Address = x.Address,
                            Latitude = x.Latitude,
                            Longitude = x.Longitude
                        })
                    .ToList();

                modelBuilder.Entity<Location>().HasData(records);
            }
        }
    }
}

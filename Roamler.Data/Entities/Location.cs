using System;

namespace Roamler.Data.Entities
{
    public class Location
    {
        public Guid Id { get; set; }

        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}

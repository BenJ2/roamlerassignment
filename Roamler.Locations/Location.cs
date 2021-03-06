using System;
using System.Collections.Generic;
using System.Text;

namespace Roamler.Locations
{
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Creates a new location that is <paramref name="offsetLat"/>, <paramref name="offsetLon"/> meters from this location.
        /// </summary>
        public Location Add(double offsetLat, double offsetLon)
        {
            double latitude = Latitude + (offsetLat / 111111d);
            double longitude = Longitude + (offsetLon / (111111d * Math.Cos(latitude)));

            return new Location(latitude, longitude);
        }

        /// <summary>
        /// Calculates the distance between this location and another one, in meters.
        /// </summary>
        public double CalculateDistance(double latitude, double longitude)
        {
            var rlat1 = Math.PI * Latitude / 180;
            var rlat2 = Math.PI * latitude / 180;
            var theta = Longitude - longitude;
            var rtheta = Math.PI * theta / 180;
            var dist = Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            return dist * 1609.344;
        }

        public override string ToString()
        {
            return Latitude + ", " + Longitude;
        }
    }
}

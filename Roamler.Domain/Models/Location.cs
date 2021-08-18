using System;

namespace Roamler.Domain.Models
{
    public class Location
    {
        public string Address { get; private set; }

        public double Latitude { get; private set; }

        public double Longitude { get; private set; }

        public Location(double latitude, double longitude)
        {
            ValidateCoordinates(latitude, longitude);

            Latitude = latitude;
            Longitude = longitude;
        }

        public Location(string address, double latitude, double longitude)
            : this(latitude, longitude)
        {
            if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Address can not be null or empty.");

            Address = address;
        }

        /// <summary>
        /// Calculates the distance between this location and another one, in meters.
        /// </summary>
        public double CalculateDistanceInMeters(double latitude, double longitude)
        {
            ValidateCoordinates(latitude, longitude);

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
            return $"{Latitude}, {Longitude}";
        }

        private void ValidateCoordinates(double latitude, double longitude)
        {
            if (latitude < -90 || latitude > 90) throw new ArgumentOutOfRangeException("Latitude must be between -90 and 90.");
            if (longitude < -180 || longitude > 180) throw new ArgumentOutOfRangeException("Longitude must be between -180 and 180.");
        }
    }
}

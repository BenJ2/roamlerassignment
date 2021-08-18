using System;
using Roamler.Domain.Models;
using Xunit;

namespace Roamler.Domain.Tests.Models
{
    public class LocationTests
    {
        [Fact]
        public void ConstructorWithAddressAndCoordinatesParameters_ValidParameters_CreatesInstance()
        {
            const string address = "Westerdoksdijk 411 1013 AD Amsterdam";
            const double latitude = 52.38532820808234;
            const double longitude = 4.8930486868251;

            var location = new Location(address, latitude, longitude);

            Assert.Equal(address, location.Address);
            Assert.Equal(latitude, location.Latitude);
            Assert.Equal(longitude, location.Longitude);
        }

        [Fact]
        public void ConstructorWithAddressAndCoordinatesParameters_InvalidLatitude_ThrowsException()
        {
            const string address = "Westerdoksdijk 411 1013 AD Amsterdam";
            const double latitude = -200;
            const double longitude = 4.8930486868251;

            Assert.Throws<ArgumentOutOfRangeException>(() => new Location(address, latitude, longitude));
        }

        [Fact]
        public void ConstructorWithAddressAndCoordinatesParameters_InvalidLongitude_ThrowsException()
        {
            const string address = "Westerdoksdijk 411 1013 AD Amsterdam";
            const double latitude = -200;
            const double longitude = 4.8930486868251;

            Assert.Throws<ArgumentOutOfRangeException>(() => new Location(address, latitude, longitude));
        }

        [Fact]
        public void ConstructorWithCoordinatesParameters_ValidParameters_CreatesInstance()
        {
            const double latitude = 52.38532820808234;
            const double longitude = 4.8930486868251;

            var location = new Location(latitude, longitude);

            Assert.Null(location.Address);
            Assert.Equal(latitude, location.Latitude);
            Assert.Equal(longitude, location.Longitude);
        }

        [Fact]
        public void ConstructorWithCoordinatesParameters_InvalidLatitude_ThrowsException()
        {
            const double latitude = -100;
            const double longitude = 4.8930486868251;

            Assert.Throws<ArgumentOutOfRangeException>(() => new Location(latitude, longitude));
        }

        [Fact]
        public void ConstructorWithCoordinatesParameters_InvalidLongitude_ThrowsException()
        {
            const double latitude = 52.38532820808234;
            const double longitude = 190;

            Assert.Throws<ArgumentOutOfRangeException>(() => new Location(latitude, longitude));
        }

        [Fact]
        public void CalculateDistanceInMeters_InvalidLatitude_ThrowsException()
        {
            const double latitude = 52.38532820808234;
            const double longitude = 4.8930486868251;
            const double invalidLatitude = 100;

            var location = new Location(latitude, longitude);

            Assert.Throws<ArgumentOutOfRangeException>(() => location.CalculateDistanceInMeters(invalidLatitude, longitude));
        }

        [Fact]
        public void CalculateDistanceInMeters_InvalidLongitude_ThrowsException()
        {
            const double latitude = 52.38532820808234;
            const double longitude = 4.8930486868251;
            const double invalidLongitude = 190;

            var location = new Location(latitude, longitude);

            Assert.Throws<ArgumentOutOfRangeException>(() => location.CalculateDistanceInMeters(latitude, invalidLongitude));
        }

        [Fact]
        public void CalculateDistanceInMeters_ValidCoordinates_CalculatesDistanceInMeters()
        {
            const double sourceLatitude = 41.23720273839443;
            const double sourceLongitude = -8.670448786460488;
            const double targetLatitude = 52.31060417384597;
            const double targetLongitude = 4.768306584973536;

            var location = new Location(sourceLatitude, sourceLongitude);

            var distanceInMeters = location.CalculateDistanceInMeters(targetLatitude, targetLongitude);

            Assert.Equal(1595733.6687250568, distanceInMeters);
        }

        [Fact]
        public void ToString_ReturnsCoordinates()
        {
            const string address = "Westerdoksdijk 411 1013 AD Amsterdam";
            const double latitude = 41.23720273839443;
            const double longitude = -8.670448786460488;

            var location = new Location(address, latitude, longitude);

            var coordinates = location.ToString();

            Assert.Equal($"{latitude}, {longitude}", coordinates);
        }
    }
}

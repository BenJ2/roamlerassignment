using NetTopologySuite.Geometries;

namespace Roamler.Locations.Entities
{
    class Location
    {
        public string Address { get; set; }

        public Point Point { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostcodeSearch.Models
{
    public class Postcodes
    {
        public int Id { get; set; }
        public string Thoroughfare { get; set; }
        public string Posttown { get; set; }
        public string Postcode { get; set; }
        public int Easting { get; set; }
        public int Northing { get; set; }

        public double DistanceFrom(Postcodes destination)
        {
            var maxEasting = Math.Max(destination.Easting, Easting);
            var minEasting = Math.Min(destination.Easting, Easting);
            var maxNorthing = Math.Max(destination.Northing, Northing);
            var minNorthing = Math.Min(destination.Northing, Northing);

            var easting = Math.Pow(maxEasting - minEasting, 2);
            var northing = Math.Pow(maxNorthing - minNorthing, 2);

            return Math.Sqrt(northing + easting);
        }
    }
}
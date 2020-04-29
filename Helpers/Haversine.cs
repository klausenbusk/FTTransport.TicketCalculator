using System;

namespace FTTransport.TicketCalculator {
    // Implementeret ud fra:
    // https://en.wikipedia.org/wiki/Haversine_formula
    // http://www.movable-type.co.uk/scripts/latlong.html
    class Haversine {
        private static double toRadian(double input) {
            return input * Math.PI / 180;
        }

        // Returnerer distancen mellem to positioner i km
        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2) {
            // Earth mean radius (i km)
            const double R = 6371;
            // Delta i radian
            double dLat = toRadian(lat2 - lat1);
            double dLon = toRadian(lon2 - lon1);
            // Distance
            double d = Math.Pow(Math.Sin(dLat / 2), 2) + Math.Cos(toRadian(lat1)) * Math.Cos(toRadian(lat2)) * Math.Pow(Math.Sin(dLon / 2), 2);
            d = 2 * R * Math.Asin(Math.Sqrt(d));
            return d;
        }
    }
}

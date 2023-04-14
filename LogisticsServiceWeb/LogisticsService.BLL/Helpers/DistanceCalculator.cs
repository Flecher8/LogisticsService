using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Helpers
{
    public class DistanceCalculator
    {
        private const double EarthRadiusKm = 6371.0; // Радиус Земли в километрах

        public static double CalculateDistance(Coordinate coord1, Coordinate coord2)
        {
            double lat1Rad = DegreeToRadians(coord1.Latitude);
            double lon1Rad = DegreeToRadians(coord1.Longitude);
            double lat2Rad = DegreeToRadians(coord2.Latitude);
            double lon2Rad = DegreeToRadians(coord2.Longitude);

            double deltaLat = lat2Rad - lat1Rad;
            double deltaLon = lon2Rad - lon1Rad;

            double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                       Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                       Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = EarthRadiusKm * c;
            return distance;
        }

        private static double DegreeToRadians(double degree)
        {
            return degree * (Math.PI / 180);
        }
    }
}

using LogisticsService.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Services
{
    public static class SizeConversionService
    {
        private const double InchesToCentimetersRatio = 2.54;

        public static double InchesToCentimeters(double inches)
        {
            return inches * InchesToCentimetersRatio;
        }

        public static double CentimetersToInches(double centimeters)
        {
            return centimeters / InchesToCentimetersRatio;
        }
    }
}

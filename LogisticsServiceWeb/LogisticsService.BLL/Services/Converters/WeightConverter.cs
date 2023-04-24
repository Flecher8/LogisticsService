using LogisticsService.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Services.Converters
{
    public static class WeightConverter
    {
        private const double PoundsToKilogramsFactor = 0.45359237;
        private const double KilogramsToPoundsFactor = 2.20462;

        public static double PoundsToKilograms(double pounds)
        {
            return pounds * PoundsToKilogramsFactor;
        }

        public static double KilogramsToPounds(double kilograms)
        {
            return kilograms * KilogramsToPoundsFactor;
        }
    }
}

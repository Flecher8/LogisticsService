using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.Helpers
{
    public class PriceCalculator
    {
        private double PriceForMeasure;
        private double PathLengthInMeasure;

        public PriceCalculator(double priceForMeasure, double pathLengthInMeasure)
        {
            PriceForMeasure = priceForMeasure;
            PathLengthInMeasure = pathLengthInMeasure;
        }

        public double Compute()
        {
            return Math.Round(PriceForMeasure * PathLengthInMeasure, 2);
        }
    }
}

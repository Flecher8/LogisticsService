using LogisticsService.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Services.Converters
{
    public class CargoConverter
    {
        private string WeightMeasureUnit;
        private string SizeMeasureUnit;

        public CargoConverter(string weightMeasureUnit, string sizeMeasureUnit)
        {
            WeightMeasureUnit = weightMeasureUnit;
            SizeMeasureUnit = sizeMeasureUnit;
        }

        public CargoSizeType GetCargoSizeType()
        {
            return (CargoSizeType)Enum.Parse(typeof(CargoSizeType), SizeMeasureUnit);
        }

        public CargoWeightType GetCargoWeightType()
        {
            return (CargoWeightType)Enum.Parse(typeof(CargoWeightType), WeightMeasureUnit);
        }

        public double ConvertCargoSizeToCm(double value)
        {
            return GetCargoSizeType()
                .Equals(CargoSizeType.cm) ? value :
                SizeConversionService.InchesToCentimeters(value);
        }

        public double ConvertCargoSizeToInch(double value)
        {
            return GetCargoSizeType()
                .Equals(CargoSizeType.inch) ? value :
                SizeConversionService.CentimetersToInches(value);
        }

        public double ConvertCargoWeightToKg(double value)
        {
            return GetCargoWeightType()
                .Equals(CargoWeightType.kg) ? value :
                WeightConverterService.PoundsToKilograms(value);
        }

        public double ConvertCargoWeightToLb(double value)
        {
            return GetCargoWeightType()
                .Equals(CargoWeightType.lb) ? value :
                WeightConverterService.KilogramsToPounds(value);
        }
    }
}

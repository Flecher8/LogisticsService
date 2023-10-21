using LogisticsService.Core.Dtos;
using LogisticsService.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Services.Validators
{
    public class CargoValidator
    {
        public bool IsCargoValid(CargoDto cargoDto)
        {
            if (!IsCargoSizesValid(cargoDto))
            {
                throw new ArgumentOutOfRangeException("Size values are not correct");
            }
            if (!IsCargoSizeTypeValid(cargoDto.SizeMeasureUnit))
            {
                throw new ArgumentOutOfRangeException("Cargo size measure unit is not valid");
            }

            if (!IsCargoWeightTypeValid(cargoDto.WeightMeasureUnit))
            {
                throw new ArgumentOutOfRangeException("Cargo weight measure unit is not valid");
            }

            return true;
        }

        private bool IsCargoSizesValid(CargoDto cargoDto)
        {
            if (cargoDto.Weight <= 0 ||
                cargoDto.Length <= 0 ||
                cargoDto.Width <= 0 ||
                cargoDto.Height <= 0)
            {
                return false;
            }
            return true;
        }

        public bool IsCargoTypesValid(string cargoSizeType, string cargoWeightType)
        {
            if (!IsCargoSizeTypeValid(cargoSizeType))
            {
                throw new ArgumentOutOfRangeException("Cargo size measure unit is not valid");
            }
            if (!IsCargoWeightTypeValid(cargoWeightType))
            {
                throw new ArgumentOutOfRangeException("Cargo weight measure unit is not valid");
            }
            return true;
        }


        private bool IsCargoSizeTypeValid(string cargoSizeType)
        {
            return Enum.IsDefined(typeof(CargoSizeType), cargoSizeType);
        }

        private bool IsCargoWeightTypeValid(string cargoWeightType)
        {
            return Enum.IsDefined(typeof(CargoWeightType), cargoWeightType);
        }
    }
}

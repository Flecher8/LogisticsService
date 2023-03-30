using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using LogisticsService.Core.Enums;
using LogisticsService.DAL.Interfaces;
using LogisticsService.DAL.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Services
{
    public class CargoService : ICargoService
    {
        private readonly IDataRepository<Cargo> _cargoRepository;

        private readonly ILogger<CargoService> _logger;

        public CargoService(IDataRepository<Cargo> cargoRepository, ILogger<CargoService> logger)
        {
            _cargoRepository = cargoRepository;
            _logger = logger;
        }

        public void CreateCargo(CargoDto cargoDto)
        {
            IsCargoValid(cargoDto);

            Cargo cargo = new Cargo();
            cargo.Weight = ConvertCargoWeightToKg(cargoDto.Weight, GetCargoWeightType(cargoDto.WeightMeasureUnit));
            cargo.Length = ConvertCargoSizeToCm(cargoDto.Length, GetCargoSizeType(cargoDto.SizeMeasureUnit));
            cargo.Width = ConvertCargoSizeToCm(cargoDto.Width, GetCargoSizeType(cargoDto.SizeMeasureUnit));
            cargo.Height = ConvertCargoSizeToCm(cargoDto.Height, GetCargoSizeType(cargoDto.SizeMeasureUnit));
            cargo.Description = cargoDto.Description;


            try
            {
                _cargoRepository.InsertItem(cargo);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        // TODO Move this methods to another class...
        private bool IsCargoValid(CargoDto cargoDto)
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


        private bool IsCargoSizeTypeValid(string cargoSizeType)
        {
            return Enum.IsDefined(typeof(CargoSizeType), cargoSizeType);
        }

        private CargoSizeType GetCargoSizeType(string cargoSizeType)
        {
            return (CargoSizeType)Enum.Parse(typeof(CargoSizeType), cargoSizeType);
        }


        private bool IsCargoWeightTypeValid(string cargoWeightType)
        {
            return Enum.IsDefined(typeof(CargoWeightType), cargoWeightType);
        }

        private CargoWeightType GetCargoWeightType(string cargoWeightType)
        {
            return (CargoWeightType)Enum.Parse(typeof(CargoWeightType), cargoWeightType);
        }


        private double ConvertCargoSizeToCm(double value, CargoSizeType cargoSizeType)
        {
            return cargoSizeType.Equals(CargoSizeType.cm) ? value : 
                SizeConversionService.InchesToCentimeters(value);
        }

        private double ConvertCargoSizeToInch(double value, CargoSizeType cargoSizeType)
        {
            return cargoSizeType.Equals(CargoSizeType.inch) ? value :
                SizeConversionService.CentimetersToInches(value);
        }


        private double ConvertCargoWeightToKg(double value, CargoWeightType cargoWeightType)
        {
            return cargoWeightType.Equals(CargoWeightType.kg) ? value :
                WeightConverterService.PoundsToKilograms(value);
        }

        private double ConvertCargoWeightToLb(double value, CargoWeightType cargoWeightType)
        {
            return cargoWeightType.Equals(CargoWeightType.lb) ? value :
                WeightConverterService.KilogramsToPounds(value);
        }






        public void DeleteCargo(int cargoId)
        {
            try
            {
                _cargoRepository.DeleteItem(cargoId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public CargoDto? GetCargoById(int cargoId, string cargoSizeType = "cm", string cargoWeightType = "kg")
        {
            IsCargoTypesValid(cargoSizeType, cargoWeightType);
            try
            {
                Cargo? cargo = _cargoRepository.GetItemById(cargoId);
                if(cargo == null)
                {
                    return null;
                }

                return CreateCargoDtoFromCargo(cargo, cargoSizeType, cargoWeightType);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        private CargoDto CreateCargoDtoFromCargo(Cargo cargo, string cargoSizeType, string cargoWeightType)
        {
            CargoSizeType sizeType = GetCargoSizeType(cargoSizeType);
            CargoWeightType weightType = GetCargoWeightType(cargoWeightType);

            CargoDto cargoDto = new CargoDto();
            cargoDto.CargoId = cargo.CargoId;
            cargoDto.Description = cargo.Description;
            cargoDto.WeightMeasureUnit = weightType.ToString();
            cargoDto.SizeMeasureUnit = sizeType.ToString();

            if (!sizeType.Equals(CargoSizeType.cm))
            {
                cargoDto.Length = SizeConversionService.CentimetersToInches(cargo.Length);
                cargoDto.Width = SizeConversionService.CentimetersToInches(cargo.Width);
                cargoDto.Height = SizeConversionService.CentimetersToInches(cargo.Height);
            }
            else
            {
                cargoDto.Length = cargo.Length;
                cargoDto.Width = cargo.Width;
                cargoDto.Height = cargo.Height;
            }

            if (!weightType.Equals(CargoWeightType.kg))
            {
                cargoDto.Weight = WeightConverterService.KilogramsToPounds(cargo.Weight);
            }
            else
            {
                cargoDto.Weight = cargo.Weight;
            }

            return cargoDto;
        }


        private bool IsCargoTypesValid(string cargoSizeType, string cargoWeightType)
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



        public List<CargoDto> GetAllCargos(string cargoSizeType = "cm", string cargoWeightType = "kg")
        {
            IsCargoTypesValid(cargoSizeType, cargoWeightType);

            var cargos = new List<Cargo>();
            try
            {
                cargos = _cargoRepository.GetAllItems();

                var cargoDtos = new List<CargoDto>();
                foreach (var item in cargos)
                {
                    cargoDtos.Add(CreateCargoDtoFromCargo(item, cargoSizeType, cargoWeightType));
                }
                return cargoDtos;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return cargos;
        }

        public void UpdateCargo(CargoDto cargoDto)
        {
            try
            {
                IsCargoValid(cargoDto);

                Cargo cargo = new Cargo();
                cargo.CargoId = cargoDto.CargoId;
                cargo.Weight = ConvertCargoWeightToKg(cargoDto.Weight, GetCargoWeightType(cargoDto.WeightMeasureUnit));
                cargo.Length = ConvertCargoSizeToCm(cargoDto.Length, GetCargoSizeType(cargoDto.SizeMeasureUnit));
                cargo.Width = ConvertCargoSizeToCm(cargoDto.Width, GetCargoSizeType(cargoDto.SizeMeasureUnit));
                cargo.Height = ConvertCargoSizeToCm(cargoDto.Height, GetCargoSizeType(cargoDto.SizeMeasureUnit));
                cargo.Description = cargoDto.Description;
                _cargoRepository.UpdateItem(cargo);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}

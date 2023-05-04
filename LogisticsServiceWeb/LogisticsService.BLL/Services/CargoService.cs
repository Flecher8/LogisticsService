using LogisticsService.BLL.Interfaces;
using LogisticsService.BLL.Services.Converters;
using LogisticsService.BLL.Services.Validators;
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

        public Cargo CreateCargo(CargoDto cargoDto)
        {
            CargoValidator cargoValidator = new CargoValidator();
            cargoValidator.IsCargoValid(cargoDto);

            CargoConverter cargoConverter = 
                new CargoConverter(cargoDto.WeightMeasureUnit, cargoDto.SizeMeasureUnit);

            Cargo cargo = new Cargo();
            cargo.Name = cargoDto.Name;
            cargo.Weight = cargoConverter.ConvertCargoWeightToKg(cargoDto.Weight);
            cargo.Length = cargoConverter.ConvertCargoSizeToCm(cargoDto.Length);
            cargo.Width = cargoConverter.ConvertCargoSizeToCm(cargoDto.Width);
            cargo.Height = cargoConverter.ConvertCargoSizeToCm(cargoDto.Height);
            cargo.Description = cargoDto.Description;

            try
            {
                _cargoRepository.InsertItem(cargo);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return cargo;
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

        public Cargo? GetCargoById(int cargoId, string cargoSizeType = "cm", string cargoWeightType = "kg")
        {
            CargoValidator cargoValidator = new CargoValidator();
            cargoValidator.IsCargoTypesValid(cargoSizeType, cargoWeightType);

            try
            {
                Cargo? cargo = _cargoRepository.GetItemById(cargoId);
                if(cargo == null)
                {
                    return null;
                }
                //return cargo;
                CargoDto dto = CreateCargoDtoFromCargo(cargo, cargoSizeType, cargoWeightType);
                Cargo newCargo = new Cargo();
                newCargo.CargoId = cargoId;
                newCargo.Name = dto.Name;
                newCargo.Weight = dto.Weight;
                newCargo.Length = dto.Length;
                newCargo.Width = dto.Width;
                newCargo.Height = dto.Height;
                newCargo.Description = dto.Description;
                return newCargo;

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        private CargoDto CreateCargoDtoFromCargo(Cargo cargo, string cargoSizeType, string cargoWeightType)
        {
            CargoConverter cargoConverter = new CargoConverter(cargoWeightType, cargoSizeType);
            CargoSizeType sizeType = cargoConverter.GetCargoSizeType();
            CargoWeightType weightType = cargoConverter.GetCargoWeightType();

            CargoDto cargoDto = new CargoDto();
            cargoDto.Name = cargo.Name;
            cargoDto.CargoId = cargo.CargoId;
            cargoDto.Description = cargo.Description;
            cargoDto.WeightMeasureUnit = weightType.ToString();
            cargoDto.SizeMeasureUnit = sizeType.ToString();

            if (!sizeType.Equals(CargoSizeType.cm))
            {
                cargoDto.Length = SizeConversion.CentimetersToInches(cargo.Length);
                cargoDto.Width = SizeConversion.CentimetersToInches(cargo.Width);
                cargoDto.Height = SizeConversion.CentimetersToInches(cargo.Height);
            }
            else
            {
                cargoDto.Length = cargo.Length;
                cargoDto.Width = cargo.Width;
                cargoDto.Height = cargo.Height;
            }

            if (!weightType.Equals(CargoWeightType.kg))
            {
                cargoDto.Weight = WeightConverter.KilogramsToPounds(cargo.Weight);
            }
            else
            {
                cargoDto.Weight = cargo.Weight;
            }

            return cargoDto;
        }

        public List<CargoDto> GetAllCargos(string cargoSizeType = "cm", string cargoWeightType = "kg")
        {
            CargoValidator cargoValidator = new CargoValidator();
            cargoValidator.IsCargoTypesValid(cargoSizeType, cargoWeightType);

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

            return new List<CargoDto>();
        }

        public Cargo UpdateCargo(CargoDto cargoDto)
        {
            CargoValidator cargoValidator = new CargoValidator();
            cargoValidator.IsCargoValid(cargoDto);

            CargoConverter cargoConverter =
                new CargoConverter(cargoDto.WeightMeasureUnit, cargoDto.SizeMeasureUnit);

            Cargo cargo = new Cargo();
            cargo.CargoId = cargoDto.CargoId;
            cargo.Name = cargoDto.Name;
            cargo.Weight = cargoConverter.ConvertCargoWeightToKg(cargoDto.Weight);
            cargo.Length = cargoConverter.ConvertCargoSizeToCm(cargoDto.Length);
            cargo.Width = cargoConverter.ConvertCargoSizeToCm(cargoDto.Width);
            cargo.Height = cargoConverter.ConvertCargoSizeToCm(cargoDto.Height);
            cargo.Description = cargoDto.Description;

            try
            {
                _cargoRepository.UpdateItem(cargo);
                
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return cargo;
        }
    }
}

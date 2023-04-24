using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Interfaces
{
    public interface ICargoService
    {
        Cargo? GetCargoById(int cargoId, string cargoSizeType = "cm", string cargoWeightType = "kg");

        List<CargoDto> GetAllCargos(string cargoSizeType, string cargoWeightType);

        Cargo CreateCargo(CargoDto cargo);

        Cargo UpdateCargo(CargoDto cargoDto);

        void DeleteCargo(int cargoId);
    }
}

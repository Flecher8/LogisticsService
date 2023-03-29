using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Interfaces
{
    public interface ILogisticCompaniesDriverService
    {
        LogisticCompaniesDriver? GetLogisticCompaniesDriverById(int logisticCompaniesDriverId);

        List<LogisticCompaniesDriver> GetAllLogisticCompaniesDrivers();

        List<LogisticCompaniesDriver> GetAllLogisticCompaniesDriversByLogisticCompanyId(int logisticCompanyId);

        void CreateLogisticCompaniesDriver(PersonDto person);

        void UpdateLogisticCompaniesDriver(LogisticCompaniesDriver logisticCompaniesDriver);

        void DeleteLogisticCompaniesDriver(int logisticCompaniesDriverId);
    }
}

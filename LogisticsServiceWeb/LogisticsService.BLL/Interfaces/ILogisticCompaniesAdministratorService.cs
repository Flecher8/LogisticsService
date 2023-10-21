using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Interfaces
{
    public interface ILogisticCompaniesAdministratorService
    {
        LogisticCompaniesAdministrator? GetLogisticCompaniesAdministratorById(int logisticCompaniesAdministratorId);

        List<LogisticCompaniesAdministrator> GetAllLogisticCompaniesAdministrators();

        List<LogisticCompaniesAdministrator> GetAllLogisticCompaniesAdministratorsByLogisticCompanyId(int logisticCompanyId);

        LogisticCompaniesAdministrator CreateLogisticCompaniesAdministrator(PersonDto person);

        LogisticCompaniesAdministrator UpdateLogisticCompaniesAdministrator(PersonDto person);

        void DeleteLogisticCompaniesAdministrator(int logisticCompaniesAdministratorId);

        LogisticCompaniesAdministrator? GetLogisticCompaniesAdministratorByEmail(string email);
    }
}

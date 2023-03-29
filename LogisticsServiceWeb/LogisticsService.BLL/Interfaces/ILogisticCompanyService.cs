using LogisticsService.Core.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Interfaces
{
    public interface ILogisticCompanyService
    {
        LogisticCompany? GetLogisticCompanyById(int logisticCompanyId);

        List<LogisticCompany> GetAllLogisticCompanies();

        LogisticCompany? GetLogisticCompanyByEmail(string email);

        void CreateLogisticCompany(LogisticCompany logisticCompany);

        void UpdateLogisticCompany(LogisticCompany logisticCompany);
    }
}

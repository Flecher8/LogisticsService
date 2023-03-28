using LogisticsService.Core.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Interfaces
{
    public interface IPrivateCompanyService
    {
        PrivateCompany? GetPrivateCompanyById(int privateCompanyId);

        List<PrivateCompany> GetAllPrivateCompanies();

        void CreatePrivateCompany(PrivateCompany privateCompany);

        void UpdatePrivateCompany(PrivateCompany privateCompany);
    }
}

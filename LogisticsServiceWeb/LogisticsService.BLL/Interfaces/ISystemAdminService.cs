using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Interfaces
{
    public interface ISystemAdminService
    {
        SystemAdmin? GetSystemAdminById(int systemAdminId);

        List<SystemAdmin> GetAllSystemAdmins();

        void CreateSystemAdmin(PersonDto person);

        void UpdateSystemAdmin(SystemAdmin systemAdmin);

        void DeleteSystemAdmin(int systemAdminId);
    }
}

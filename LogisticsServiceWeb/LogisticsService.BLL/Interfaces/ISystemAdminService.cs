using LogisticsService.Core.DbModels;
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

        void CreateSystemAdmin(SystemAdmin systemAdmin);

        void UpdateSystemAdmin(SystemAdmin systemAdmin);

        void DeleteSystemAdmin(int systemAdminId);
    }
}

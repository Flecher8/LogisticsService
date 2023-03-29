using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.DbModels;
using LogisticsService.DAL.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Services
{
    public class SystemAdminService : ISystemAdminService
    {
        private readonly IDataRepository<SystemAdmin> _systemAdminRepository;
        private readonly ILogger<SystemAdmin> _logger;
        private const int minSystemAdminId = 1;

        public void CreateSystemAdmin(SystemAdmin systemAdmin)
        {
            throw new NotImplementedException();
        }

        public void DeleteSystemAdmin(int systemAdminId)
        {
            throw new NotImplementedException();
        }

        public List<SystemAdmin> GetAllSystemAdmins()
        {
            throw new NotImplementedException();
        }

        public SystemAdmin? GetSystemAdminById(int systemAdminId)
        {
            throw new NotImplementedException();
        }

        public void UpdateSystemAdmin(SystemAdmin systemAdmin)
        {
            throw new NotImplementedException();
        }
    }
}

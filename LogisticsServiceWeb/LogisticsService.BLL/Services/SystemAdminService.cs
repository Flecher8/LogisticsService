using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.DbModels;
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
    public class SystemAdminService : ISystemAdminService
    {
        private readonly IDataRepository<SystemAdmin> _systemAdminRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IUserService _userService;
        private readonly ILogger<SystemAdminService> _logger;

        public SystemAdminService(IDataRepository<SystemAdmin> systemAdminRepository, 
            IUserService userService,
            IEncryptionService encryptionService,
            ILogger<SystemAdminService> logger)
        {
            _systemAdminRepository = systemAdminRepository;
            _userService = userService;
            _encryptionService = encryptionService;
            _logger = logger;
        }

        public void CreateSystemAdmin(SystemAdmin systemAdmin)
        {
            if(_userService.IsEmailAlreadyRegistered(systemAdmin.Email))
            {
                throw new ArgumentException("User with such email already registered.");
            }
            systemAdmin.HashedPassword = _encryptionService.HashPassword(systemAdmin.HashedPassword);

            try
            {
                _systemAdminRepository.InsertItem(systemAdmin);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public void DeleteSystemAdmin(int systemAdminId)
        {
            try
            {
                _systemAdminRepository.DeleteItem(systemAdminId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public List<SystemAdmin> GetAllSystemAdmins()
        {
            var systemAdmins = new List<SystemAdmin>();
            try
            {
                systemAdmins = _systemAdminRepository.GetAllItems();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return systemAdmins;
        }

        public SystemAdmin? GetSystemAdminById(int systemAdminId)
        {
            try
            {
                SystemAdmin? systemAdmin = _systemAdminRepository.GetItemById(systemAdminId);
                return systemAdmin;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        public void UpdateSystemAdmin(SystemAdmin systemAdmin)
        {
            try
            {
                _systemAdminRepository.UpdateItem(systemAdmin);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}

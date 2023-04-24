using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.DbModels;
using LogisticsService.Core.Enums;
using LogisticsService.DAL.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IDataRepository<SystemAdmin> _systemAdminRepository;
        private readonly IDataRepository<PrivateCompany> _privateCompanyRepository;
        private readonly IDataRepository<LogisticCompany> _logisticCompanyRepository;
        private readonly IDataRepository<LogisticCompaniesAdministrator> _logisticCompaniesAdministratorRepository;
        private readonly IDataRepository<LogisticCompaniesDriver> _logisticCompaniesDriverRepository;



        private readonly ILogger<UserService> _logger;

        public UserService(IDataRepository<SystemAdmin> systemAdminRepository, 
            IDataRepository<PrivateCompany> privateCompanyRepository, 
            IDataRepository<LogisticCompany> logisticCompanyRepository, 
            IDataRepository<LogisticCompaniesAdministrator> logisticCompaniesAdministratorRepository, 
            IDataRepository<LogisticCompaniesDriver> logisticCompaniesDriverRepository,
            ILogger<UserService> logger)
        {
            _systemAdminRepository = systemAdminRepository;
            _privateCompanyRepository = privateCompanyRepository;
            _logisticCompanyRepository = logisticCompanyRepository;
            _logisticCompaniesAdministratorRepository = logisticCompaniesAdministratorRepository;
            _logisticCompaniesDriverRepository = logisticCompaniesDriverRepository;
            _logger = logger;
        }

        public UserType GetUserTypeByEmail(string email)
        {
            if (_systemAdminRepository.GetFilteredItems(s => s.Email == email).Any())
                return UserType.SystemAdmin;

            if (_privateCompanyRepository.GetFilteredItems(c => c.Email == email).Any())
                return UserType.PrivateCompany;

            if (_logisticCompanyRepository.GetFilteredItems(c => c.Email == email).Any())
                return UserType.LogisticCompany;

            if (_logisticCompaniesAdministratorRepository.GetFilteredItems(a => a.Email == email).Any())
                return UserType.LogisticCompanyAdministrator;

            if (_logisticCompaniesDriverRepository.GetFilteredItems(d => d.Email == email).Any())
                return UserType.LogisticCompanyDriver;

            // Guest
            return default(UserType);
        }

        public bool IsEmailAlreadyRegistered(string email)
        {
            bool isRegisteredAsSystemAdmin = _systemAdminRepository.GetFilteredItems(s => s.Email == email).Any();

            bool isRegisteredAsPrivateCompany = _privateCompanyRepository.GetFilteredItems(s => s.Email == email).Any();
            
            bool isRegisteredAsLogisticCompany = _logisticCompanyRepository.GetFilteredItems(s => s.Email == email).Any();

            bool isRegisteredAsLogisticCompanyAdmin = _logisticCompaniesAdministratorRepository.GetFilteredItems(s => s.Email == email).Any();

            bool isRegisteredAsLogisticCompanyDriver = _logisticCompaniesDriverRepository.GetFilteredItems(s => s.Email == email).Any();

            return isRegisteredAsSystemAdmin || 
                isRegisteredAsPrivateCompany ||
                isRegisteredAsLogisticCompany ||
                isRegisteredAsLogisticCompanyAdmin || 
                isRegisteredAsLogisticCompanyDriver;
        }

        public string GetUserHashedPassword(string email, UserType userType)
        {
            string result = "";

            switch(userType)
            {
                case UserType.SystemAdmin:
                    result = _systemAdminRepository.GetFilteredItems(s => s.Email == email).FirstOrDefault().HashedPassword;
                    return result is not null ? result : string.Empty;
                case UserType.PrivateCompany:
                    result = _privateCompanyRepository.GetFilteredItems(s => s.Email == email).FirstOrDefault().HashedPassword;
                    return result is not null ? result : string.Empty;
                case UserType.LogisticCompany:
                    result = _logisticCompanyRepository.GetFilteredItems(s => s.Email == email).FirstOrDefault().HashedPassword;
                    return result is not null ? result : string.Empty;
                case UserType.LogisticCompanyAdministrator:
                    result = _logisticCompaniesAdministratorRepository.GetFilteredItems(s => s.Email == email).FirstOrDefault().HashedPassword;
                    return result is not null ? result : string.Empty;
                case UserType.LogisticCompanyDriver:
                    result = _logisticCompaniesDriverRepository.GetFilteredItems(s => s.Email == email).FirstOrDefault().HashedPassword;
                    return result is not null ? result : string.Empty;
                default:
                    return string.Empty;
            }
        }

    }
}

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
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly IEncryptionService _encryptionService;
        private readonly IUserService _userService;
        private readonly IPrivateCompanyService _privateCompanyService;
        private readonly ILogisticCompanyService _logisticCompanyService;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(ITokenService tokenService,
            IEncryptionService encryptionService,
            IUserService userService, 
            IPrivateCompanyService privateCompanyService,
            ILogisticCompanyService logisticCompanyService,
            ILogger<AuthenticationService> logger)
        {
            _tokenService = tokenService;
            _encryptionService = encryptionService;
            _userService = userService;
            _privateCompanyService = privateCompanyService; 
            _logisticCompanyService = logisticCompanyService;
            _logger = logger;
        }

        public string Login(string email, string password)
        {
            if(!_userService.IsEmailAlreadyRegistered(email))
            {
                throw new ArgumentException("User with this email does not exist.");
            }

        }

        public bool Registration(string email, string password, string companyName, string userType)
        {
            if(_userService.IsEmailAlreadyRegistered(email))
            {
                throw new ArgumentException("User with such email already registered.");
            }

            if(!IsUserTypeValid(userType))
            {
                throw new ArgumentException("User type is not valid.");
            }

            UserType type = GetUserType(userType);

            if(type.Equals(UserType.Guest) || 
                type.Equals(UserType.SystemAdmin) || 
                type.Equals(UserType.LogisticCompanyAdministrator) || 
                type.Equals(UserType.LogisticCompanyDriver))
            {
                throw new ArgumentException("You do not have access to register this type of user.");
            }

            if(type == UserType.PrivateCompany)
            {
                PrivateCompanyRegistration(email, password, companyName);
                return true;
            }

            if (type == UserType.LogisticCompany)
            {
                LogisticCompanyRegistration(email, password, companyName);
                return true;
            }

            return false;
        }

        private void PrivateCompanyRegistration(string email, string password, string companyName)
        {
            PrivateCompany privateCompany = new PrivateCompany();
            privateCompany.CompanyName = companyName;
            privateCompany.Email = email;
            privateCompany.HashedPassword = _encryptionService.HashPassword(password);
            try
            {
                _privateCompanyService.CreatePrivateCompany(privateCompany);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        private void LogisticCompanyRegistration(string email, string password, string companyName)
        {
            LogisticCompany logisticCompany = new LogisticCompany();
            logisticCompany.CompanyName = companyName;
            logisticCompany.Email = email;
            logisticCompany.HashedPassword = _encryptionService.HashPassword(password);
            try
            {
                _logisticCompanyService.CreateLogisticCompany(logisticCompany);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        private bool IsUserTypeValid(string userType)
        {
            return Enum.IsDefined(typeof(UserType), userType);
        }

        private UserType GetUserType(string userType)
        {
            return (UserType)Enum.Parse(typeof(UserType), userType);
        }
    }
}

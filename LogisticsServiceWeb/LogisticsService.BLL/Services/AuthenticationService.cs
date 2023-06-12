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
        // TODO Create UserDto class ( email, password, ...), instead of 2,3,4 parameters
        public string Login(string email, string password)
        {
            if(!CanUserLogin(email))
            {
                return string.Empty;
            }
            
            UserType userType = _userService.GetUserTypeByEmail(email);


            string dbHashedPassword = string.Empty;

            try
            {
                dbHashedPassword = _userService.GetUserHashedPassword(email, userType);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            

            if (!_encryptionService.VerifyPassword(password, dbHashedPassword))
            {
                throw new ArgumentException("Password is not correct.");
            }

            string token = _tokenService.CreateToken(email, userType);

            return token;

        }

        private bool CanUserLogin(string email)
        {
            if (!_userService.IsEmailAlreadyRegistered(email))
            {
                throw new ArgumentException("User with this email does not exist.");
            }

            return true;
        }

        // TODO Create UserDto class ( email, password, ...), instead of 2,3,4 parameters
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

            if(!IsUserTypeCanRegister(type))
            {
                throw new ArgumentException("You do not have access to register this type of user");
            }

            if(type == UserType.PrivateCompany)
            {
                var privateCompany = CreatePrivateCompany(email, password, companyName);
                PrivateCompanyRegistration(privateCompany);
                return true;
            }

            if (type == UserType.LogisticCompany)
            {
                var logisticCompany = CreateLogisticCompany(email, password, companyName);
                LogisticCompanyRegistration(logisticCompany);
                return true;
            }

            return false;
        }

        private bool IsUserTypeCanRegister(UserType userType)
        {
            if (userType.Equals(UserType.Guest) ||
                userType.Equals(UserType.SystemAdmin) ||
                userType.Equals(UserType.LogisticCompanyAdministrator) ||
                userType.Equals(UserType.LogisticCompanyDriver))
            {
                return false;
            }
            return true;
        }

        private void PrivateCompanyRegistration(PrivateCompany privateCompany)
        {
            try
            {
                _privateCompanyService.CreatePrivateCompany(privateCompany);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        private void LogisticCompanyRegistration(LogisticCompany logisticCompany)
        {
            try
            {
                _logisticCompanyService.CreateLogisticCompany(logisticCompany);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        // TODO Move methods CreatePrivateCompany and CreateLogisticCompany in another class
        private PrivateCompany CreatePrivateCompany(string email, string password, string companyName)
        {
            PrivateCompany privateCompany = new PrivateCompany();
            privateCompany.CompanyName = companyName;
            privateCompany.Email = email;
            privateCompany.HashedPassword = _encryptionService.HashPassword(password);
            return privateCompany;
        }

        private LogisticCompany CreateLogisticCompany(string email, string password, string companyName)
        {
            LogisticCompany logisticCompany = new LogisticCompany();
            logisticCompany.CompanyName = companyName;
            logisticCompany.Email = email;
            logisticCompany.HashedPassword = _encryptionService.HashPassword(password);
            return logisticCompany;
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

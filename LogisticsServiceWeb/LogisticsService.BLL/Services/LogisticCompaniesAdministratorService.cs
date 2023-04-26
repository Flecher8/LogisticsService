using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
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
    public class LogisticCompaniesAdministratorService : ILogisticCompaniesAdministratorService
    {
        private readonly IDataRepository<LogisticCompaniesAdministrator> _logisticCompaniesAdministratorRepository;
        private readonly ILogisticCompanyService _logisticCompanyService;
        private readonly IEncryptionService _encryptionService;
        private readonly IUserService _userService;

        private readonly ILogger<LogisticCompaniesAdministratorService> _logger;

        public LogisticCompaniesAdministratorService(
            IDataRepository<LogisticCompaniesAdministrator> logisticCompaniesAdministratorRepository,
            ILogisticCompanyService logisticCompanyService,
            IEncryptionService encryptionService, IUserService userService, 
            ILogger<LogisticCompaniesAdministratorService> logger)
        {
            _logisticCompaniesAdministratorRepository = logisticCompaniesAdministratorRepository;
            _logisticCompanyService = logisticCompanyService;
            _encryptionService = encryptionService;
            _userService = userService;
            _logger = logger;
        }

        public LogisticCompaniesAdministrator CreateLogisticCompaniesAdministrator(PersonDto person)
        {
            LogisticCompaniesAdministrator logisticCompaniesAdministrator = new LogisticCompaniesAdministrator();
            logisticCompaniesAdministrator.FirstName = person.FirstName;
            logisticCompaniesAdministrator.LastName = person.LastName;
            logisticCompaniesAdministrator.Email = person.Email;
            logisticCompaniesAdministrator.HashedPassword = _encryptionService.HashPassword(person.Password);
            logisticCompaniesAdministrator.LogisticCompany = _logisticCompanyService.GetLogisticCompanyById(person.LogisticCompanyId);

            IsLogisticCompaniesAdministratorValid(logisticCompaniesAdministrator);

            try
            {
                _logisticCompaniesAdministratorRepository.InsertItem(logisticCompaniesAdministrator);
                return logisticCompaniesAdministrator;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        private bool IsLogisticCompaniesAdministratorValid(LogisticCompaniesAdministrator logisticCompaniesAdministrator)
        {
            if (_userService.IsEmailAlreadyRegistered(logisticCompaniesAdministrator.Email))
            {
                throw new ArgumentException("User with such email already registered.");
            }
            return true;
        }

        public void DeleteLogisticCompaniesAdministrator(int logisticCompaniesAdministratorId)
        {
            try
            {
                _logisticCompaniesAdministratorRepository.DeleteItem(logisticCompaniesAdministratorId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public List<LogisticCompaniesAdministrator> GetAllLogisticCompaniesAdministrators()
        {
            var logisticCompaniesAdministrator = new List<LogisticCompaniesAdministrator>();
            try
            {
                logisticCompaniesAdministrator = _logisticCompaniesAdministratorRepository.GetAllItems();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return logisticCompaniesAdministrator;
        }

        public List<LogisticCompaniesAdministrator> GetAllLogisticCompaniesAdministratorsByLogisticCompanyId(int logisticCompanyId)
        {
            var logisticCompaniesAdministrators = new List<LogisticCompaniesAdministrator>();
            try
            {
                logisticCompaniesAdministrators = _logisticCompaniesAdministratorRepository
                    .GetFilteredItems(l => l.LogisticCompany.LogisticCompanyId == logisticCompanyId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return logisticCompaniesAdministrators;
        }

        public LogisticCompaniesAdministrator? GetLogisticCompaniesAdministratorById(int logisticCompaniesAdministratorId)
        {
            try
            {
                LogisticCompaniesAdministrator? logisticCompaniesAdministrator = 
                    _logisticCompaniesAdministratorRepository.GetItemById(logisticCompaniesAdministratorId);
                return logisticCompaniesAdministrator;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        public LogisticCompaniesAdministrator UpdateLogisticCompaniesAdministrator(PersonDto person)
        {
            LogisticCompaniesAdministrator logisticCompaniesAdministrator = new LogisticCompaniesAdministrator();
            logisticCompaniesAdministrator.LogisticCompaniesAdministratorId = person.Id;
            logisticCompaniesAdministrator.FirstName = person.FirstName;
            logisticCompaniesAdministrator.LastName = person.LastName;
            logisticCompaniesAdministrator.Email = person.Email;
            logisticCompaniesAdministrator.HashedPassword = _encryptionService.HashPassword(person.Password);

            LogisticCompaniesAdministrator dbLogisticCompaniesAdministrator = 
                GetLogisticCompaniesAdministratorById(logisticCompaniesAdministrator.LogisticCompaniesAdministratorId);

            if(dbLogisticCompaniesAdministrator != null && dbLogisticCompaniesAdministrator.Email != person.Email)
            {
                throw new ArgumentException
                    ("Email changed, you can't change email. " +
                    "You need to delete admin to change email");
            }

            
            try
            {
                _logisticCompaniesAdministratorRepository.UpdateItem(logisticCompaniesAdministrator);
                return logisticCompaniesAdministrator;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        public LogisticCompaniesAdministrator? GetLogisticCompaniesAdministratorByEmail(string email)
        {
            try
            {
                LogisticCompaniesAdministrator? logisticCompaniesAdministrator = 
                    _logisticCompaniesAdministratorRepository
                    .GetFilteredItems(l => l.Email == email)
                    .FirstOrDefault();
                return logisticCompaniesAdministrator;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }
    }
}

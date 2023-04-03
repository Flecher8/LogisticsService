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
    public class LogisticCompaniesDriverService : ILogisticCompaniesDriverService
    {
        private readonly IDataRepository<LogisticCompaniesDriver> _logisticCompaniesDriverRepository;
        private readonly ILogisticCompanyService _logisticCompanyService;
        private readonly IEncryptionService _encryptionService;
        private readonly IUserService _userService;

        private readonly ILogger<LogisticCompaniesDriverService> _logger;

        public LogisticCompaniesDriverService(
            IDataRepository<LogisticCompaniesDriver> logisticCompaniesDriverRepository, 
            ILogisticCompanyService logisticCompanyService, 
            IEncryptionService encryptionService, 
            IUserService userService, 
            ILogger<LogisticCompaniesDriverService> logger)
        {
            _logisticCompaniesDriverRepository = logisticCompaniesDriverRepository;
            _logisticCompanyService = logisticCompanyService;
            _encryptionService = encryptionService;
            _userService = userService;
            _logger = logger;
        }

        public void CreateLogisticCompaniesDriver(PersonDto person)
        {
            LogisticCompaniesDriver logisticCompaniesDriver = new LogisticCompaniesDriver();
            logisticCompaniesDriver.FirstName = person.FirstName;
            logisticCompaniesDriver.LastName = person.LastName;
            logisticCompaniesDriver.Email = person.Email;
            logisticCompaniesDriver.HashedPassword = _encryptionService.HashPassword(person.Password);
            logisticCompaniesDriver.LogisticCompany = _logisticCompanyService.GetLogisticCompanyById(person.LogisticCompanyId);

            IsLogisticCompaniesDriverValid(logisticCompaniesDriver);

            try
            {
                _logisticCompaniesDriverRepository.InsertItem(logisticCompaniesDriver);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        private bool IsLogisticCompaniesDriverValid(LogisticCompaniesDriver logisticCompaniesDriver)
        {
            if (_userService.IsEmailAlreadyRegistered(logisticCompaniesDriver.Email))
            {
                throw new ArgumentException("User with such email already registered.");
            }
            return true;
        }

        public void DeleteLogisticCompaniesDriver(int logisticCompaniesDriverId)
        {
            try
            {
                _logisticCompaniesDriverRepository.DeleteItem(logisticCompaniesDriverId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public List<LogisticCompaniesDriver> GetAllLogisticCompaniesDrivers()
        {
            var logisticCompaniesDriver = new List<LogisticCompaniesDriver>();
            try
            {
                logisticCompaniesDriver = _logisticCompaniesDriverRepository.GetAllItems();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return logisticCompaniesDriver;
        }

        public List<LogisticCompaniesDriver> GetAllLogisticCompaniesDriversByLogisticCompanyId(int logisticCompanyId)
        {
            var logisticCompaniesDrivers = new List<LogisticCompaniesDriver>();
           
            try
            {
                logisticCompaniesDrivers = _logisticCompaniesDriverRepository
                    .GetFilteredItems(l => l.LogisticCompany.LogisticCompanyId == logisticCompanyId);
                
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return logisticCompaniesDrivers;
        }

        public LogisticCompaniesDriver? GetLogisticCompaniesDriverById(int logisticCompaniesDriverId)
        {
            try
            {
                LogisticCompaniesDriver? logisticCompaniesDriver =
                    _logisticCompaniesDriverRepository.GetItemById(logisticCompaniesDriverId);
                return logisticCompaniesDriver;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        public void UpdateLogisticCompaniesDriver(LogisticCompaniesDriver logisticCompaniesDriver)
        {
            try
            {
                _logisticCompaniesDriverRepository.UpdateItem(logisticCompaniesDriver);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}

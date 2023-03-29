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
    public class LogisticCompanyService : ILogisticCompanyService
    {
        private readonly IDataRepository<LogisticCompany> _logisticCompanyRepository;
        private readonly IRateService _rateService;

        private int DefaultRate = 5;

        private readonly ILogger<LogisticCompanyService> _logger;

        public LogisticCompanyService(IDataRepository<LogisticCompany> logisticCompanyRepository,
            IRateService rateService,
            ILogger<LogisticCompanyService> logger)
        {
            _logisticCompanyRepository = logisticCompanyRepository;
            _rateService = rateService;
            _logger = logger;
        }

        public void CreateLogisticCompany(LogisticCompany logisticCompany)
        {
            try
            {
                _logisticCompanyRepository.InsertItem(logisticCompany);

                Rate newRate = new Rate();
                newRate.LogisticCompany = logisticCompany;
                newRate.PriceForKmInDollar = DefaultRate;

                _rateService.CreateRate(newRate);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public List<LogisticCompany> GetAllLogisticCompanies()
        {
            var logisticCompanies = new List<LogisticCompany>();
            try
            {
                logisticCompanies = _logisticCompanyRepository.GetAllItems();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return logisticCompanies;
        }
        
        public LogisticCompany? GetLogisticCompanyById(int logisticCompanyId)
        {
            try
            {
                LogisticCompany? logisticCompany = _logisticCompanyRepository.GetItemById(logisticCompanyId);
                return logisticCompany;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        public void UpdateLogisticCompany(LogisticCompany logisticCompany)
        {
            try
            {
                _logisticCompanyRepository.UpdateItem(logisticCompany);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public LogisticCompany? GetLogisticCompanyByEmail(string email)
        {
            try
            {
                LogisticCompany? logisticCompany = _logisticCompanyRepository.GetFilteredItems(l => l.Email == email).FirstOrDefault();
                return logisticCompany;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }
    }
}

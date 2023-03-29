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

        private readonly ILogger<LogisticCompanyService> _logger;

        public LogisticCompanyService(IDataRepository<LogisticCompany> logisticCompanyRepository,
            ILogger<LogisticCompanyService> logger)
        {
            _logisticCompanyRepository = logisticCompanyRepository;
            _logger = logger;
        }

        public void CreateLogisticCompany(LogisticCompany logisticCompany)
        {
            try
            {
                _logisticCompanyRepository.InsertItem(logisticCompany);
                // TODO When creating logistic company, you need to create a rate in for this company ( table Rate )
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
        // TODO
        public List<LogisticCompany> GetAllLogisticCompanies()
        {
            throw new NotImplementedException();
        }
        // TODO
        public LogisticCompany? GetLogisticCompanyById(int logisticCompanyId)
        {
            throw new NotImplementedException();
        }
        // TODO
        public void UpdateLogisticCompany(LogisticCompany logisticCompany)
        {
            throw new NotImplementedException();
        }
    }
}

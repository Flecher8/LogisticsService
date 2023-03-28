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
    public class PrivateCompanyService : IPrivateCompanyService
    {
        private readonly IDataRepository<PrivateCompany> _privateCompanyRepository;

        private readonly ILogger<PrivateCompanyService> _logger;

        public PrivateCompanyService(IDataRepository<PrivateCompany> privateCompanyRepository, 
            ILogger<PrivateCompanyService> logger)
        {
            _privateCompanyRepository = privateCompanyRepository;
            _logger = logger;
        }

        public void CreatePrivateCompany(PrivateCompany privateCompany)
        {
            try
            {
                _privateCompanyRepository.InsertItem(privateCompany);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
        // TODO
        public List<PrivateCompany> GetAllPrivateCompanies()
        {
            throw new NotImplementedException();
        }
        // TODO
        public PrivateCompany? GetPrivateCompanyById(int privateCompanyId)
        {
            throw new NotImplementedException();
        }
        // TODO
        public void UpdatePrivateCompany(PrivateCompany privateCompany)
        {
            throw new NotImplementedException();
        }
    }
}

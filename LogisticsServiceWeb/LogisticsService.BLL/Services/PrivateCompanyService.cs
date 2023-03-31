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
        
        public List<PrivateCompany> GetAllPrivateCompanies()
        {
            var privateCompanies = new List<PrivateCompany>();
            try
            {
                privateCompanies = _privateCompanyRepository.GetAllItems();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return privateCompanies;
        }

        public PrivateCompany? GetPrivateCompanyByEmail(string email)
        {
            try
            {
                PrivateCompany? privateCompany = _privateCompanyRepository.GetFilteredItems(l => l.Email == email).FirstOrDefault();
                return privateCompany;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        
        public PrivateCompany? GetPrivateCompanyById(int privateCompanyId)
        {
            try
            {
                PrivateCompany? privateCompany = _privateCompanyRepository.GetItemById(privateCompanyId);
                return privateCompany;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }
        
        public void UpdatePrivateCompany(PrivateCompany privateCompany)
        {
            try
            {
                _privateCompanyRepository.UpdateItem(privateCompany);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}

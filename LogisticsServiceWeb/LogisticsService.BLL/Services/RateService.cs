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
    public class RateService : IRateService
    {
        private readonly IDataRepository<Rate> _rateRepository;
        private readonly ILogger<RateService> _logger;

        public RateService(IDataRepository<Rate> rateRepository, ILogger<RateService> logger)
        {
            _rateRepository = rateRepository;
            _logger = logger;
        }

        public void CreateRate(Rate rate)
        {

            try
            {
                _rateRepository.InsertItem(rate);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public Rate? GetRateById(int rateId)
        {
            try
            {
                Rate? rate = _rateRepository.GetItemById(rateId);
                return rate;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        public Rate? GetRateByLogisticCompanyId(int logisticCompanyId)
        {
            try
            {
                Rate? rate = _rateRepository.GetFilteredItems(r => r.LogisticCompany.LogisticCompanyId == logisticCompanyId).FirstOrDefault();
                return rate;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        public void UpdateRate(RateDto rateDto)
        {
            Rate? rate = GetRateById(rateDto.RateId);
            if(rate == null)
            {
                return;
            }

            rate.PriceForKmInDollar = rateDto.PriceForKmInDollar;

            try
            {
                _rateRepository.UpdateItem(rate);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}

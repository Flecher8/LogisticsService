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
    public class SubscriptionTypeService : ISubscriptionTypeService
    {
        private readonly IDataRepository<SubscriptionType> _subscriptionTypeRepository;
        private readonly ILogger<SubscriptionTypeService> _logger;
        private const int minSubscriptionTypeId = 1;

        public SubscriptionTypeService(IDataRepository<SubscriptionType> subscriptionTypeRepository, 
            ILogger<SubscriptionTypeService> logger)
        {
            _subscriptionTypeRepository = subscriptionTypeRepository;
            _logger = logger;
        }

        public SubscriptionType? GetSubscriptionTypeById(int subscriptionTypeId)
        {
            if(!IsSubscriptionTypeIdCorrect(subscriptionTypeId))
            {
                throw new ArgumentException("Incorrect subscription id");
            }

            try
            {
                SubscriptionType? subscriptionType = _subscriptionTypeRepository.GetItemById(subscriptionTypeId);
                return subscriptionType;
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        private bool IsSubscriptionTypeIdCorrect(int subscriptionId)
        {
            return subscriptionId >= minSubscriptionTypeId ? true : false;
        }

        public List<SubscriptionType> GetAllSubscriptionTypes()
        {
            List<SubscriptionType> subscriptionTypes = new List<SubscriptionType>();
            try
            {
                subscriptionTypes = _subscriptionTypeRepository.GetAllItems();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return subscriptionTypes;
        }

        public void CreateSubscriptionType(SubscriptionType subscriptionType)
        {
            throw new NotImplementedException();
        }

        public void UpdateSubscriptionType(SubscriptionType subscriptionType)
        {
            throw new NotImplementedException();
        }

        public void DeleteSubscriptionType(int subscriptionId)
        {
            throw new NotImplementedException();
        }
    }
}

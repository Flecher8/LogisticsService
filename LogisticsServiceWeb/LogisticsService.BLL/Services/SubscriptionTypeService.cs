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
        private readonly IDataRepository<Subscription> _subscriptionRepository;
        private readonly ILogger<SubscriptionTypeService> _logger;
        private const int minSubscriptionTypeId = 1;

        public SubscriptionTypeService(IDataRepository<SubscriptionType> subscriptionTypeRepository, 
            ILogger<SubscriptionTypeService> logger,
            IDataRepository<Subscription> subscriptionRepository)
        {
            _subscriptionTypeRepository = subscriptionTypeRepository;
            _logger = logger;
            _subscriptionRepository = subscriptionRepository;
        }

        public SubscriptionType? GetSubscriptionTypeById(int subscriptionTypeId)
        {
            if(!IsSubscriptionTypeIdValid(subscriptionTypeId))
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

        private bool IsSubscriptionTypeIdValid(int subscriptionId)
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
            try
            {
                _subscriptionTypeRepository.InsertItem(subscriptionType);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public void UpdateSubscriptionType(SubscriptionType subscriptionType)
        {
            try
            {
                _subscriptionTypeRepository.UpdateItem(subscriptionType);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public void DeleteSubscriptionType(int subscriptionTypeId)
        {
            if(!IsSubscriptionTypeIdValid(subscriptionTypeId))
            {
                throw new ArgumentException("Incorrect subscription id");
            }

            
            if(_subscriptionRepository
                .GetFilteredItems(
                s => s.SubscriptionType.SubscriptionTypeId == subscriptionTypeId && 
                s.EndDateTime > DateTime.UtcNow).Count != 0)
            {
                throw new ArgumentException("This subscription type can't be deleted " +
                    "because subscriptions with such subscription type are still active");
            }

            try
            {
                _subscriptionTypeRepository.DeleteItem(subscriptionTypeId);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}

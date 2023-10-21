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
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IDataRepository<Subscription> _subscriptionRepository;
        private readonly ISubscriptionTypeService _subscriptionTypeService;
        private readonly ILogisticCompanyService _logisticCompanyService;
        private readonly ILogger<SubscriptionService> _logger;

        public SubscriptionService(
            IDataRepository<Subscription> subscriptionRepository, 
            ISubscriptionTypeService subscriptionTypeService, 
            ILogisticCompanyService logisticCompanyService, 
            ILogger<SubscriptionService> logger)
        {
            _subscriptionRepository = subscriptionRepository;
            _subscriptionTypeService = subscriptionTypeService;
            _logisticCompanyService = logisticCompanyService;
            _logger = logger;
        }

        public List<Subscription> GetAllSubscriptions()
        {
            var subscriptions = new List<Subscription>();
            try
            {
                subscriptions = _subscriptionRepository.GetAllItems();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return subscriptions;
        }

        public Subscription? GetSubscriptionById(int subscriptionId)
        {
            try
            {
                Subscription? subscription = _subscriptionRepository.GetItemById(subscriptionId);
                return subscription;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        public Subscription? GetSubscriptionByLogisticCompanyId(int logisticCompanyId)
        {
            try
            {
                Subscription? subscription = _subscriptionRepository
                    .GetFilteredItems(s => s.LogisticCompany.LogisticCompanyId == logisticCompanyId)
                    .FirstOrDefault();
                return subscription;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        public bool HasSubscription(int logisticCompanyId)
        {
            Subscription? subscription = GetSubscriptionByLogisticCompanyId(logisticCompanyId);
            if(subscription != null && IsSubscriptionActive(subscription))
            {
                return true;
            }
            return false;
        }

        private bool IsSubscriptionActive(Subscription subscription)
        {
            return subscription.EndDateTime > DateTime.UtcNow;
        }

        public Subscription UpdateSubscription(SubscriptionDto subscriptionDto)
        {
            if(HasSubscription(subscriptionDto.LogisticCompanyId))
            {
                throw new ArgumentException("This logistic company has already an active subscription");
            }

            IsSubscriptionValid(subscriptionDto);

            Subscription? subscription = GetSubscriptionByLogisticCompanyId(subscriptionDto.LogisticCompanyId);
            if( subscription == null )
            {
                subscription = CreateSubscription(subscriptionDto);
                return subscription;
            }

            subscription.StartDateTime = DateTime.UtcNow;
            subscription.EndDateTime = DateTime.UtcNow.AddDays(_subscriptionTypeService
                .GetSubscriptionTypeById(subscriptionDto.SubscriptionTypeId).DurationInDays);

            try
            {
                _subscriptionRepository.UpdateItem(subscription);
                return subscription;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        private Subscription CreateSubscription(SubscriptionDto subscriptionDto)
        {
            IsSubscriptionValid(subscriptionDto);

            Subscription subscription = new Subscription();
            subscription.SubscriptionType = _subscriptionTypeService
                .GetSubscriptionTypeById(subscriptionDto.SubscriptionTypeId);

            subscription.LogisticCompany = _logisticCompanyService
                .GetLogisticCompanyById(subscriptionDto.LogisticCompanyId);

            subscription.StartDateTime = DateTime.UtcNow;
            subscription.EndDateTime = DateTime.UtcNow.AddDays(subscription.SubscriptionType.DurationInDays);

            try
            {
                _subscriptionRepository.InsertItem(subscription);
                return subscription;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        private bool IsSubscriptionValid(SubscriptionDto subscriptionDto)
        {
            if(IsSubscriptionTypeValid(subscriptionDto.SubscriptionTypeId) && 
                IsLogisticCompanyValid(subscriptionDto.LogisticCompanyId))
            {
                return true;
            }
            return false;
        }

        private bool IsSubscriptionTypeValid(int subscriptionTypeId)
        {
            if(_subscriptionTypeService.GetSubscriptionTypeById(subscriptionTypeId) == null)
            {
                throw new ArgumentException("SubscriptionType id is not valid");
            }
            return true;
        }

        private bool IsLogisticCompanyValid(int logisticCompanyId)
        {
            if(_logisticCompanyService.GetLogisticCompanyById(logisticCompanyId) == null)
            {
                throw new ArgumentException("Logistic Company id is not valid");
            }
            return true;
        }
    }
}

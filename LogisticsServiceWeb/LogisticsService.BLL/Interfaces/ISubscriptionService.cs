using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Interfaces
{
    public interface ISubscriptionService
    {
        Subscription? GetSubscriptionById(int subscriptionId);

        Subscription? GetSubscriptionByLogisticCompanyId(int logisticCompanyId);

        List<Subscription> GetAllSubscriptions();

        Subscription UpdateSubscription(SubscriptionDto subscriptionDto);

        bool HasSubscription(int logisticCompanyId);
    }
}

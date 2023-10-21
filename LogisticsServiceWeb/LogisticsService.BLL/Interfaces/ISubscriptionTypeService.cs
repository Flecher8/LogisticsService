using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticsService.Core.DbModels;

namespace LogisticsService.BLL.Interfaces
{
    public interface ISubscriptionTypeService
    {
        SubscriptionType? GetSubscriptionTypeById(int subscriptionId);

        List<SubscriptionType> GetAllSubscriptionTypes();

        void CreateSubscriptionType(SubscriptionType subscriptionType);

        void UpdateSubscriptionType(SubscriptionType subscriptionType);

        void DeleteSubscriptionType(int subscriptionId);

    }
}

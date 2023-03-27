using LogisticsService.Core.DbModels;
using LogisticsService.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.DAL.Repositories
{
    public class SubscriptionTypeRepository : IDataRepository<SubscriptionType>
    {
        private readonly DataContext context;

        public SubscriptionTypeRepository(DataContext context)
        {
            this.context = context;
        }

        public SubscriptionType? GetItemById(int itemId)
        {
            return context.SubscriptionTypes
                .FirstOrDefault(s => s.SubscriptionTypeId == itemId);
        }

        public List<SubscriptionType> GetItems(Expression<Func<SubscriptionType, bool>> filter)
        {
            return context.SubscriptionTypes
                .Where(filter)
                .ToList();
        }

        public void InsertItem(SubscriptionType item)
        {
            context.SubscriptionTypes.Add(item);
            context.SaveChanges();
        }

        public void UpdateItem(SubscriptionType item)
        {
            SubscriptionType? dbSubscriptionType = context.SubscriptionTypes.Find(item.SubscriptionTypeId);

            if(dbSubscriptionType is not null)
            {
                dbSubscriptionType.SubscriptionTypeName = item.SubscriptionTypeName;
                dbSubscriptionType.DurationInDays = item.DurationInDays;
                dbSubscriptionType.Price = item.Price;

                context.SaveChanges();
            }
        }

        public void DeleteItem(int itemId)
        {
            SubscriptionType? subscriptionType = context.SubscriptionTypes.Find(itemId);

            if (subscriptionType is not null)
            {
                context.SubscriptionTypes.Remove(subscriptionType);
                context.SaveChanges();
            }
        }
    }
}

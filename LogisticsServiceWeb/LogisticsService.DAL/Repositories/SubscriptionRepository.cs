using LogisticsService.Core.DbModels;
using LogisticsService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.DAL.Repositories
{
    public class SubscriptionRepository : IDataRepository<Subscription>
    {
        private readonly DataContext context;

        public SubscriptionRepository(DataContext context)
        {
            this.context = context;
        }

        public Subscription? GetItemById(int itemId)
        {
            return context.Subscriptions
                .FirstOrDefault(s => s.SubscriptionId == itemId);
        }

        public List<Subscription> GetFilteredItems(Expression<Func<Subscription, bool>> filter)
        {
            return context.Subscriptions
                .Where(filter)
                .ToList();
        }

        public void InsertItem(Subscription item)
        {
            context.Subscriptions.Add(item);
            context.SaveChanges();
        }

        public void UpdateItem(Subscription item)
        {
            Subscription? dbSubscription = context.Subscriptions.Find(item.SubscriptionId);

            if (dbSubscription is not null)
            {
                dbSubscription.StartDateTime = item.StartDateTime;
                dbSubscription.EndDateTime = item.EndDateTime;
                dbSubscription.SubscriptionType = item.SubscriptionType;

                context.SaveChanges();
            }
        }

        public void DeleteItem(int itemId)
        {
            Subscription? subscription = context.Subscriptions.Find(itemId);

            if (subscription is not null)
            {
                context.Subscriptions.Remove(subscription);
                context.SaveChanges();
            }
        }

        public List<Subscription> GetAllItems()
        {
            return context.Subscriptions.ToList();
        }
    }
}

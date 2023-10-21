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
    public class OrderTrackerRepository : IDataRepository<OrderTracker>
    {
        private readonly DataContext context;

        public OrderTrackerRepository(DataContext context)
        {
            this.context = context;
        }

        public OrderTracker? GetItemById(int itemId)
        {
            return context.OrderTrackers
                .FirstOrDefault(s => s.OrderTrackerId == itemId);
        }

        public List<OrderTracker> GetFilteredItems(Expression<Func<OrderTracker, bool>> filter)
        {
            return context.OrderTrackers
                .Where(filter)
                .ToList();
        }

        public int InsertItem(OrderTracker item)
        {
            context.OrderTrackers.Add(item);
            context.SaveChanges();
            int createdItemId = item.OrderTrackerId;
            return createdItemId;
        }

        public void UpdateItem(OrderTracker item)
        {
            OrderTracker? dbOrderTracker = context.OrderTrackers.Find(item.OrderTrackerId);

            if (dbOrderTracker is not null)
            {
                dbOrderTracker.Order = item.Order;
                dbOrderTracker.Latitude = item.Latitude;
                dbOrderTracker.Longitude = item.Longitude;
                dbOrderTracker.DateTime = item.DateTime;

                context.SaveChanges();
            }
        }

        public void DeleteItem(int itemId)
        {
            OrderTracker? orderTracker = context.OrderTrackers.Find(itemId);

            if (orderTracker is not null)
            {
                context.OrderTrackers.Remove(orderTracker);
                context.SaveChanges();
            }
        }

        public List<OrderTracker> GetAllItems()
        {
            return context.OrderTrackers.ToList();
        }
    }
}

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
    public class CancelledOrderRepository : IDataRepository<CancelledOrder>
    {
        private readonly DataContext context;

        public CancelledOrderRepository(DataContext context)
        {
            this.context = context;
        }

        public CancelledOrder? GetItemById(int itemId)
        {
            return context.CancelledOrders
                .FirstOrDefault(s => s.CancelledOrderId == itemId);
        }

        public List<CancelledOrder> GetItems(Expression<Func<CancelledOrder, bool>> filter)
        {
            return context.CancelledOrders
                .Where(filter)
                .ToList();
        }

        public void InsertItem(CancelledOrder item)
        {
            context.CancelledOrders.Add(item);
            context.SaveChanges();
        }

        public void UpdateItem(CancelledOrder item)
        {
            CancelledOrder? dbCancelledOrder = context.CancelledOrders.Find(item.CancelledOrderId);

            if (dbCancelledOrder is not null)
            {
                dbCancelledOrder.Order = item.Order;
                dbCancelledOrder.Reason = item.Reason;
                dbCancelledOrder.Description = item.Description;
                dbCancelledOrder.DateTime = item.DateTime;
                dbCancelledOrder.CancelledBy = item.CancelledBy;
                dbCancelledOrder.CancelledById = item.CancelledById;

                context.SaveChanges();
            }
        }

        public void DeleteItem(int itemId)
        {
            CancelledOrder? cancelledOrder = context.CancelledOrders.Find(itemId);

            if (cancelledOrder is not null)
            {
                context.CancelledOrders.Remove(cancelledOrder);
                context.SaveChanges();
            }
        }
    }
}

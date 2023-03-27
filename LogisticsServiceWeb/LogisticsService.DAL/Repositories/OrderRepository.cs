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
    public class OrderRepository : IDataRepository<Order>
    {
        private readonly DataContext context;

        public OrderRepository(DataContext context)
        {
            this.context = context;
        }

        public Order? GetItemById(int itemId)
        {
            return context.Orders
                .FirstOrDefault(s => s.OrderId == itemId);
        }

        public List<Order> GetItems(Expression<Func<Order, bool>> filter)
        {
            return context.Orders
                .Where(filter)
                .ToList();
        }

        public void InsertItem(Order item)
        {
            context.Orders.Add(item);
            context.SaveChanges();
        }

        public void UpdateItem(Order item)
        {
            Order? dbOrder = context.Orders.Find(item.OrderId);

            if (dbOrder is not null)
            {
                dbOrder.PrivateCompany = item.PrivateCompany;
                dbOrder.LogisticCompany = item.LogisticCompany;
                dbOrder.LogisticCompaniesDriver = item.LogisticCompaniesDriver;
                dbOrder.Sensor = item.Sensor;
                dbOrder.Cargo = item.Cargo;
                dbOrder.OrderStatus = item.OrderStatus;
                dbOrder.CreationDateTime = item.CreationDateTime;
                dbOrder.EstimatedDeliveryDateTime = item.EstimatedDeliveryDateTime;
                dbOrder.StartDeliveryDateTime = item.StartDeliveryDateTime;
                dbOrder.DeliveryDateTime = item.DeliveryDateTime;
                dbOrder.Price = item.Price;
                dbOrder.PathLength = item.PathLength;
                dbOrder.DeliveryStartAddress = item.DeliveryStartAddress;
                dbOrder.DeliveryEndAddress = item.DeliveryEndAddress;

                context.SaveChanges();
            }
        }

        public void DeleteItem(int itemId)
        {
            Order? order = context.Orders.Find(itemId);

            if (order is not null)
            {
                context.Orders.Remove(order);
                context.SaveChanges();
            }
        }
    }
}

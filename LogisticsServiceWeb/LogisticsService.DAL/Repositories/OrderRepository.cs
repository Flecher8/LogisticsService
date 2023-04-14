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
                .Include(o => o.PrivateCompany)
                .Include(o => o.LogisticCompany)
                .Include(o => o.Cargo)
                .Include(o => o.Sensor)
                .Include(o => o.StartDeliveryAddress)
                .Include(o => o.EndDeliveryAddress)
                .FirstOrDefault(s => s.OrderId == itemId);
        }

        public List<Order> GetFilteredItems(Expression<Func<Order, bool>> filter)
        {
            return context.Orders
                .Where(filter)
                .Include(o => o.PrivateCompany)
                .Include(o => o.LogisticCompany)
                .Include(o => o.Cargo)
                .Include(o => o.Sensor)
                .Include(o => o.StartDeliveryAddress)
                .Include(o => o.EndDeliveryAddress)
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
                dbOrder.LogisticCompaniesDriver = item.LogisticCompaniesDriver;
                dbOrder.Sensor = item.Sensor;
                dbOrder.OrderStatus = item.OrderStatus;
                dbOrder.EstimatedDeliveryDateTime = item.EstimatedDeliveryDateTime;
                dbOrder.Price = item.Price;
                dbOrder.StartDeliveryAddress = item.StartDeliveryAddress;
                dbOrder.EndDeliveryAddress = item.EndDeliveryAddress;
                dbOrder.PathLength = item.PathLength;

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

        public List<Order> GetAllItems()
        {
            return context.Orders
                .Include(o => o.PrivateCompany)
                .Include(o => o.LogisticCompany)
                .Include(o => o.Cargo)
                .Include(o => o.Sensor)
                .ToList();
        }
    }
}

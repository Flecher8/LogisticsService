using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Services
{
    public class OrderService : IOrderService
    {
        public void CreateOrder(OrderDto order)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrdersByLogisticCompaniesDriverId(int logisticCompaniesDriverId)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrdersByLogisticCompanyId(int logisticCompanyId)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrdersByPrivateCompanyId(int privateCompanyId)
        {
            throw new NotImplementedException();
        }

        public Order? GetOrderById(int orderId)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}

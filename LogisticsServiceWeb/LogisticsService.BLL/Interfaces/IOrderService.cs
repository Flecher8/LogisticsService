using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Interfaces
{
    public interface IOrderService
    {
        Order? GetOrderById(int orderId);

        List<Order> GetAllOrders();

        List<Order> GetAllOrdersByPrivateCompanyId(int privateCompanyId);

        List<Order> GetAllOrdersByLogisticCompanyId(int logisticCompanyId);

        List<Order> GetAllOrdersByLogisticCompaniesDriverId(int logisticCompaniesDriverId);

        void CreateOrder(OrderDto orderDto);

        void UpdateOrder(Order order);

        void DeleteOrder(int orderId);

        void UpdateOrderStatus(int orderId, string status);
    }
}

using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using LogisticsService.DAL.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Services
{
    public class OrderTrackerService : IOrderTrackerService
    {
        private readonly IDataRepository<OrderTracker> _orderTrackerRepository;
        private readonly ILogger<SystemAdminService> _logger;

        public void CreateOrderTracker(OrderTrackerDto orderTracker)
        {
            throw new NotImplementedException();
        }

        public List<OrderTracker> GetAllOrderTrackers()
        {
            throw new NotImplementedException();
        }

        public OrderTracker? GetCurrentOrderTrackerByOrderId(int orderId)
        {
            throw new NotImplementedException();
        }

        public OrderTracker? GetOrderTrackerById(int orderTrackerId)
        {
            throw new NotImplementedException();
        }

        public List<OrderTracker> GetOrderTrackersByOrderId(int orderId)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrderTracker(OrderTrackerDto orderTracker)
        {
            throw new NotImplementedException();
        }
    }
}

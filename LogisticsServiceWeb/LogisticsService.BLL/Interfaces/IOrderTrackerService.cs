using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Interfaces
{
    public interface IOrderTrackerService
    {
        OrderTracker? GetCurrentOrderTrackerByOrderId(int orderId);

        List<OrderTracker> GetOrderTrackersByOrderId(int orderId);

        List<OrderTracker> GetAllOrderTrackers();

        void CreateOrderTracker(OrderTrackerDto orderTrackerDto);
    }
}

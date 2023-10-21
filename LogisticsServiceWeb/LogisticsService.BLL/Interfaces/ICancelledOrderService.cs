using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Interfaces
{
    public interface ICancelledOrderService
    {
        CancelledOrder? GetCancelledOrderById(int cancelledOrderId);

        CancelledOrder? GetCancelledOrderByOrderId(int orderId);

        List<CancelledOrder> GetAllCancelledOrders();

        CancelledOrder? CreateCancelledOrder(CancelledOrderDto cancelledOrderDto);

    }
}

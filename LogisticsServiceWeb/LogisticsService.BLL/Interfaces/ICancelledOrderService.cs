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

        List<CancelledOrder> GetAllCancelledOrders();

        void CreateCancelledOrder(CancelledOrderDto cancelledOrderDto);

        void UpdateCancelledOrder(CancelledOrder cancelledOrder);

        void DeleteCancelledOrder(int cancelledOrderId);
    }
}

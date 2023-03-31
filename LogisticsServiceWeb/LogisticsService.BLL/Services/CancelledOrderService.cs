using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using LogisticsService.Core.Enums;
using LogisticsService.DAL.Interfaces;
using LogisticsService.DAL.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Services
{
    public class CancelledOrderService : ICancelledOrderService
    {
        private readonly IDataRepository<CancelledOrder> _cancelledOrderRepository;
        private readonly IOrderService _orderService;

        private readonly ILogger<CancelledOrderService> _logger;

        public CancelledOrderService(
            IDataRepository<CancelledOrder> cancelledOrderSRepository, 
            IOrderService orderService, 
            ILogger<CancelledOrderService> logger)
        {
            _cancelledOrderRepository = cancelledOrderSRepository;
            _orderService = orderService;
            _logger = logger;
        }

        public void CreateCancelledOrder(CancelledOrderDto cancelledOrderDto)
        {
            IsCancelledOrderValid(cancelledOrderDto);

            CancelledOrder cancelledOrder = new CancelledOrder();
            cancelledOrder.Order = _orderService.GetOrderById(cancelledOrderDto.OrderId);
            cancelledOrder.Reason = cancelledOrderDto.Reason;
            cancelledOrder.Description = cancelledOrderDto.Description;
            cancelledOrder.DateTime = DateTime.UtcNow;
            cancelledOrder.CancelledBy = GetCancelledOrderCancelledByType(cancelledOrderDto.CancelledBy);
            cancelledOrder.CancelledById = cancelledOrderDto.CancelledById;

            try
            {
                _cancelledOrderRepository.InsertItem(cancelledOrder);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        private bool IsCancelledOrderValid(CancelledOrderDto cancelledOrderDto)
        {
            if(!IsOrderIdValid(cancelledOrderDto.OrderId))
            {
                throw new ArgumentNullException("OrderId is not valid");
            }
            if(!IsCancelledOrderCancelledByTypeValid(cancelledOrderDto.CancelledBy))
            {
                throw new ArgumentOutOfRangeException("Cannceled by parameter is not valid");
            }
            return true;
        }

        private bool IsOrderIdValid(int orderId)
        {
            if (_orderService.GetOrderById(orderId) == null)
            {
                return false;
            }
            return true;
        }

        private bool IsCancelledOrderCancelledByTypeValid(string cancelledBy)
        {
            return Enum.IsDefined(typeof(OrderCancellationAuthority), cancelledBy);
        }

        private OrderCancellationAuthority GetCancelledOrderCancelledByType(string cancelledBy)
        {
            return (OrderCancellationAuthority)Enum.Parse(typeof(OrderCancellationAuthority), cancelledBy);
        }


        public void DeleteCancelledOrder(int cancelledOrderId)
        {
            try
            {
                _cancelledOrderRepository.DeleteItem(cancelledOrderId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public List<CancelledOrder> GetAllCancelledOrders()
        {
            var cancelledOrders = new List<CancelledOrder>();
            try
            {
                cancelledOrders = _cancelledOrderRepository.GetAllItems();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return cancelledOrders;
        }

        public CancelledOrder? GetCancelledOrderById(int cancelledOrderId)
        {
            try
            {
                CancelledOrder? cancelledOrder = _cancelledOrderRepository.GetItemById(cancelledOrderId);
                return cancelledOrder;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        public void UpdateCancelledOrder(CancelledOrder cancelledOrder)
        {
            try
            {
                _cancelledOrderRepository.UpdateItem(cancelledOrder);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}

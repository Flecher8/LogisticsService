using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
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
    public class OrderTrackerService : IOrderTrackerService
    {
        private readonly IDataRepository<OrderTracker> _orderTrackerRepository;
        private readonly IOrderService _orderService;

        private readonly ILogger<OrderTrackerService> _logger;

        public OrderTrackerService(
            IDataRepository<OrderTracker> orderTrackerRepository, 
            IOrderService orderService, 
            ILogger<OrderTrackerService> logger)
        {
            _orderTrackerRepository = orderTrackerRepository;
            _orderService = orderService;
            _logger = logger;
        }

        public void CreateOrderTracker(OrderTrackerDto orderTrackerDto)
        {
            
            OrderTracker orderTracker = new OrderTracker();
            orderTracker.Order = _orderService.GetOrderById(orderTrackerDto.OrderId);
            orderTracker.Latitude = orderTrackerDto.Latitude;
            orderTracker.Longitude = orderTrackerDto.Longitude;
            orderTracker.DateTime = DateTime.UtcNow;

            IsOrderTrackerValid(orderTracker);

            try
            {
                _orderTrackerRepository.InsertItem(orderTracker);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        private bool IsOrderTrackerValid(OrderTracker orderTracker)
        {
            if(orderTracker.Order == null)
            {
                throw new ArgumentNullException("Order is not valid");
            }
            return false;
        }

        public List<OrderTracker> GetAllOrderTrackers()
        {
            var orderTrackers = new List<OrderTracker>();
            try
            {
                orderTrackers = _orderTrackerRepository.GetAllItems();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return orderTrackers;
        }

        public OrderTracker? GetCurrentOrderTrackerByOrderId(int orderId)
        {
            Order? order = _orderService.GetOrderById(orderId);
            if(order == null)
            {
                return null;
            }

            try
            {
                OrderTracker? orderTracker = _orderTrackerRepository
                    .GetFilteredItems(o => o.Order.OrderId == orderId)
                    .OrderBy(o => (DateTime.UtcNow - o.DateTime).Duration())
                    .ToList()
                    .FirstOrDefault();

                if (orderTracker == null)
                {
                    orderTracker = CreateNewOrderTrackerByStartDeliveryAddress(order);
                }

                return orderTracker;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        private OrderTracker? CreateNewOrderTrackerByStartDeliveryAddress(Order order)
        {
            try
            {
                OrderTracker tracker = new OrderTracker();
                tracker.Order = order;
                if (order.DeliveryDateTime == null)
                {
                    tracker.Latitude = order.StartDeliveryAddress.Latitude;
                    tracker.Longitude = order.StartDeliveryAddress.Longitute;
                }
                else
                {
                    tracker.Latitude = order.EndDeliveryAddress.Latitude;
                    tracker.Longitude = order.EndDeliveryAddress.Longitute;
                }
                tracker.DateTime = DateTime.UtcNow;

                _orderTrackerRepository.InsertItem(tracker);
                return tracker;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public List<OrderTracker> GetOrderTrackersByOrderId(int orderId)
        {
            var orderTrackers = new List<OrderTracker>();
            try
            {
                orderTrackers = _orderTrackerRepository
                    .GetFilteredItems(o => o.Order.OrderId == orderId)
                    .OrderBy(o => (DateTime.Now.ToUniversalTime() - o.DateTime).Duration())
                    .Reverse()
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return orderTrackers;
        }
    }
}

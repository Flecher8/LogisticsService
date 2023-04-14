using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using LogisticsService.Core.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Services
{
    public class SmartDeviceCommunicationService : ISmartDeviceCommunicationService
    {
        private readonly IOrderTrackerService _orderTrackerService;
        private readonly IOrderService _orderService;
        private readonly ISmartDeviceService _smartDeviceService;

        private readonly ILogger<SmartDeviceCommunicationService> _logger;

        public SmartDeviceCommunicationService(
            IOrderTrackerService orderTrackerService, 
            IOrderService orderService, 
            ISmartDeviceService smartDeviceService, 
            ILogger<SmartDeviceCommunicationService> logger)
        {
            _orderTrackerService = orderTrackerService;
            _orderService = orderService;
            _smartDeviceService = smartDeviceService;
            _logger = logger;
        }

        public bool ActivateSensor(int sensorId)
        {
            //if (order.OrderStatus == OrderStatus.InTransit)
            //{
            //    order.StartDeliveryDateTime = DateTime.UtcNow;
            //}
            try
            {
                Order order = _orderService
                    .GetAllOrders()
                    .AsParallel()
                    .Where(s => s.Sensor.SensorId == sensorId)
                    .ToList()
                    .FirstOrDefault();
                if(order == null)
                {
                    return false;
                }
               
                if(order.OrderStatus == Core.Enums.OrderStatus.OrderAccepted)
                {
                    _orderService.UpdateOrderStatus(order.OrderId);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public bool WriteOrderTrackers(SmartDeviceCommunicationDto communicationDto)
        {
            try
            {
                SmartDevice smartDevice = _smartDeviceService.GetSmartDeviceById(communicationDto.SmartDeviceId);

                List<Order> orders = _orderService
                    .GetAllOrders()
                    .AsParallel()
                    .Where(s =>
                    s.LogisticCompany.LogisticCompanyId == smartDevice.LogisticCompany.LogisticCompanyId &&
                    s.Sensor != null)
                    .ToList();
                foreach (var order in orders)
                {
                    foreach (var sensor in smartDevice.Sensors)
                    {
                        if (order.Sensor.SensorId == sensor.SensorId && 
                            (order.OrderStatus == OrderStatus.InTransit || 
                            order.OrderStatus == OrderStatus.PreparingForDispatch) && 
                            sensor != null)
                        {
                            OrderTrackerDto orderTrackerDto = new OrderTrackerDto();
                            orderTrackerDto.OrderId = order.OrderId;
                            orderTrackerDto.Latitude = communicationDto.Latitude;
                            orderTrackerDto.Longitude = communicationDto.Longitude;
                            _orderTrackerService.CreateOrderTracker(orderTrackerDto);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
            return true;
        }
    }
}

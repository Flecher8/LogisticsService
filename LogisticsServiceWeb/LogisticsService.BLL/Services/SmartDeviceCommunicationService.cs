using LogisticsService.BLL.Helpers;
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

        private double distanceInKmWhenCargoIsConsideredDelivered = 0.1;

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
            try
            {
                Order? order = _orderService
                    .GetAllOrders()
                    .AsParallel()
                    .Where(s => s.Sensor.SensorId == sensorId)
                    .ToList()
                    .FirstOrDefault();
                if(order == null)
                {
                    return false;
                }
               
                if(order.OrderStatus == OrderStatus.OrderAccepted)
                {
                    _orderService.UpdateOrderStatus(order.OrderId);
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
                SmartDevice? smartDevice = _smartDeviceService.GetSmartDeviceById(communicationDto.SmartDeviceId);

                if(smartDevice == null)
                {
                    return false;
                }


                List<Order> orders = GetOrdersConnectedToSmartDevice(smartDevice);

                CreateOrderTrackersByOrdersAndSmartDevice(orders, smartDevice, communicationDto);
                // If order near order end delivery address, status == Delivered
                IsOrdersNearEndDeliveryAddress(orders, communicationDto);

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        private List<Order> GetOrdersConnectedToSmartDevice(SmartDevice smartDevice)
        {
            return _orderService
                    .GetAllOrders()
                    .Where(s =>
                    s.LogisticCompany.LogisticCompanyId == smartDevice.LogisticCompany.LogisticCompanyId &&
                    s.Sensor != null)
                    .ToList();
        }

        private void CreateOrderTrackersByOrdersAndSmartDevice(List<Order> orders, SmartDevice smartDevice, SmartDeviceCommunicationDto communicationDto)
        {
            foreach (var order in orders)
            {
                foreach (var sensor in smartDevice.Sensors)
                {
                    if (order.Sensor.SensorId == sensor.SensorId && (
                        order.OrderStatus == OrderStatus.InTransit)
                        )
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

        private void IsOrdersNearEndDeliveryAddress(List<Order> orders, SmartDeviceCommunicationDto communicationDto)
        {
            foreach(var order in orders)
            {
                if (IsSmartDeviceNearEndDeliveryAddress(communicationDto, order))
                {
                    _orderService.UpdateOrderStatus(order.OrderId);
                }
            }
        }

        private bool IsSmartDeviceNearEndDeliveryAddress(SmartDeviceCommunicationDto communicationDto, Order order)
        {
            DistanceCalculator distanceCalculator = new DistanceCalculator();

            Coordinate coordinate1 = new Coordinate(communicationDto.Latitude, communicationDto.Longitude);
            Coordinate coordinate2 = new Coordinate(order.EndDeliveryAddress.Latitude, order.EndDeliveryAddress.Longitute);

            return DistanceCalculator.CalculateDistance(coordinate1, coordinate2) <= distanceInKmWhenCargoIsConsideredDelivered;
        }
    }
}

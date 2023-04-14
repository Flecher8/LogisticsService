using LogisticsService.BLL.Helpers.GoogleMapsApi;
using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using LogisticsService.Core.Enums;
using LogisticsService.Core.Helpers;
using LogisticsService.DAL.Interfaces;
using LogisticsService.DAL.Repositories;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IDataRepository<Order> _orderRepository;
        
        private readonly IPrivateCompanyService _privateCompanyService;
        private readonly ILogisticCompanyService _logisticCompanyService;
        private readonly ILogisticCompaniesDriverService _logisticCompaniesDriverService;
        private readonly ICargoService _cargoService;
        private readonly IAddressService _addressService;
        private readonly IRateService _rateService;
        private readonly ISensorService _sensorService;

        private readonly IGoogleMapsApiDirectionsService _googleMapsApiDirectionsService;


        private readonly ILogger<OrderService> _logger;

        public OrderService(
            IDataRepository<Order> orderRepository,
            IPrivateCompanyService privateCompanyService,
            ILogisticCompanyService logisticCompanyService,
            ILogisticCompaniesDriverService logisticCompaniesDriverService,
            ICargoService cargoService,
            IAddressService addressService,
            IRateService rateService,
            ISensorService sensorService,
            IGoogleMapsApiDirectionsService googleMapsApiDirectionsService,
            ILogger<OrderService> logger
            )
        {
            _orderRepository = orderRepository;
            _privateCompanyService = privateCompanyService;
            _logisticCompanyService = logisticCompanyService;
            _logisticCompaniesDriverService = logisticCompaniesDriverService;
            _cargoService = cargoService;
            _addressService = addressService;
            _rateService = rateService;
            _sensorService = sensorService;
            _googleMapsApiDirectionsService = googleMapsApiDirectionsService;
            _logger = logger;
        }

        public async Task<Order> CreateOrder(OrderDto orderDto)
        {
            IsOrderValid(orderDto);

            Order order = new Order();
            order.PrivateCompany = _privateCompanyService.GetPrivateCompanyById(orderDto.PrivateCompanyId);
            order.LogisticCompany = _logisticCompanyService.GetLogisticCompanyById(orderDto.LogisticCompanyId);
            order.Cargo = _cargoService.GetCargoById(orderDto.CargoId);
            order.StartDeliveryAddress = _addressService.GetAddressById(orderDto.StartDeliveryAddressId);
            order.EndDeliveryAddress = _addressService.GetAddressById(orderDto.EndDeliveryAddressId);

            order.OrderStatus = OrderStatus.WaitingForAcceptanceByLogisticCompany;

            order.CreationDateTime = DateTime.UtcNow;
            // TODO maybe make orderDto.TimeZoneId and create a class where system can calculate UTC time
            order.EstimatedDeliveryDateTime = orderDto.EstimatedDeliveryDateTime;

            order.StartDeliveryDateTime = null;
            order.DeliveryDateTime = null;

            DirectionInfo directionInfo = await GetDirectionInfo(order);

            order.Price = await GetOrderPrice(order, directionInfo);
            order.PathLength = directionInfo.DistanceInMeters;
            
            try
            {
                _orderRepository.InsertItem(order);
                return order;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null; 
        }

        private bool IsOrderValid(OrderDto orderDto)
        {
            return (IsOrderPrivateCompanyIdValid(orderDto.PrivateCompanyId) &&
            IsOrderLogisticCompanyIdValid(orderDto.LogisticCompanyId) &&
            IsOrderCargoIdValid(orderDto.CargoId) &&
            IsOrderAddressIdValid(orderDto.StartDeliveryAddressId) &&
            IsOrderAddressIdValid(orderDto.EndDeliveryAddressId)
            );
        }

        private bool IsOrderPrivateCompanyIdValid(int privateCompanyId)
        {
            if(privateCompanyId <= 0 || 
                _privateCompanyService.GetPrivateCompanyById(privateCompanyId) == null)
            {
                throw new ArgumentOutOfRangeException("PrivateCompanyId is not valid");
            }
            return true;
        }

        private bool IsOrderLogisticCompanyIdValid(int logisticCompanyId)
        {
            if (logisticCompanyId <= 0 || 
                _logisticCompanyService.GetLogisticCompanyById(logisticCompanyId) == null)
            {
                throw new ArgumentOutOfRangeException("LogisticCompanyId is not valid");
            }
            return true;
        }

        private bool IsOrderCargoIdValid(int cargoId)
        {
            if (cargoId <= 0 ||
                _cargoService.GetCargoById(cargoId) == null ||
                _orderRepository.GetFilteredItems(o => o.Cargo.CargoId == cargoId).Count != 0)
            {
                throw new ArgumentOutOfRangeException("CargoId is not valid");
            }
            return true;
        }

        private bool IsOrderAddressIdValid(int addressId)
        {
            if (addressId <= 0 ||
                _addressService.GetAddressById(addressId) == null)
            {
                throw new ArgumentOutOfRangeException("AddressId is not valid");
            }
            return true;
        }

        private async Task<double> GetOrderPrice(Order order, DirectionInfo directionInfo)
        {
            const int numOfMetersInKm = 1000;
            Rate rate = _rateService.GetRateByLogisticCompanyId(order.LogisticCompany.LogisticCompanyId);
            
            double price = new PriceCalculator(
                rate.PriceForKmInDollar,
                directionInfo.DistanceInMeters / numOfMetersInKm)
                .Compute();

            return price;
        }

        private async Task<DirectionInfo> GetDirectionInfo(Order order)
        {
            GoogleMapsApiDirectionsParam directionsParam = new GoogleMapsApiDirectionsParam();
            directionsParam.StartAddress = order.StartDeliveryAddress;
            directionsParam.EndAddress = order.EndDeliveryAddress;

            DirectionInfo directionInfo =
                await _googleMapsApiDirectionsService
                .GetGoogleMapsDirectionInfo(directionsParam);
            return directionInfo;
        }


        public void DeleteOrder(int orderId)
        {
            Order order = GetOrderById(orderId);
            if(order == null || ((int)order.OrderStatus > (int)OrderStatus.WaitingForPaymentByPrivateCompany))
            {
                throw new ArgumentException("This order can't be deleted");
            }
            try
            {
                _orderRepository.DeleteItem(orderId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public List<Order> GetAllOrders()
        {
            var orders = new List<Order>();
            try
            {
                orders = _orderRepository.GetAllItems();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return orders;
        }

        public List<Order> GetAllOrdersByLogisticCompaniesDriverId(int logisticCompaniesDriverId)
        {
            var orders = new List<Order>();
            try
            {
                orders = _orderRepository
                    .GetFilteredItems(o => 
                    o.LogisticCompaniesDriver.LogisticCompaniesDriverId == logisticCompaniesDriverId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return orders;
        }

        public List<Order> GetAllOrdersByLogisticCompanyId(int logisticCompanyId)
        {
            var orders = new List<Order>();
            try
            {
                orders = _orderRepository.GetFilteredItems(o => o.LogisticCompany.LogisticCompanyId == logisticCompanyId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return orders;
        }

        public List<Order> GetAllOrdersByPrivateCompanyId(int privateCompanyId)
        {
            var orders = new List<Order>();
            try
            {
                orders = _orderRepository
                    .GetFilteredItems(o => o.PrivateCompany.PrivateCompanyId == privateCompanyId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return orders;
        }

        public Order? GetOrderById(int orderId)
        {
            try
            {
                Order? order = _orderRepository.GetItemById(orderId);
                return order;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null; 
        }

        public Order? UpdateOrder(OrderDto orderDto)
        {
            try
            {
                Order? order = GetOrderById(orderDto.OrderId);
                if(order == null)
                {
                    return null;
                }

                order.LogisticCompaniesDriver = TryGetLogisticCompaniesDriver(orderDto.LogisticCompaniesDriverId);
                order.Sensor = TryGetSensor(orderDto.SensorId);

                UpdateOrder(order);

                return order;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        private LogisticCompaniesDriver? TryGetLogisticCompaniesDriver(int driverId)
        {
            LogisticCompaniesDriver? logisticCompaniesDriver = _logisticCompaniesDriverService
                    .GetLogisticCompaniesDriverById(driverId);
            if (logisticCompaniesDriver == null)
            {
                throw new ArgumentException("LogisticCompaniesDriver id is not correct");
            }
            return logisticCompaniesDriver;
        }

        private Sensor? TryGetSensor(int sensorId)
        {
            Sensor? sensor = _sensorService.GetSensorById(sensorId);

            if (sensor == null ||
                _orderRepository.GetFilteredItems(o => o.Sensor.SensorId == sensorId).Count != 0)
            {
                throw new ArgumentException("Sensor id is not correct");
            }

            return sensor;
        }

        public void UpdateOrderStatus(int orderId)
        {
            Order order = GetOrderById(orderId);
            if (order.OrderStatus == OrderStatus.Cancelled || 
                order.OrderStatus == OrderStatus.Delivered || 
                order.OrderStatus == OrderStatus.WaitingForPaymentByPrivateCompany)
            {
                return;
            }

            order.OrderStatus = (OrderStatus)((int)order.OrderStatus + 1);

            if(order.OrderStatus == OrderStatus.InTransit)
            {
                order.StartDeliveryDateTime = DateTime.UtcNow;
            }

            if (order.OrderStatus == OrderStatus.Delivered)
            {
                order.DeliveryDateTime = DateTime.UtcNow;
                order.Sensor = null;
            }

            UpdateOrder(order);
        }

        public void MakeOrderStatusCancelled(int orderId)
        {
            Order? order = GetOrderById(orderId);
            order.OrderStatus = OrderStatus.Cancelled;

            UpdateOrder(order);
        }

        private void UpdateOrder(Order order)
        {
            try
            {
                _orderRepository.UpdateItem(order);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public void UpdateOrderStatusPaid(int orderId)
        {
            Order order = GetOrderById(orderId);

            order.OrderStatus = OrderStatus.OrderAccepted;

            UpdateOrder(order);
        }
    }
}

using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.DbModels;
using LogisticsService.Core.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<AnalyticsService> _logger;

        public AnalyticsService(IOrderService orderService, ILogger<AnalyticsService> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        public double GetAverageDeliveryPathLengthByLogisticCompany(int logisticCompanyId, string metric = "km")
        {
            List<Order> orders = _orderService.GetAllOrdersByLogisticCompanyId(logisticCompanyId);
            return CalculateAverageDeliveryPathLength(orders, metric);
        }

        public double GetAverageDeliveryPathLengthByPrivateCompany(int privateCompanyId, string metric = "km")
        {
            List<Order> orders = _orderService.GetAllOrdersByPrivateCompanyId(privateCompanyId);
            return CalculateAverageDeliveryPathLength(orders, metric);
        }

        private double CalculateAverageDeliveryPathLength(List<Order> orders, string metric = "km")
        {
            var averagePathLength = orders
                .AsParallel()
                .Where(order => order.OrderStatus == OrderStatus.Delivered)
                .Average(order => order.PathLength);

            switch (metric)
            {
                case "km":
                    averagePathLength /= 1000;
                    break;
                case "mi":
                    double milesInMeter = 0.000621371;
                    averagePathLength *= milesInMeter;
                    break;
                default:
                    throw new ArgumentException("Неверное значение параметра metric");
            }

            return averagePathLength;
        }

        public TimeSpan GetAverageDeliveryTimeByLogisticCompany(int logisticCompanyId)
        {
            List<Order> orders = _orderService.GetAllOrdersByLogisticCompanyId(logisticCompanyId);
            return CalculateAverageDeliveryTime(orders);
        }

        public TimeSpan GetAverageDeliveryTimeByPrivateCompany(int privateCompanyId)
        {
            List<Order> orders = _orderService.GetAllOrdersByPrivateCompanyId(privateCompanyId);
            return CalculateAverageDeliveryTime(orders);
        }

        private TimeSpan CalculateAverageDeliveryTime(List<Order> orders)
        {
            int count = orders
                .Where(order => order.OrderStatus.Equals(OrderStatus.Delivered))
                .Count();
            TimeSpan totalDeliveryTime = TimeSpan.Zero;

            totalDeliveryTime = orders
                .AsParallel()
                .Where(order => order.OrderStatus.Equals(OrderStatus.Delivered))
                .Select(order =>
                {
                    DateTime startDateTime = (DateTime)order.StartDeliveryDateTime;
                    DateTime EndDateTime = (DateTime)order.DeliveryDateTime;
                    TimeSpan deliveryTime = EndDateTime.Subtract(startDateTime);
                    _logger.LogInformation(deliveryTime.ToString());
                    return deliveryTime;
                })
                .Aggregate(totalDeliveryTime, (sum, deliveryTime) => sum.Add(deliveryTime));

            TimeSpan averageDeliveryTime = TimeSpan.FromSeconds(totalDeliveryTime.TotalSeconds / count);
            return averageDeliveryTime;
        }


        public double GetAverageOrderPriceByLogisticCompany(int logisticCompanyId)
        {
            List<Order> orders = _orderService.GetAllOrdersByLogisticCompanyId(logisticCompanyId);
            return CalculateAverageOrderPrice(orders);
        }

        public double GetAverageOrderPriceByPrivateCompany(int privateCompanyId)
        {
            List<Order> orders = _orderService.GetAllOrdersByPrivateCompanyId(privateCompanyId);
            return CalculateAverageOrderPrice(orders);
        }

        private double CalculateAverageOrderPrice(List<Order> orders)
        {
            if (orders == null || orders.Count == 0)
            {
                return 0;
            }

            double totalPrice = orders
                .AsParallel()
                .Where(order => order.OrderStatus.Equals(OrderStatus.Delivered))
                .Sum(order => order.Price);

            double averagePrice = totalPrice / orders.Where(order => order.OrderStatus.Equals(OrderStatus.Delivered)).Count();

            return averagePrice;
        }


        public int GetNumberOfDeliveredOrdersByLogisticCompany(int logisticCompanyId)
        {
            List<Order> orders = _orderService.GetAllOrdersByLogisticCompanyId(logisticCompanyId);
            return CalculateNumberOfDeliveredOrders(orders);
        }

        public int GetNumberOfDeliveredOrdersByPrivateCompany(int privateCompanyId)
        {
            List<Order> orders = _orderService.GetAllOrdersByPrivateCompanyId(privateCompanyId);
            return CalculateNumberOfDeliveredOrders(orders);
        }

        private int CalculateNumberOfDeliveredOrders(List<Order> orders)
        {
            int count = 0;
            Parallel.ForEach(orders, order =>
            {
                if (order.OrderStatus.Equals(OrderStatus.Delivered))
                {
                    Interlocked.Increment(ref count);
                }
            });
            return count;
        }


        public int GetNumberOfNotDeliveredOrdersByLogisticCompany(int logisticCompanyId)
        {
            List<Order> orders = _orderService.GetAllOrdersByLogisticCompanyId(logisticCompanyId);
            return CalculateNumberOfNotDeliveredOrders(orders);
        }

        public int GetNumberOfNotDeliveredOrdersByPrivateCompany(int privateCompanyId)
        {
            List<Order> orders = _orderService.GetAllOrdersByPrivateCompanyId(privateCompanyId);
            return CalculateNumberOfNotDeliveredOrders(orders);
        }

        private int CalculateNumberOfNotDeliveredOrders(List<Order> orders)
        {
            int count = 0;
            Parallel.ForEach(orders, order =>
            {
                if (!order.OrderStatus.Equals(OrderStatus.Delivered))
                {
                    Interlocked.Increment(ref count);
                }
            });

            return count;
        }
    }
}

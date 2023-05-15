using mobile.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using mobile.Models.Enums;
using mobile.Services.Interfaces;

namespace mobile.Services
{
    public class OrdersService : ServerService
    {
        const string ordersApi = "Orders/logisticCompaniesDriverId/";
        const string orderByIdApi = "Orders/id/";

        const string cancelOrderApi = "CancelledOrders";

        public List<Order> GetOrdersByOrderStatus(List<Order> orders, OrderStatus orderStatus)
        {
            List<Order> acceptedOrders = new List<Order>();
            foreach (Order order in orders)
            {
                if (order.OrderStatus == orderStatus)
                {
                    acceptedOrders.Add(order);
                }
            }
            return acceptedOrders;
        }

        public async Task<List<Order>> GetLogisticCompaniesDriverOrders()
        {
            PropertiesService propertiesService = new PropertiesService();
            HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    SecurityTokenStartTemplate,
                propertiesService.GetProperty(UserTokenPath).ToString()
                );

            HttpResponseMessage response = await HttpClient.GetAsync(ordersApi + propertiesService.GetProperty(UserIdPath).ToString());

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new List<Order>();
            }

            HttpContent responseContent = response.Content;
            List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(await responseContent.ReadAsStringAsync());

            return orders;
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            PropertiesService propertiesService = new PropertiesService();
            HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    SecurityTokenStartTemplate,
                propertiesService.GetProperty(UserTokenPath).ToString()
                );

            HttpResponseMessage response = await HttpClient.GetAsync(orderByIdApi + orderId.ToString());

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            HttpContent responseContent = response.Content;
            Order order = JsonConvert.DeserializeObject<Order>(await responseContent.ReadAsStringAsync());

            return order;
        }

        public async Task<bool> CancelOrder(CancelledOrder cancelledOrder)
        {
            PropertiesService propertiesService = new PropertiesService();
            cancelledOrder.CancelledBy = "LogisticsCompanyDriver";
            cancelledOrder.CancelledById = (int)propertiesService.GetProperty(UserIdPath);

            HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    SecurityTokenStartTemplate,
                propertiesService.GetProperty(UserTokenPath).ToString()
                );

            StringContent content = new StringContent(JsonConvert.SerializeObject(cancelledOrder), Encoding.UTF8, RequestMediaType);

            HttpResponseMessage response = await HttpClient.PostAsync(cancelOrderApi, content);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            return true;
        }
    }
}

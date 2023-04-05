using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.DbModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IOrderService _orderService;
        private readonly ITransactionService _transactionService;
        
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(
            IOrderService orderService, 
            ILogger<PaymentService> logger,
            ITransactionService transactionService)
        {
            _orderService = orderService;
            _logger = logger;
            _transactionService = transactionService;
        }

        public bool OrderPayment(int orderId)
        {
            Order? order = _orderService.GetOrderById(orderId);
            if (order != null && order.OrderStatus == Core.Enums.OrderStatus.WaitingForPaymentByPrivateCompany)
            {
                try
                {
                    _transactionService.CreateTransaction(order);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    return false;
                }

                try
                {
                    _orderService.UpdateOrderStatusPaid(orderId);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    return false;
                }

                
                return true;
            }

            return false;
        }
    }
}

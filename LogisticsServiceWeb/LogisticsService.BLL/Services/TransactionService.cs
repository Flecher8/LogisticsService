using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.DbModels;
using LogisticsService.Core.Helpers.Transactions;
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
    public class TransactionService : ITransactionService
    {
        private readonly IDataRepository<Transaction> _transactionRepository;
        private readonly IOrderService _orderService;
        private readonly ILogger<TransactionService> _logger;

        public TransactionService(
            IDataRepository<Transaction> transactionRepository, 
            IOrderService orderService,
            ILogger<TransactionService> logger)
        {
            _transactionRepository = transactionRepository;
            _orderService = orderService;
            _logger = logger;
        }

        public void CreateTransaction(Order order)
        {
            IsOrderCorrect(order);

            Transaction transaction = new Transaction();
            transaction.PrivateCompany = order.PrivateCompany;
            transaction.LogisticCompany = order.LogisticCompany;
            transaction.Order = order;
            transaction.Amount = new TransactionAmountCalculator(order.Price).Compute();
            transaction.DateTime = DateTime.UtcNow;
            transaction.CommissionPercent = new TransactionCalculator().CommisionPersent;
            transaction.EarnedAmount = new TransactionEarnedAmountCalculator(order.Price).Compute();

            try
            {
                _transactionRepository.InsertItem(transaction);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        private bool IsOrderCorrect(Order order)
        {
            if(_orderService.GetOrderById(order.OrderId) != null || order.Equals(order))
            {
                return true;
            }
            throw new ArgumentNullException("Order is not in database");
        }

        public List<Transaction> GetAllTransactions()
        {
            var transactions = new List<Transaction>();
            try
            {
                transactions = _transactionRepository.GetAllItems();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return transactions;
        }

        public Transaction? GetTransactionById(int transactionId)
        {
            try
            {
                Transaction? transaction = _transactionRepository.GetItemById(transactionId);
                return transaction;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }
    }
}

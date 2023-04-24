using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Interfaces
{
    public interface ITransactionService
    {
        Transaction? GetTransactionById(int transactionId);

        List<Transaction> GetAllTransactions();

        void CreateTransaction(Order order);
    }
}

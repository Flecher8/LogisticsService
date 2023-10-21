using LogisticsService.Core.DbModels;
using LogisticsService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.DAL.Repositories
{
    public class TransactionRepository : IDataRepository<Transaction>
    {
        private readonly DataContext context;

        public TransactionRepository(DataContext context)
        {
            this.context = context;
        }

        public Transaction? GetItemById(int itemId)
        {
            return context.Transactions
                .FirstOrDefault(s => s.TransactionId == itemId);
        }

        public List<Transaction> GetFilteredItems(Expression<Func<Transaction, bool>> filter)
        {
            return context.Transactions
                .Where(filter)
                .ToList();
        }

        public int InsertItem(Transaction item)
        {
            context.Transactions.Add(item);
            context.SaveChanges();
            int createdItemId = item.TransactionId;
            return createdItemId;
        }

        public void UpdateItem(Transaction item)
        {
            Transaction? dbTransaction = context.Transactions.Find(item.TransactionId);

            if (dbTransaction is not null)
            {
                dbTransaction.PrivateCompany = item.PrivateCompany;
                dbTransaction.LogisticCompany = item.LogisticCompany;
                dbTransaction.Order = item.Order;
                dbTransaction.Amount = item.Amount;
                dbTransaction.DateTime = item.DateTime;
                dbTransaction.CommissionPercent = item.CommissionPercent;
                dbTransaction.EarnedAmount = item.EarnedAmount;

                context.SaveChanges();
            }
        }

        public void DeleteItem(int itemId)
        {
            Transaction? transaction = context.Transactions.Find(itemId);

            if (transaction is not null)
            {
                context.Transactions.Remove(transaction);
                context.SaveChanges();
            }
        }

        public List<Transaction> GetAllItems()
        {
            return context.Transactions.ToList();
        }
    }
}

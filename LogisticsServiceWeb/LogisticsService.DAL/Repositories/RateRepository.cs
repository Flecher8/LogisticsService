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
    public class RateRepository : IDataRepository<Rate>
    {
        private readonly DataContext context;

        public RateRepository(DataContext context)
        {
            this.context = context;
        }

        public Rate? GetItemById(int itemId)
        {
            return context.Rates
                .FirstOrDefault(s => s.RateId == itemId);
        }

        public List<Rate> GetFilteredItems(Expression<Func<Rate, bool>> filter)
        {
            return context.Rates
                .Where(filter)
                .ToList();
        }

        public void InsertItem(Rate item)
        {
            context.Rates.Add(item);
            context.SaveChanges();
        }

        public void UpdateItem(Rate item)
        {
            Rate? dbRate = context.Rates.Find(item.RateId);

            if (dbRate is not null)
            {
                dbRate.PriceForKmInDollar = item.PriceForKmInDollar;
                dbRate.LogisticCompany = item.LogisticCompany;
                
                context.SaveChanges();
            }
        }

        public void DeleteItem(int itemId)
        {
            Rate? rate = context.Rates.Find(itemId);

            if (rate is not null)
            {
                context.Rates.Remove(rate);
                context.SaveChanges();
            }
        }

        public List<Rate> GetAllItems()
        {
            return context.Rates.ToList();
        }
    }
}

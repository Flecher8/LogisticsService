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
    public class LogisticCompanyRepository : IDataRepository<LogisticCompany>
    {
        private readonly DataContext context;

        public LogisticCompanyRepository(DataContext context)
        {
            this.context = context;
        }

        public LogisticCompany? GetItemById(int itemId)
        {
            return context.LogisticCompanies
               .FirstOrDefault(s => s.LogisticCompanyId == itemId);
        }

        public List<LogisticCompany> GetFilteredItems(Expression<Func<LogisticCompany, bool>> filter)
        {
            return context.LogisticCompanies
                .Where(filter)
                .ToList();
        }

        public void InsertItem(LogisticCompany item)
        {
            context.LogisticCompanies.Add(item);
            context.SaveChanges();
        }

        public void UpdateItem(LogisticCompany item)
        {
            LogisticCompany? dbLogisticCompany = context.LogisticCompanies.Find(item.LogisticCompanyId);

            if (dbLogisticCompany is not null)
            {
                dbLogisticCompany.LogisticCompanyName = item.LogisticCompanyName;
                dbLogisticCompany.LogisticCompanyEmail = item.LogisticCompanyEmail;
                dbLogisticCompany.HashedPassword = item.HashedPassword;
                dbLogisticCompany.Description = item.Description;

                context.SaveChanges();
            }
        }

        public void DeleteItem(int itemId)
        {
            LogisticCompany? logisticCompany = context.LogisticCompanies.Find(itemId);

            if (logisticCompany is not null)
            {
                context.LogisticCompanies.Remove(logisticCompany);
                context.SaveChanges();
            }
        }

        public List<LogisticCompany> GetAllItems()
        {
            return context.LogisticCompanies.ToList();
        }
    }
}

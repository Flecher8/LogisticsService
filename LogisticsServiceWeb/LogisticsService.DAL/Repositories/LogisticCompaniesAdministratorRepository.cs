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
    public class LogisticCompaniesAdministratorRepository : IDataRepository<LogisticCompaniesAdministrator>
    {
        private readonly DataContext context;

        public LogisticCompaniesAdministratorRepository(DataContext context)
        {
            this.context = context;
        }

        public LogisticCompaniesAdministrator? GetItemById(int itemId)
        {
            return context.LogisticCompaniesAdministrators
                .FirstOrDefault(s => s.LogisticCompaniesAdministratorId == itemId);
        }

        public List<LogisticCompaniesAdministrator> GetItems(Expression<Func<LogisticCompaniesAdministrator, bool>> filter)
        {
            return context.LogisticCompaniesAdministrators
                .Where(filter)
                .ToList();
        }

        public void InsertItem(LogisticCompaniesAdministrator item)
        {
            context.LogisticCompaniesAdministrators.Add(item);
            context.SaveChanges();
        }

        public void UpdateItem(LogisticCompaniesAdministrator item)
        {
            LogisticCompaniesAdministrator? dbLogisticCompaniesAdministrator = 
                context.LogisticCompaniesAdministrators
                .Find(item.LogisticCompaniesAdministratorId);

            if (dbLogisticCompaniesAdministrator is not null)
            {
                dbLogisticCompaniesAdministrator.FirstName = item.FirstName;
                dbLogisticCompaniesAdministrator.LastName = item.LastName;
                dbLogisticCompaniesAdministrator.Email = item.Email;
                dbLogisticCompaniesAdministrator.HashedPassword = item.HashedPassword;
                dbLogisticCompaniesAdministrator.LogisticCompany = item.LogisticCompany;

                context.SaveChanges();
            }
        }

        public void DeleteItem(int itemId)
        {
            LogisticCompaniesAdministrator? logisticCompaniesAdministrator = context.LogisticCompaniesAdministrators.Find(itemId);

            if (logisticCompaniesAdministrator is not null)
            {
                context.LogisticCompaniesAdministrators.Remove(logisticCompaniesAdministrator);
                context.SaveChanges();
            }
        }
    }
}

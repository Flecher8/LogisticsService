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
    public class SystemAdminRepository : IDataRepository<SystemAdmin>
    {
        private readonly DataContext context;

        public SystemAdminRepository(DataContext context)
        {
            this.context = context;
        }

        public SystemAdmin? GetItemById(int itemId)
        {
            return context.SystemAdmins
                .FirstOrDefault(s => s.SystemAdminId == itemId);
        }

        public List<SystemAdmin> GetFilteredItems(Expression<Func<SystemAdmin, bool>> filter)
        {
            return context.SystemAdmins
                .Where(filter)
                .ToList();
        }

        public void InsertItem(SystemAdmin item)
        {
            context.SystemAdmins.Add(item);
            context.SaveChanges();
        }

        public void UpdateItem(SystemAdmin item)
        {
            SystemAdmin? dbSystemAdmin = context.SystemAdmins.Find(item.SystemAdminId);

            if (dbSystemAdmin is not null)
            {
                dbSystemAdmin.FirstName = item.FirstName;
                dbSystemAdmin.LastName = item.LastName;
                dbSystemAdmin.Email = item.Email;
                dbSystemAdmin.HashedPassword = item.HashedPassword;

                context.SaveChanges();
            }
        }

        public void DeleteItem(int itemId)
        {
            SystemAdmin? systemAdmin = context.SystemAdmins.Find(itemId);

            if (systemAdmin is not null)
            {
                context.SystemAdmins.Remove(systemAdmin);
                context.SaveChanges();
            }
        }

        public List<SystemAdmin> GetAllItems()
        {
            return context.SystemAdmins.ToList();
        }
    }
}

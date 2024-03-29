﻿using LogisticsService.Core.DbModels;
using LogisticsService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.DAL.Repositories
{
    public class PrivateCompanyRepository : IDataRepository<PrivateCompany>
    {
        private readonly DataContext context;

        public PrivateCompanyRepository(DataContext context)
        {
            this.context = context;
        }

        public PrivateCompany? GetItemById(int itemId)
        {
            return context.PrivateCompanies
                .FirstOrDefault(s => s.PrivateCompanyId == itemId);
        }

        public List<PrivateCompany> GetFilteredItems(Expression<Func<PrivateCompany, bool>> filter)
        {
            return context.PrivateCompanies
                .Where(filter)
                .ToList();
        }

        public int InsertItem(PrivateCompany item)
        {
            context.PrivateCompanies.Add(item);
            context.SaveChanges();
            int createdItemId = item.PrivateCompanyId;
            return createdItemId;
        }

        public void UpdateItem(PrivateCompany item)
        {
            PrivateCompany? dbPrivateCompany = context.PrivateCompanies.Find(item.PrivateCompanyId);

            if (dbPrivateCompany is not null)
            {
                dbPrivateCompany.CompanyName = item.CompanyName;
                dbPrivateCompany.Email = item.Email;
                dbPrivateCompany.HashedPassword = item.HashedPassword;
                dbPrivateCompany.Description = item.Description;

                context.SaveChanges();
            }
        }

        public void DeleteItem(int itemId)
        {
            PrivateCompany? privateCompany = context.PrivateCompanies.Find(itemId);

            if (privateCompany is not null)
            {
                context.PrivateCompanies.Remove(privateCompany);
                context.SaveChanges();
            }
        }

        public List<PrivateCompany> GetAllItems()
        {
            return context.PrivateCompanies.ToList();
        }
    }
}

﻿using LogisticsService.Core.DbModels;
using LogisticsService.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.DAL.Repositories
{
    public class LogisticCompaniesDriverRepository : IDataRepository<LogisticCompaniesDriver>
    {
        private readonly DataContext context;

        public LogisticCompaniesDriverRepository(DataContext context)
        {
            this.context = context;
        }

        public LogisticCompaniesDriver? GetItemById(int itemId)
        {
            return context.LogisticCompaniesDrivers
                .Include(s => s.LogisticCompany)
                .FirstOrDefault(s => s.LogisticCompaniesDriverId == itemId);
        }

        public List<LogisticCompaniesDriver> GetFilteredItems(Expression<Func<LogisticCompaniesDriver, bool>> filter)
        {
            return context.LogisticCompaniesDrivers
                .Include(s => s.LogisticCompany)
                .Where(filter)
                .ToList();
        }

        public int InsertItem(LogisticCompaniesDriver item)
        {
            context.LogisticCompaniesDrivers.Add(item);
            context.SaveChanges();
            int createdItemId = item.LogisticCompaniesDriverId;
            return createdItemId;
        }

        public void UpdateItem(LogisticCompaniesDriver item)
        {
            LogisticCompaniesDriver? dbLogisticCompaniesDriver = 
                context.LogisticCompaniesDrivers
                .Find(item.LogisticCompaniesDriverId);

            if (dbLogisticCompaniesDriver is not null)
            {
                dbLogisticCompaniesDriver.FirstName = item.FirstName;
                dbLogisticCompaniesDriver.LastName = item.LastName;
                dbLogisticCompaniesDriver.HashedPassword = item.HashedPassword;

                context.SaveChanges();
            }
        }

        public void DeleteItem(int itemId)
        {
            LogisticCompaniesDriver? logisticCompaniesDriver = context.LogisticCompaniesDrivers.Find(itemId);

            if (logisticCompaniesDriver is not null)
            {
                context.LogisticCompaniesDrivers.Remove(logisticCompaniesDriver);
                context.SaveChanges();
            }
        }

        public List<LogisticCompaniesDriver> GetAllItems()
        {
            return context.LogisticCompaniesDrivers
                .Include(s => s.LogisticCompany)
                .ToList();
        }
    }
}

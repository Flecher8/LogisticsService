using LogisticsService.Core.DbModels;
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
    public class SmartDeviceRepository : IDataRepository<SmartDevice>
    {
        private readonly DataContext context;

        public SmartDeviceRepository(DataContext context)
        {
            this.context = context;
        }

        public SmartDevice? GetItemById(int itemId)
        {
            return context.SmartDevices
                .Include(s => s.Sensors)
                .Include(s => s.LogisticCompany)
                .FirstOrDefault(s => s.SmartDeviceId == itemId);
        }

        public List<SmartDevice> GetFilteredItems(Expression<Func<SmartDevice, bool>> filter)
        {
            return context.SmartDevices
                .Where(filter)
                .Include(s => s.Sensors)
                .Include(s => s.LogisticCompany)
                .ToList();
        }

        public int InsertItem(SmartDevice item)
        {
            context.SmartDevices.Add(item);
            context.SaveChanges();
            int createdItemId = item.SmartDeviceId;
            return createdItemId;
        }

        public void UpdateItem(SmartDevice item)
        {
            SmartDevice? dbSmartDevice = context.SmartDevices.Find(item.SmartDeviceId);

            if (dbSmartDevice is not null)
            {
                dbSmartDevice.LogisticCompany = item.LogisticCompany;
                dbSmartDevice.NumberOfSensors = item.NumberOfSensors;

                context.SaveChanges();
            }
        }

        public void DeleteItem(int itemId)
        {
            SmartDevice? smartDevice = context.SmartDevices.Find(itemId);

            if (smartDevice is not null)
            {
                context.SmartDevices.Remove(smartDevice);
                context.SaveChanges();
            }
        }

        public List<SmartDevice> GetAllItems()
        {
            return context.SmartDevices
                .Include(s => s.Sensors)
                .Include(s => s.LogisticCompany)
                .ToList();
        }
    }
}

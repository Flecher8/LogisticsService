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
    public class SensorRepository : IDataRepository<Sensor>
    {
        private readonly DataContext context;

        public SensorRepository(DataContext context)
        {
            this.context = context;
        }

        public Sensor? GetItemById(int itemId)
        {
            return context.Sensors
                .Include(s => s.SmartDevice)
                .FirstOrDefault(s => s.SensorId == itemId);
        }

        public List<Sensor> GetFilteredItems(Expression<Func<Sensor, bool>> filter)
        {
            return context.Sensors
                .Where(filter)
                .Include(s => s.SmartDevice)
                .ToList();
        }

        public int InsertItem(Sensor item)
        {
            context.Sensors.Add(item);
            context.SaveChanges();
            int createdItemId = item.SensorId;
            return createdItemId;
        }

        public void UpdateItem(Sensor item)
        {
            Sensor? dbSensor = context.Sensors.Find(item.SensorId);

            if (dbSensor is not null)
            {
                dbSensor.SmartDevice = item.SmartDevice;
                dbSensor.Status = item.Status;

                context.SaveChanges();
            }
        }

        public void DeleteItem(int itemId)
        {
            Sensor? sensor = context.Sensors.Find(itemId);

            if (sensor is not null)
            {
                context.Sensors.Remove(sensor);
                context.SaveChanges();
            }
        }

        public List<Sensor> GetAllItems()
        {
            return context.Sensors
                .Include(s => s.SmartDevice)
                .ToList();
        }
    }
}

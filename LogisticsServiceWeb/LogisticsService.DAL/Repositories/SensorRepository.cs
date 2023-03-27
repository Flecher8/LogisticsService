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
                .FirstOrDefault(s => s.SensorId == itemId);
        }

        public List<Sensor> GetItems(Expression<Func<Sensor, bool>> filter)
        {
            return context.Sensors
                .Where(filter)
                .ToList();
        }

        public void InsertItem(Sensor item)
        {
            context.Sensors.Add(item);
            context.SaveChanges();
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
    }
}

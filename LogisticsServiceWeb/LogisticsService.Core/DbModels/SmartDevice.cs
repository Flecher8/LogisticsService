using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.DbModels
{
    public class SmartDevice
    {
        public int SmartDeviceId { get; set; }
        public int NumberOfSensors { get; set; }
        public LogisticCompany? LogisticCompany { get; set; }
    }
}

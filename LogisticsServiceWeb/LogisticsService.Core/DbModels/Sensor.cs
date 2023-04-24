using LogisticsService.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.DbModels
{
    public class Sensor
    {
        public int SensorId { get; set; }
        public SmartDevice SmartDevice { get; set; } = null!;
        public SensorStatus Status { get; set; }
    }
}

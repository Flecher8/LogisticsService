using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.Dtos
{
    public class SmartDeviceCommunicationDto
    {
        public int SmartDeviceId { get; set; }
        public int SensorId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}

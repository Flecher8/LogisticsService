using LogisticsService.Core.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.Dtos
{
    public class SmartDeviceDto
    {
        public int SmartDeviceId { get; set; }
        public int LogisticCompanyId { get; set; }
        public int NumberOfSensors { get; set; }
    }
}

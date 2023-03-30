using LogisticsService.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.Dtos
{
    public class OrderTrackerDto
    {
        public int OrderId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public UserDataTime UserDataTime { get; set; } = null!;
    }
}

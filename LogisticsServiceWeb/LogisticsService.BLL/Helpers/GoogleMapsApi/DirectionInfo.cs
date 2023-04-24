using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Helpers.GoogleMapsApi
{
    public class DirectionInfo
    {
        public string Distance { get; set; } = string.Empty;
        public int DistanceInMeters { get; set; }
        public string Time { get; set; } = string.Empty;
        public int TimeInSeconds { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Helpers.GoogleMapsApi
{
    public class GeocodedWaypoint
    {
        public string GeocoderStatus { get; set; }
        public string PlaceId { get; set; }
        public List<string> Types { get; set; }
    }
}

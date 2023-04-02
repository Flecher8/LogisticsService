using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Helpers.GoogleMapsApi
{
    public class Step
    {
        public Distance Distance { get; set; }
        public Duration Duration { get; set; }
        public EndLocation EndLocation { get; set; }
        public string HtmlInstructions { get; set; }
        public Polyline Polyline { get; set; }
        public StartLocation StartLocation { get; set; }
        public string TravelMode { get; set; }
        public string Maneuver { get; set; }
    }
}

using LogisticsService.Core.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Helpers.GoogleMapsApi
{
    public class GoogleMapsApiDirectionsParam
    {
        private const string defaultMetric = "metric";
        private const string defaultLanguage = "en";
        public Address StartAddress { get; set; } = null!;
        public Address EndAddress { get; set; } = null!;
        public string Metric { get; set; } = defaultMetric;
        public string Language { get; set; } = defaultLanguage;
    }
}

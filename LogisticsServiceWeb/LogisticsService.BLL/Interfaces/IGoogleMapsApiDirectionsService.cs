using LogisticsService.BLL.Helpers.GoogleMapsApi;
using LogisticsService.Core.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Interfaces
{
    public interface IGoogleMapsApiDirectionsService
    {
        public Task<DirectionInfo> GetGoogleMapsDirectionInfo(GoogleMapsApiDirectionsParam googleMapsApiDirectionsParam);
    }
}

namespace LogisticsService.BLL.Helpers.GoogleMapsApi
{
    public class GoogleMapsDirectionsResponse
    {
        public List<GeocodedWaypoint> GeocodedWaypoints { get; set; }
        public List<Route> Routes { get; set; }
        public string Status { get; set; }
    }
}

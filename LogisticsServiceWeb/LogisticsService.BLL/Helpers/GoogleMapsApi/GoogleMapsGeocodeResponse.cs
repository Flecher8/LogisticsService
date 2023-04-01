namespace LogisticsService.BLL.Helpers.GoogleMapsApi
{
    public class GoogleMapsGeocodeResponse
    {
        public PlusCode plus_code { get; set; }
        public List<Result> results { get; set; }
        public string status { get; set; }
    }
}

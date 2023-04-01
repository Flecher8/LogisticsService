using LogisticsService.BLL.Helpers.GoogleMapsApi;
using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.DbModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LogisticsService.BLL.Services
{
    public class GoogleMapsApiGeocodeService : IGoogleMapsApiGeocodeService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<GoogleMapsApiGeocodeService> _logger;

        private const string googleMapsApiGeocodeUrl = "https://maps.googleapis.com/maps/api/geocode/";
        private const string answearType = "json?";
        private const string resultType = "premise";

        public GoogleMapsApiGeocodeService(
            IConfiguration configuration, 
            ILogger<GoogleMapsApiGeocodeService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> GetAddressAsync(Address address, string language = "en")
        {
            string url = CreateUrl(address.Latitude, address.Longitute, language);

            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                string responseString = await response.Content.ReadAsStringAsync();
                GoogleMapsGeocodeResponse myDeserializedClass = JsonConvert.DeserializeObject<GoogleMapsGeocodeResponse>(responseString);
                return myDeserializedClass.results.FirstOrDefault().formatted_address;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return string.Empty;
        }

        private string GetGoogleMapsApiKey()
        {
            return _configuration.GetSection("GoogleMapsApi:Key").Value;
        }

        private string GetLanguageType(string language)
        {
            const string english = "en";
            const string ukrainian = "uk";
            if (language == english || language == ukrainian)
            {
                return language;
            }
            return english;
        }

        private string FormateDouble(double num)
        {
            return num.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
        }

        private string CreateUrl(double latitude, double longitude, string language = "en")
        {
            string apiKey = GetGoogleMapsApiKey();
            string langType = GetLanguageType(language);
            string latitudeFormatted = FormateDouble(latitude);
            string longitudeFormatted = FormateDouble(longitude);

            string url =
                googleMapsApiGeocodeUrl +
                answearType +
                "latlng=" + latitudeFormatted + "," + longitudeFormatted +
                "&language=" + langType +
                "&result_type=" + resultType +
                "&key=" + apiKey;

            return url;
        }
        
    }
}

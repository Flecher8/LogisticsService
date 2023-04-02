using LogisticsService.BLL.Helpers.GoogleMapsApi;
using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.DbModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Services
{
    public class GoogleMapsApiDirectionsService : IGoogleMapsApiDirectionsService
    {
        private readonly IConfiguration _configuration;

        private readonly ILanguageService _languageService;
        private readonly IFormatterService _formatterService;

        private readonly ILogger<GoogleMapsApiDirectionsService> _logger;

        private const string googleMapsApiDirectionsUrl = "https://maps.googleapis.com/maps/api/directions/";
        private const string answearType = "json?";

        public GoogleMapsApiDirectionsService(
            IConfiguration configuration, 
            ILogger<GoogleMapsApiDirectionsService> logger,
            ILanguageService languageService,
            IFormatterService formatterService)
        {
            _configuration = configuration;
            _logger = logger;
            _languageService = languageService;
            _formatterService = formatterService;
        }

        public async Task<DirectionInfo> GetGoogleMapsDirectionInfo(GoogleMapsApiDirectionsParam googleMapsApiDirectionsParam)
        {
            IsParametersValid(googleMapsApiDirectionsParam);
            string url = CreateUrl(googleMapsApiDirectionsParam);

            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                string responseString = await response.Content.ReadAsStringAsync();
                GoogleMapsDirectionsResponse myDeserializedClass = JsonConvert.DeserializeObject<GoogleMapsDirectionsResponse>(responseString);

                return GetDirectionInfo(myDeserializedClass);

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return new DirectionInfo();
        }

        private string CreateUrl(GoogleMapsApiDirectionsParam googleMapsApiDirectionsParam)
        {
            string apiKey = GetGoogleMapsApiKey();
            string langType = _languageService.GetLanguageType(googleMapsApiDirectionsParam.Language);
            string metric = googleMapsApiDirectionsParam.Metric;
            string origin = GetFormattedCoordinates(googleMapsApiDirectionsParam.StartAddress);
            string destination = GetFormattedCoordinates(googleMapsApiDirectionsParam.EndAddress);

            string url = googleMapsApiDirectionsUrl +
                answearType +
                "origin=" + origin +
                "&destination=" + destination +
                "&unit=" + metric +
                "&language=" + langType +
                "&key=" + apiKey;
            return url;
        }

        private string GetFormattedCoordinates(Address address)
        {
            return _formatterService.FormateNumberToInvariantCulture(address.Latitude) + 
                "," + 
                _formatterService.FormateNumberToInvariantCulture(address.Longitute);
        }

        private bool IsParametersValid(GoogleMapsApiDirectionsParam googleMapsApiDirectionsParam)
        {
            if(!IsMetricValid(googleMapsApiDirectionsParam.Metric))
            {
                throw new ArgumentOutOfRangeException("Metric is not valid");
            }
            return true;
        }

        private bool IsMetricValid(string metric)
        {
            if(metric == "metric" || metric == "imperial")
            {
                return true;
            }
            return false;
        }

        private DirectionInfo GetDirectionInfo(GoogleMapsDirectionsResponse googleMapsDirectionsResponse)
        {
            DirectionInfo directionInfo = new DirectionInfo();
            directionInfo.Distance = 
                googleMapsDirectionsResponse
                .Routes.FirstOrDefault()
                .Legs.FirstOrDefault()
                .Distance
                .Text;

            directionInfo.DistanceInMeters =
                googleMapsDirectionsResponse
                .Routes.FirstOrDefault()
                .Legs.FirstOrDefault()
                .Distance
                .Value;
            
            directionInfo.Time =
                googleMapsDirectionsResponse
                .Routes.FirstOrDefault()
                .Legs.FirstOrDefault()
                .Duration
                .Text;

            directionInfo.TimeInSeconds =
                googleMapsDirectionsResponse
                .Routes.FirstOrDefault()
                .Legs.FirstOrDefault()
                .Duration
                .Value;

            return directionInfo;
        }

        private string GetGoogleMapsApiKey()
        {
            return _configuration.GetSection("GoogleMapsApi:Key").Value;
        }

    }
}

using mobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace mobile.Services
{
    public class GeolocationService : ServerService
    {
        const string geolocationApi = "Geocode?language=";
        public async Task<string> GetOrderAddress(Address address, string language = "en")
        {
            PropertiesService propertiesService = new PropertiesService();
            HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    SecurityTokenStartTemplate,
                propertiesService.GetProperty(UserTokenPath).ToString()
                );

            StringContent content = new StringContent(JsonConvert.SerializeObject(address), Encoding.UTF8, RequestMediaType);

            HttpResponseMessage response = await HttpClient.PostAsync(geolocationApi + language, content);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return string.Empty;
            }

            HttpContent responseContent = response.Content;
            string fullAddress = JsonConvert.DeserializeObject<string>(await responseContent.ReadAsStringAsync());

            return fullAddress;
        }
    }
}

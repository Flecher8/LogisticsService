using mobile.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace mobile.Services
{
    public class LogisticCompaniesDriversService : ServerService
    {
        private string logisticCompaniesDriversApi = "LogisticCompaniesDrivers";
        private string getDriverByEmailApi = "/email/";
        public async Task<int> GetDriverIdByEmail(string email, string jwtToken)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(SecurityTokenStartTemplate, jwtToken);

            HttpResponseMessage response = await HttpClient.GetAsync(logisticCompaniesDriversApi + getDriverByEmailApi + email);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return 0;
            }

            HttpContent responseContent = response.Content;
            LogisticCompaniesDriver driver = JsonConvert.DeserializeObject<LogisticCompaniesDriver>(await responseContent.ReadAsStringAsync());

            return driver.LogisticCompaniesDriverId;
        }
    }
}

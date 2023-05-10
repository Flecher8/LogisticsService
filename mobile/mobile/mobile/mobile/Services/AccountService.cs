using mobile.Services.Interfaces;
using mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;

namespace mobile.Services
{
    public class AccountService : ServerService, IAccountService
    {
        private const string LoginAddress = "Authentication/Login";

        public async Task<bool> LoginAsync(string email, string password)
        {
            LoginModel LoginModel = new LoginModel(email, password);


            StringContent content = new StringContent(JsonConvert.SerializeObject(LoginModel), Encoding.UTF8, RequestMediaType);

            HttpResponseMessage response = await HttpClient.PostAsync(LoginAddress, content);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            HttpContent responseContent = response.Content;
            LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(await responseContent.ReadAsStringAsync());
            Console.WriteLine(loginResponse.UserType.ToString());
            //await PropertiesService.SetProperty(UserTokenPath, loginResponse.Token);
            //await PropertiesService.SetProperty(UserIdPath, loginResponse.UserId);

            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}

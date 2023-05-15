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
            try
            {


                HttpResponseMessage response = await SendLoginData(email, password);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return false;
                }

                HttpContent responseContent = response.Content;
                LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(await responseContent.ReadAsStringAsync());

                if (!isUserDriver(loginResponse.UserType))
                {
                    return false;
                }

                int driverId = await GetUserId(email, loginResponse.Token);

                if (driverId == 0)
                {
                    return false;
                }

                await PropertiesService.SetProperty(UserTokenPath, loginResponse.Token);
                await PropertiesService.SetProperty(UserIdPath, driverId);

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        private async Task<HttpResponseMessage> SendLoginData(string email, string password)
        {
            try
            {
                LoginModel LoginModel = new LoginModel(email, password);

                StringContent content = new StringContent(JsonConvert.SerializeObject(LoginModel), Encoding.UTF8, RequestMediaType);

                HttpResponseMessage response = await HttpClient.PostAsync(LoginAddress, content);

                return response;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<int> GetUserId(string email, string token)
        {
            LogisticCompaniesDriversService logisticCompaniesDriversService = new LogisticCompaniesDriversService();
            return await logisticCompaniesDriversService.GetDriverIdByEmail(email, token);
        }

        private bool isUserDriver(string userType)
        {
            const string userTypeDriver = "LogisticCompanyDriver";
            return userType == userTypeDriver;
        }
    }
}

using mobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace mobile.Services
{
    public class ServerService : IServerService
    {
        public const string ServerAddress = "https://othertealbox27.conveyor.cloud/api/";

        protected const string UserTokenPath = "userToken";
        protected const string UserIdPath = "userId";
        protected const string UserTypePath = "userType";

        protected const string AcceptRequestHeader = "Accept";
        protected const string AuthorizationRequestHeader = "Authorization";
        protected const string SecurityTokenStartTemplate = "Bearer";
        protected const string RequestMediaType = "application/json";

        public PropertiesService PropertiesService { get; set; }

        public HttpClient HttpClient { get; set; }

        public ServerService()
        {
            HttpClient = SetUpHttpClient();
            PropertiesService = new PropertiesService();
        }

        public HttpClient SetUpHttpClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ServerAddress);
            client.DefaultRequestHeaders.Add(AcceptRequestHeader, RequestMediaType);
            return client;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace MobileClient.Shared.Identity
{
    public class IdentityManager
    {
        IdentityManagerSettings _settings;

        private static HttpClient _httpClient;

        public IdentityManager(IdentityManagerSettings settings)
        {
            _settings = settings;
        }

        static IdentityManager()
        {
            _httpClient = new HttpClient();
        }

        public SecurityToken SignIn(string username, string password)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _settings.TokenEndpoint);

            var keyValues = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("client_id", _settings.ClientID),
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            };

            request.Content = new FormUrlEncodedContent(keyValues);

            var bearerResult = _httpClient.SendAsync(request).Result;
            var bearerData = bearerResult.Content.ReadAsStringAsync().Result;

            SecurityToken token = JsonConvert.DeserializeObject<SecurityToken>(bearerData);

            return token;
        }
    }
}
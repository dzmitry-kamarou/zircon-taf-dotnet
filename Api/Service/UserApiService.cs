using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Commons;
using Model.Account;

namespace Api.Service
{
    public class UserApiService
    {
        private const string UserEndpoint = "user";
        private const string LoginEndpoint = "login";
        private const string FindEndpoint = "find";
        private const string AuthEndpoint = "auth";
        private readonly HttpClient _client;

        public UserApiService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(ServiceConfig.BaseApiUri);
        }

        public async Task<string> LoginAccount(Account account)
        {
            var endpoint = $"api/{UserEndpoint}/{LoginEndpoint}";
            var map = new Dictionary<string, object>
            {
                {"email", account.Email},
                {"password", account.Password}
            };
            var payload = JsonSerializer.Serialize(map);
            var response = await _client.PostAsync(endpoint, ProceedPayload(payload));
            var message = JsonSerializer
                .Deserialize<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
            if (message == null) return null;
            account.Token = message["token"];
            return message["token"] == null ? string.Empty : message["token"];
        }

        public async Task<Account> FindAccount(Account account)
        {
            var endpoint = $"api/{UserEndpoint}/{FindEndpoint}?email={account.Email}";
            var response = await _client.GetAsync(endpoint);
            try
            {
                var messageDict = JsonSerializer
                    .Deserialize<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
                if (messageDict != null && messageDict["message"].Equals("There is no such user"))
                {
                    return null;
                }
            }
            catch (JsonException)
            {
                return JsonSerializer.Deserialize<Account>(await response.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task<string> AuthAccount(Account account)
        {
            var endpoint = $"api/{UserEndpoint}/{AuthEndpoint}";
            _client.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue("Bearer", account.Token);
            var response = await _client.GetAsync(endpoint);
            var dict = JsonSerializer
                .Deserialize<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
            try
            {
                return dict?["message"];
            }
            catch (KeyNotFoundException)
            {
                return dict?["token"];
            }
        }

        private static StringContent ProceedPayload(string content)
        {
            return new StringContent(content, Encoding.UTF8, "application/json");
        }
    }
}
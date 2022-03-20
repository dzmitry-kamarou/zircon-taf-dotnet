using System;
using System.Collections.Generic;
using System.Net.Http;
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
        private readonly HttpClient _client;

        public UserApiService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(ServiceConfig.BaseApiUri);
        }

        public async Task<string> LoginAccount(Account account)
        {
            var map = new Dictionary<string, object>
            {
                {"email", account.Email},
                {"password", account.Password}
            };
            var payload = JsonSerializer.Serialize(map);
            var response = await _client.PostAsync($"api/{UserEndpoint}/{LoginEndpoint}", ProceedPayload(payload));
            var message = JsonSerializer
                .Deserialize<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
            return message?["token"] == null ? string.Empty : message["token"];
        }

        private static StringContent ProceedPayload(string content)
        {
            return new StringContent(content, Encoding.UTF8, "application/json");
        }
    }
}
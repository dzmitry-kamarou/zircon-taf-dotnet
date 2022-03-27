using System.Text.Json.Serialization;

namespace Model.Account
{
    public class Account
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
        
        [JsonPropertyName("password")]
        public string Password { get; set; }
        
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
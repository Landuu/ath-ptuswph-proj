using System.Text.Json.Serialization;

namespace ptuswph_backend.Models
{
    public class User
    {
        public int Id { get; set; }

        [JsonPropertyName("login")]
        public string Login { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}

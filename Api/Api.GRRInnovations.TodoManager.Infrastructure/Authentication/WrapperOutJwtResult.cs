using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Authentication
{
    public class WrapperOutJwtResult
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expire")]
        public double Expire { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}

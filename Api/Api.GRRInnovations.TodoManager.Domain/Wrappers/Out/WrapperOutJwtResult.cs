using Api.GRRInnovations.TodoManager.Domain.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Api.GRRInnovations.TodoManager.Domain.Wrappers.Out
{
    public class WrapperOutJwtResult : WrapperBase<JwtResultModel>
    {
        [JsonPropertyName("access_token")]
        public string AccessToken
        {
            get => Data.AccessToken;
            set => Data.AccessToken = value;
        }

        [JsonPropertyName("expire")]
        public double Expire
        {
            get => Data.Expire;
            set => Data.Expire = value;
        }

        [JsonPropertyName("type")]
        public string Type
        {
            get => Data.Type;
            set => Data.Type = value;
        }
    }
}

using Api.GRRInnovations.TodoManager.Domain.Models;
using Newtonsoft.Json;

namespace Api.GRRInnovations.TodoManager.Domain.Wrappers.Out
{
    public class WrapperOutJwtResult : WrapperBase<JwtResultModel>
    {
        [JsonProperty("access_token")]
        public string AccessToken
        {
            get => Data.AccessToken;
            set => Data.AccessToken = value;
        }

        [JsonProperty("expire")]
        public double Expire
        {
            get => Data.Expire;
            set => Data.Expire = value;
        }

        [JsonProperty("type")]
        public string Type
        {
            get => Data.Type;
            set => Data.Type = value;
        }
    }
}

using Api.GRRInnovations.TodoManager.Domain.Wrappers;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.Out;
using Api.GRRInnovations.TodoManager.Interfaces.Authentication;
using Api.GRRInnovations.TodoManager.Interfaces.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Security.Authentication
{
    public class WrapperOutJwtResult : WrapperBase<IJwtResultModel, WrapperOutJwtResult>
    {
        public WrapperOutJwtResult() : base(null) { }

        public WrapperOutJwtResult(IJwtResultModel data) : base(data) { }

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

        public new static async Task<WrapperOutJwtResult> From(IJwtResultModel model)
        {
            var wrapper = new WrapperOutJwtResult();
            await wrapper.Populate(model).ConfigureAwait(false);

            return wrapper;
        }
    }
}

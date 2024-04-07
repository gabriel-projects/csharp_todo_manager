using Newtonsoft.Json;
namespace Api.GRRInnovations.TodoManager.Domain.Wrappers.Out
{
    public class WrapperOutError
    {
        [JsonProperty("error")]
        public string Title { get; set; } = "Erro";

        [JsonProperty("detail")]
        public string Message { get; set; }
    }
}

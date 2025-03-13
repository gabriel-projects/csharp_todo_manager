using Newtonsoft.Json;
using System.Text.Json.Serialization;
namespace Api.GRRInnovations.TodoManager.Domain.Wrappers.Out
{
    public class WrapperOutError
    {
        [JsonPropertyName("error")]
        public string Title { get; set; } = "Error";

        [JsonPropertyName("detail")]
        public string Message { get; set; }

        public WrapperOutError(string message)
        {
            Message = message;
        }
    }
}

using Api.GRRInnovations.TodoManager.Interfaces.Enuns;
using Api.GRRInnovations.TodoManager.Interfaces.Models;
using System.Net;
using System.Text.Json.Serialization;

namespace Api.GRRInnovations.TodoManager.Domain.Wrappers.In
{
    public class WrapperInTask<TTask>: WrapperBase<TTask, WrapperInTask<TTask>>
        where TTask : ITaskModel
    {
        [JsonPropertyName("title")]
        public string Title
        {
            get => Data.Title;
            set => Data.Title = value;
        }

        [JsonPropertyName("description")]
        public string Description
        {
            get => Data.Description;
            set => Data.Description = value;
        }

        [JsonPropertyName("recurrent")]
        public bool Recurrent
        {
            get => Data.Recurrent;
            set => Data.Recurrent = value;
        }

        [JsonPropertyName("priority")]
        public string Priority
        {
            get => Data.Priority.ToString();
            set
            {
                if (Enum.TryParse(value, true, out EPriorityTask action))
                {
                    Data.Priority = action;
                }
            }
        }

        [JsonPropertyName("category_name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CategoryName { get; set; }
    }
}

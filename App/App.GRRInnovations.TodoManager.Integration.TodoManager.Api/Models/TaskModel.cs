using App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Enums;
using App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Interfaces;
using System.Text.Json.Serialization;

namespace App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Models
{
    public class TaskModel : ITaskModel
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("recurrent")]
        public bool Recurrent { get; set; }

        [JsonPropertyName("status")]
        public EStatusTask Status { get; set; }

        [JsonPropertyName("priority")]
        public EPriorityTask Priority { get; set; }
    }
}

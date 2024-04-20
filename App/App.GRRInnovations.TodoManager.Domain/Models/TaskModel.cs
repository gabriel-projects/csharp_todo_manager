using App.GRRInnovations.TodoManager.Interfaces.Enuns;
using App.GRRInnovations.TodoManager.Interfaces.Models;
using System.Text.Json.Serialization;

namespace App.GRRInnovations.TodoManager.Domain.Models
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

using App.GRRInnovations.TodoManager.Interfaces.ApiTodoManagerCommunic.Models;
using App.GRRInnovations.TodoManager.Interfaces.Enuns;
using System.Text.Json.Serialization;

namespace App.GRRInnovations.TodoManager.Domain.ApiTodoManagerCommunic.Models
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

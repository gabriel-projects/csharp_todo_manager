using Api.GRRInnovations.TodoManager.Interfaces.Enuns;
using Api.GRRInnovations.TodoManager.Interfaces.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Api.GRRInnovations.TodoManager.Domain.Wrappers.In
{
    public class WrapperInTask<TTask, TCategory>: WrapperBase<TTask, WrapperInTask<TTask, TCategory>>
        where TTask : ITaskModel
        where TCategory : ICategoryModel
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

        [JsonPropertyName("start")]
        public DateTime Start
        {
            get => Data.Start.ToUniversalTime();
            set => Data.Start = value;
        }

        [JsonPropertyName("end")]
        public DateTime End
        {
            get => Data.End.ToUniversalTime();
            set => Data.End = value;
        }

        [JsonPropertyName("priority")]
        public EPriorityTask Priority
        {
            get => Data.Priority;
            set => Data.Priority = value;
        }
    }
}

using Api.GRRInnovations.TodoManager.Interfaces.Models;
using System.Text.Json.Serialization;

namespace Api.GRRInnovations.TodoManager.Domain.Wrappers.In
{
    public class WrapperInTaskRecurrence<TTaskRecurrence> : WrapperBase<TTaskRecurrence, WrapperInTaskRecurrence<TTaskRecurrence>> 
        where TTaskRecurrence : ITaskRecurrence
    {
        [JsonPropertyName("start")]
        public DateTime Start
        {
            get => Data.Start;
            set => Data.Start = value;
        }

        [JsonPropertyName("end")]
        public DateTime? End
        {
            get => Data.End;
            set => Data.End = value;
        }
    }
}

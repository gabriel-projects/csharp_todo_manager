
using Api.GRRInnovations.TodoManager.Interfaces.Enuns;

namespace Api.GRRInnovations.TodoManager.Interfaces.Models
{
    public interface ITaskRecurrence : IBaseModel
    {
        /// <summary>
        /// Dias da semana que  ira se repetir
        /// </summary>
        public EDayOfWeek? DayOfWeek { get; set; }

        /// <summary>
        /// se diario tiver mais de um dia não pode ser semanal ou diario
        /// </summary>
        public RecurrenceType RecurrenceType { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public ITaskModel Task { get; set; }
    }
}

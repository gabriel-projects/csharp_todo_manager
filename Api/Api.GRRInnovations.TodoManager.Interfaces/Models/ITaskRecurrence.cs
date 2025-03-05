
using Api.GRRInnovations.TodoManager.Interfaces.Enuns;

namespace Api.GRRInnovations.TodoManager.Interfaces.Models
{
    public interface ITaskRecurrence : IBaseModel
    {
        /// <summary>
        /// Dias da semana que  ira se repetir
        /// </summary>
        public EDayOfWeek? DayOfWeek { get; set; }
        public RecurrenceType RecurrenceType { get; set; }
        public DateTime Start { get; set; }

        /// <summary>
        /// se nulo, irá se repetir sempre
        /// </summary>
        public DateTime? End { get; set; }
        public ITaskModel Task { get; set; }
    }
}

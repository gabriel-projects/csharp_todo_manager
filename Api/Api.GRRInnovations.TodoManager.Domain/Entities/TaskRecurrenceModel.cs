using Api.GRRInnovations.TodoManager.Interfaces.Enuns;
using Api.GRRInnovations.TodoManager.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Domain.Entities
{
    public class TaskRecurrenceModel : BaseModel, ITaskRecurrence
    {
        public EDayOfWeek? DayOfWeek { get; set; }
        public RecurrenceType RecurrenceType { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set ; }
        public Guid TaskUid { get; set; }
        public TaskModel DbTask { get; set; }
        public ITaskModel Task
        {
            get => DbTask;
            set => DbTask = value as TaskModel;
        }
    }
}

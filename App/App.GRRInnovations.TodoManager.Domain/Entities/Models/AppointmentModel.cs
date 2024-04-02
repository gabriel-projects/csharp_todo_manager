using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.GRRInnovations.TodoManager.Domain.Entities.Models
{
    public class AppointmentModel : IAppointment
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Completed { get; set; }
        public bool Recurrent { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

using App.GRRInnovations.TodoManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.GRRInnovations.TodoManager.Domain.Repositories
{
    public interface IAppointmentRepository
    {
        Task<List<IAppointment>> Appointments();
    }
}

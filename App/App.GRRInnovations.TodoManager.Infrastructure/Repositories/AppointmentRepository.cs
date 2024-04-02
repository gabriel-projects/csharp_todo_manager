using App.GRRInnovations.TodoManager.Domain.Entities;
using App.GRRInnovations.TodoManager.Domain.Entities.Models;
using App.GRRInnovations.TodoManager.Domain.Repositories;

namespace App.GRRInnovations.TodoManager.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public async Task<List<IAppointment>> Appointments()
        {
            var appointments = new List<IAppointment>()
            {
                new AppointmentModel
                {
                    DueDate = DateTime.Now,
                    Description = "Ir cortar o cabelo no will Salon",
                    Recurrent = false,
                    Title = "Cabeleiro",
                }
            };
            return await Task.FromResult(appointments);
        }
    }
}

using App.GRRInnovations.TodoManager.Domain.Entities;
using App.GRRInnovations.TodoManager.Domain.Repositories;

namespace App.GRRInnovations.TodoManager.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public async Task<List<AppointmentData>> Appointments()
        {
            var appointments = new List<AppointmentData>()
            {
                new AppointmentData
                {
                    DueDate = DateTime.Now,
                    Description = "Ir cortar o cabelo no will Salon",
                    Recurrent = false,
                    Title = "Cabeleiro",
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "Funny"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "teste"
                }
                ,
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "teste"
                }
                ,
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "teste"
                }
                ,
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "teste"
                }
                ,
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "teste"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "teste"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "teste"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "teste"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "teste"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "teste"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "teste"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "teste"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "teste"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "teste"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "teste"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "teste"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "teste"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "teste"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "teste"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "teste"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "go to house bluego to house bluego to house bluego to house bluego to house bluego to house bluego to house blue",
                    CreatedAt = DateTime.Now,
                    Title = "teste"
                },
                new AppointmentData
                {
                    Completed = false,
                    DueDate = DateTime.Now,
                    Description = "AAAAAA",
                    CreatedAt = DateTime.Now,
                    Title = "OLÁAAAAAAA"
                }
            };
            return await Task.FromResult(appointments);
        }
    }
}

using App.GRRInnovations.TodoManager.Domain.Repositories;
using App.GRRInnovations.TodoManager.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.GRRInnovations.TodoManager.ViewModels
{
    public partial class TodayViewModel : BaseViewModel
    {
        private IAppointmentRepository AppointmentRepository { get; set; }

        public TodayViewModel(IAppointmentRepository appointmentRepository)
        {
            AppointmentRepository = appointmentRepository;
        }

        [ObservableProperty]
        private List<AppointmentModel> appointments = new List<AppointmentModel>();

        [ObservableProperty]
        private bool isRefreshing;

        [RelayCommand]
        public async Task Refresh()
        {
            await Task.Delay(5000);

            IsRefreshing = false;
        }

        [RelayCommand]
        public async Task Appearing()
        {
            try
            {
                var appointmentsRepo = await AppointmentRepository.Appointments();

                foreach (var app in appointmentsRepo)
                {
                    Appointments.Add(new AppointmentModel
                    {
                        Completed = app.Completed,
                        CreatedAt = app.CreatedAt,
                        Description = app.Description,
                        DueDate = app.DueDate,
                        EndDate = app.EndDate,
                        Id = app.Id,
                        Recurrent = app.Recurrent,
                        StartDate = app.StartDate,
                        Title = app.Title
                    });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        [RelayCommand]
        void Disappearing()
        {
            try
            {

                // DoSomething

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
    }
}

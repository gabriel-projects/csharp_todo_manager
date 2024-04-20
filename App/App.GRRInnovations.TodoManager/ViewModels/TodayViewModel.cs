using App.GRRInnovations.TodoManager.Domain.ApiTodoManagerCommunic.Models;
using App.GRRInnovations.TodoManager.Domain.Repositories;
using App.GRRInnovations.TodoManager.Interfaces.Enuns;
using App.GRRInnovations.TodoManager.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.GRRInnovations.TodoManager.ViewModels
{
    public partial class TodayViewModel : BaseViewModel
    {
        private ITaskRepository AppointmentRepository { get; set; }

        public TodayViewModel(ITaskRepository appointmentRepository)
        {
            AppointmentRepository = appointmentRepository;
        }

        [ObservableProperty]
        private List<ITaskModel> appointments = new List<ITaskModel>();

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
                    //var model = app as TaskModel;

                    Appointments.Add(app);
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

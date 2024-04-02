using App.GRRInnovations.TodoManager.Domain.Entities;
using App.GRRInnovations.TodoManager.Domain.Repositories;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace App.GRRInnovations.TodoManager.ViewModels
{
    public partial class TodayViewModel : BaseViewModel
    {
        private IAppointmentRepository AppointmentRepository { get; set; }

        public TodayViewModel(IAppointmentRepository appointmentRepository)
        {
            AppointmentRepository = appointmentRepository;
        }


        ObservableCollection<IAppointment> Appointments { get; set; }

        [RelayCommand]
        public async Task Appearing()
        {
            try
            {
                var appointments = await AppointmentRepository.Appointments();

                Appointments = new ObservableCollection<IAppointment>(appointments);

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

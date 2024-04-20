using App.GRRInnovations.TodoManager.Domain.Repositories;
using App.GRRInnovations.TodoManager.Interfaces.Models;
using App.GRRInnovations.TodoManager.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.GRRInnovations.TodoManager.ViewModels
{
    public partial class TodayViewModel : BaseViewModel
    {
        private ITaskRepository TaskRepository { get; set; }

        public TodayViewModel(ITaskRepository taskRepository)
        {
            TaskRepository = taskRepository;
        }

        [ObservableProperty]
        private List<ITaskModel> tasks = new List<ITaskModel>();

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
                var tasks = await TaskRepository.Appointments();

                Tasks.AddRange(tasks);
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

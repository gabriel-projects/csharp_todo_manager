using App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Enums;
using App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Interfaces.Models;
using App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Interfaces.Services;
using App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Services;
using App.GRRInnovations.TodoManager.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.GRRInnovations.TodoManager.ViewModels
{
    public partial class TodayViewModel : BaseViewModel
    {
        private ITaskService TaskService { get; set; }

        public TodayViewModel(ITaskService taskService)
        {
            TaskService = taskService;
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
                var resultTasks = await TaskService.GetTasks();
                if (resultTasks.ResultType == EResult.Sucess)
                {
                    Tasks.AddRange(resultTasks.Value);
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

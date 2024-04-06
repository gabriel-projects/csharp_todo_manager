using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.GRRInnovations.TodoManager.ViewModels
{
    public partial class SearchViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string textSearch;

        [RelayCommand]
        private void OnTextSearchChangedFilterList()
        {
            Console.WriteLine(TextSearch);
        }
    }
}

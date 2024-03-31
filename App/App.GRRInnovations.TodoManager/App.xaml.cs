using App.GRRInnovations.TodoManager.ViewModels;
using App.GRRInnovations.TodoManager.Views;

namespace App.GRRInnovations.TodoManager
{
    public partial class App : Application
    {
        public App(TabbedPageHomeViewModel viewModel)
        {
            InitializeComponent();

            MainPage = new TabbedPageHomeView(viewModel);
        }
    }
}

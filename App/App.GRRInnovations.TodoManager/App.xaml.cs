using App.GRRInnovations.TodoManager.ViewModels;
using App.GRRInnovations.TodoManager.Views;

namespace App.GRRInnovations.TodoManager
{
    public partial class App : Application
    {
        public App(TabbedPageHomeViewModel tabbedPageViewModel, LoginViewModel loginViewModel)
        {
            InitializeComponent();

            //buscar token do cache
            //validar token e usuario


            MainPage = new LoginView(loginViewModel);
        }
    }
}

using App.GRRInnovations.TodoManager.Views;

namespace App.GRRInnovations.TodoManager
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new TabbedPageHomeView();
        }
    }
}

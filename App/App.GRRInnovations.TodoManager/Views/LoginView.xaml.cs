using App.GRRInnovations.TodoManager.ViewModels;

namespace App.GRRInnovations.TodoManager.Views;

public partial class LoginView : ContentPage
{
	public LoginView(LoginViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
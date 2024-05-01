using App.GRRInnovations.TodoManager.ViewModels;

namespace App.GRRInnovations.TodoManager.Views;

public partial class SigninSignupView : ContentPage
{
	public SigninSignupView(SigninSignupViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
using App.GRRInnovations.TodoManager.ViewModels;

namespace App.GRRInnovations.TodoManager.Views;

public partial class TodayView : ContentPage
{
	public TodayView(TodayViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}
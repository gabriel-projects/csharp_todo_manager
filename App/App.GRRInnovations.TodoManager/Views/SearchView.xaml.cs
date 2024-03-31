using App.GRRInnovations.TodoManager.ViewModels;

namespace App.GRRInnovations.TodoManager.Views;

public partial class SearchView : ContentPage
{
	public SearchView(SearchViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
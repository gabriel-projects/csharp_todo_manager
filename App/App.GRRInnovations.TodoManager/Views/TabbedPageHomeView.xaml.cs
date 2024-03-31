using App.GRRInnovations.TodoManager.Models;
using App.GRRInnovations.TodoManager.Providers;
using App.GRRInnovations.TodoManager.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace App.GRRInnovations.TodoManager.Views;

public partial class TabbedPageHomeView
{
	public TabbedPageHomeView(TabbedPageHomeViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;

        AddTab<TodayView>();
        AddTab<SearchView>();
        AddTab<FutureView>();
        AddTab<BundleView>();
    }

    private void AddTab<T>() where T : Page
    {
        var page = DepencyInjectionServiceProvider.GetService<T>();
        Children.Add(page);
    }
}
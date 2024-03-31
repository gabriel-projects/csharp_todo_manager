using App.GRRInnovations.TodoManager.Models;
using App.GRRInnovations.TodoManager.ViewModels;
using App.GRRInnovations.TodoManager.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
using System.Reflection;

namespace App.GRRInnovations.TodoManager
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder.ConfigureSyncfusionCore();
            builder.ConfigureJsonSettings();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<TodayViewModel>();
            builder.Services.AddSingleton<TodayView>();

            builder.Services.AddSingleton<TabbedPageHomeViewModel>();
            builder.Services.AddSingleton<TabbedPageHomeView>();

            builder.Services.AddSingleton<SearchView>();
            builder.Services.AddSingleton<SearchViewModel>();

            builder.Services.AddSingleton<FutureView>();
            builder.Services.AddSingleton<FutureViewModel>();

            builder.Services.AddSingleton<BundleView>();
            builder.Services.AddSingleton<BundleViewModel>();

            return builder.Build();
        }

        private static void ConfigureJsonSettings(this MauiAppBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var solutionName = assembly.GetName().Name;

            var stream = assembly.GetManifestResourceStream($"{solutionName}.appsettings.json");

            var config = new ConfigurationBuilder()
                        .AddJsonStream(stream)
                        .Build();

            builder.Configuration.AddConfiguration(config);

            var settings = config.GetRequiredSection("Settings").Get<Setting>();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(settings.SyncfusionLicenseRegisterKey);
        }
    }
}

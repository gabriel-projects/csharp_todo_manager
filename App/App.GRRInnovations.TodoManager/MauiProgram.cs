using App.GRRInnovations.TodoManager.Models;
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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
#if DEBUG
    		builder.Logging.AddDebug();
#endif

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

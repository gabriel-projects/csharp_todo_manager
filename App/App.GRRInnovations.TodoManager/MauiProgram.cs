﻿using App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Interfaces.Services;
using App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Services;
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

            builder.Services.AddScoped<TodayViewModel>();
            builder.Services.AddScoped<TodayView>();

            builder.Services.AddScoped<TabbedPageHomeViewModel>();
            builder.Services.AddScoped<TabbedPageHomeView>();

            builder.Services.AddScoped<SearchView>();
            builder.Services.AddScoped<SearchViewModel>();

            builder.Services.AddScoped<FutureView>();
            builder.Services.AddScoped<FutureViewModel>();

            builder.Services.AddScoped<BundleView>();
            builder.Services.AddScoped<BundleViewModel>();

            builder.Services.AddScoped<BundleView>();
            builder.Services.AddScoped<BundleViewModel>();

            builder.Services.AddScoped<SigninSignupView>();
            builder.Services.AddScoped<SigninSignupViewModel>();

            builder.Services.AddScoped<LoginView>();
            builder.Services.AddScoped<LoginViewModel>();

            builder.Services.AddScoped<ITaskService, TaskService>();

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

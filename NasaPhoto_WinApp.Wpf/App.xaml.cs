using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NasaPhoto_WinApp.Application.Interfaces;
using NasaPhoto_WinApp.Application.UseCases;
using NasaPhoto_WinApp.Infrastructure.Configuration;
using NasaPhoto_WinApp.Infrastructure.Repositories;
using NasaPhoto_WinApp.Wpf.ViewModels;
using System.Windows;

namespace NasaPhoto_WinApp.Wpf;

public partial class App : System.Windows.Application
{
    public static IServiceProvider ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var services = new ServiceCollection();

        services.Configure<NasaApiSettings>(
            configuration.GetSection("NasaApi"));

        services.AddSingleton<IApodRepository, ApodRepository>();
        services.AddSingleton<GetApodsByMonthUseCase>();
        services.AddSingleton<MainViewModel>();

        ServiceProvider = services.BuildServiceProvider();

        var mainWindow = new MainWindow
        {
            DataContext = ServiceProvider.GetRequiredService<MainViewModel>()
        };

        mainWindow.Show();
    }
}
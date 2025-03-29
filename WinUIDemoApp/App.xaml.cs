using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using Serilog;
using System;

namespace WinUIDemoApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    private IHost? Host { get; set; }

    private Window? Window { get; set; }

    public static T GetService<T>() where T : class
    {
        if ((App.Current as App)?.Host?.Services.GetService(typeof(T)) is T service)
        {
            return service;
        }

        throw new InvalidOperationException($"Service of type {typeof(T).Name} not found.");
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<ShellViewModel>();
            })
            .UseSerilog((context, services, configuration) => configuration
                //.WriteTo.Console()
                //.WriteTo.File(
                //    path: "D:\\Projects\\AndrewKeepCoding\\WinUIDemos\\WinUIDemoApp\\.log",
                //    rollingInterval: RollingInterval.Day)
                .WriteTo.Debug())
            .Build();

        Window = new MainWindow();
        Window.Activate();

        Serilog.Log.Information("App.OnLaunched");
    }
}

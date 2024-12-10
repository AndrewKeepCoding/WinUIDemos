using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Documents;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WinUIDemoApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    private IHost? Host { get; set; }

    private Window? Window { get; set; }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
            })
            .Build();

        var splashScreen = new SplashScreen(typeof(MainWindow))
        {
            Function = async () =>
            {
                await GetMethod();
            }
        };

        splashScreen.Completed += (s, e) => Window = e;
    }

    private async Task GetMethod()
    {
        await Task.Delay(5000);
    }
}
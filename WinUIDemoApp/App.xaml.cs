using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;

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

        Window = new MainWindow();
        Window.Activate();
    }
}

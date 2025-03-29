using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace WinUIDemoApp;

public partial class ShellViewModel(ILogger<ShellViewModel> logger) : ObservableObject
{
    private readonly ILogger<ShellViewModel> _logger = logger;

    [ObservableProperty]
    public partial int Count { get; set; }

    [RelayCommand]
    private void Increment()
    {
        Count++;
        _logger.LogInformation("Incremented count to {Count}.", Count);
    }
}

public sealed partial class Shell : Page
{
    public Shell()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    public ShellViewModel ViewModel { get; } = App.GetService<ShellViewModel>();

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        Serilog.Log.Information("Shell.OnLoaded");
    }
}

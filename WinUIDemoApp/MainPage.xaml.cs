using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;

namespace WinUIDemoApp;

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty]
    private string _someText = string.Empty;


    [RelayCommand]
    private void ClearSomeText() => SomeText = string.Empty;
}

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();
        //DataContext = new MainPageViewModel();
    }

    public MainPage(MainPageViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
    }

    public MainPageViewModel ViewModel { get; } = new();
}

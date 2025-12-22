using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace WinUIDemoApp;

public partial class ShellViewModel : ObservableObject
{
}

public sealed partial class Shell : Page
{
    public Shell()
    {
        InitializeComponent();
        RequestedTheme = ElementTheme.Light;
    }

    public ShellViewModel ViewModel { get; } = new();

    private void Button_Click(object sender, RoutedEventArgs e)
    {

        var dialog = new ContentDialog
        {
            Title = "Hello",
            XamlRoot = this.XamlRoot,
            Content = "This is a ContentDialog in WinUI 3!",
            CloseButtonText = "OK",
        };

        var parent = (sender as FrameworkElement)?.Parent as FrameworkElement;
        var requestedTheme = parent?.RequestedTheme;
        var actualTheme = parent?.ActualTheme;

        if ((sender as FrameworkElement)?.Parent is FrameworkElement { ActualTheme: ElementTheme theme })
        {
            dialog.RequestedTheme = theme;
        }

        _ = dialog.ShowAsync();
    }
}

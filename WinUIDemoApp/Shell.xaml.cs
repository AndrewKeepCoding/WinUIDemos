using CommunityToolkit.Mvvm.ComponentModel;
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
    }

    public ShellViewModel ViewModel { get; } = new();
}

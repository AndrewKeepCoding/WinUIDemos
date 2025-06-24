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

    private void TabView_AddTabButtonClick(TabView sender, object args)
    {
        sender.TabItems.Add(new TabViewItem
        {
            Header = $"Header {sender.TabItems.Count + 1}",
            Content = new TextBlock { Text = "This is a new tab." }
        });
    }
}

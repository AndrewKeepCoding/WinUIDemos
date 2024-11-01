using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System;

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

    private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        var pageType = args switch
        {
            { IsSettingsSelected: true } => typeof(SettingsPage),
            { SelectedItem: NavigationViewItem { Tag: string itemTag } } => Type.GetType(itemTag),
            _ => null
        };

        if (pageType is null)
        {
            return;
        }

        ContentFrame.Navigate(pageType);
    }
}

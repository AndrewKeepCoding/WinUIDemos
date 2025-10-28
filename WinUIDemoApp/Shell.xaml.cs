using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.WinUI.Controls;
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
    }

    public ShellViewModel ViewModel { get; } = new();

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        var element = new Button()
        {
            Content = $"{this.WrapPanelControl.Children.Count + 1}",
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch
        };

        this.WrapPanelControl.Children.Add(element);
    }

    private void RemoveButton_Click(object sender, RoutedEventArgs e)
    {
        if (this.WrapPanelControl.Children.Count == 0)
        {
            return;
        }

        this.WrapPanelControl.Children.RemoveAt(this.WrapPanelControl.Children.Count - 1);
    }

    private void OrientationButton_Click(object sender, RoutedEventArgs e)
    {
        this.WrapPanelControl.Orientation = this.WrapPanelControl.Orientation == Orientation.Horizontal
            ? Orientation.Vertical
            : Orientation.Horizontal;
    }

    private void StretchChildButton_Click(object sender, RoutedEventArgs e)
    {
        this.WrapPanelControl.StretchChild = this.WrapPanelControl.StretchChild == StretchChild.None
            ? StretchChild.Last
            : StretchChild.None;
    }
}

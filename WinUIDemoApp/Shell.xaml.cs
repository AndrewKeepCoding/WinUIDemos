using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace WinUIDemoApp;

public partial class Item(object content, int x, int y) : ObservableObject
{
    [ObservableProperty]
    private object _content = content;

    [ObservableProperty]
    private int _x = x;

    [ObservableProperty]
    private int _y = y;
}

public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Item> _items =
        [
            new Item("Item 1", 100, 50),
            new Item("Item 2", 200, 100),
            new Item("Item 3", 300, 150),
        ];
}

public sealed partial class Shell : Page
{
    public Shell()
    {
        InitializeComponent();

        CommunityToolkit.Labs.WinUI.CanvasView view = new();
    }

    public ShellViewModel ViewModel { get; } = new();
}

using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace WinUIDemoApp;


public partial class Item(int id, string name) : ObservableObject
{
    [ObservableProperty]
    public partial int Id { get; set; } = id;

    [ObservableProperty]
    public partial string Name { get; set; } = name;
}


public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    public partial ObservableCollection<Item> Items { get; set; } = [];

    public ShellViewModel()
    {
        for (int i = 0; i < 10; i++)
        {
            int id = i + 1;
            Items.Add(new Item(id, $"Item #{id}"));
        }
    }
}

public sealed partial class Shell : Page
{
    public Shell()
    {
        InitializeComponent();
    }

    public ShellViewModel ViewModel { get; } = new();
}

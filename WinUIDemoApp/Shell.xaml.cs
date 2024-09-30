using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace WinUIDemoApp;

public partial class Item(int id, string name) : ObservableObject
{
    [ObservableProperty]
    private int _id = id;

    [ObservableProperty]
    private string _name = name;
}

public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Item> _items = [];

    [RelayCommand]
    private void AddItem()
    {
        Items.Add(new Item(Items.Count + 1, $"Item {Items.Count + 1}"));
    }

    [RelayCommand]
    private void RemoveItem(Item item)
    {
        Items.Remove(item);
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

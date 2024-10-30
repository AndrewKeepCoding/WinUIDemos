using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace WinUIDemoApp;

public partial class ShellViewModel : ObservableObject
{
}

public sealed partial class Shell : Page
{
    private ObservableCollection<string> Items = [];

    public Shell()
    {
        InitializeComponent();
    }

    public ShellViewModel ViewModel { get; } = new();

    private void Grid_DragOver(object sender, DragEventArgs e)
    {
        e.AcceptedOperation = DataPackageOperation.Copy;
    }

    private async void Grid_Drop(object sender, DragEventArgs e)
    {
        Items.Clear();

        if (e.DataView.Contains(StandardDataFormats.StorageItems) is false)
        {
            return;
        }

        foreach (var item in await e.DataView.GetStorageItemsAsync())
        {
            Items.Add(item.Path);
        }
    }
}

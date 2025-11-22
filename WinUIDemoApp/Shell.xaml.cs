using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace WinUIDemoApp;

public partial class ShellViewModel : ObservableObject
{
    public ShellViewModel()
    {
        Items.CollectionChanged += Items_CollectionChanged;
    }

    public ObservableCollection<string> Items { get; } = [];

    public ObservableCollection<string> Logs { get; } = [];

    [ObservableProperty]
    public partial string? SelectedItem { get; set; }


    [ObservableProperty]
    public partial string? SelectedLog { get; set; }


    [RelayCommand]
    private void AddItem() => Items.Add($"Item {Items.Count}");

    [RelayCommand]
    private void RemoveItem(int index)
    {
        try
        {
            Items.RemoveAt(index);
        }
        catch (Exception exception)
        {
            Logs.Add($"Error removing item at index {index}: {exception.Message}");
        }
    }

    [RelayCommand]
    private void ReplaceItem(int index)
    {
        try
        {
            var item = Items[index];
            Items[index] = item + " (Updated)";
        }
        catch (Exception exception)
        {
            Logs.Add($"Error replacing item at index {index}: {exception.Message}");
        }
    }

    [RelayCommand]
    private void MoveItem()
    {
        try
        {
            if (SelectedItem is null)
            {
                Logs.Add("No item selected to move.");
                return;
            }

            int selectedItemIndex = Items.IndexOf(SelectedItem);
            Items.Move(selectedItemIndex, (selectedItemIndex + 1) % Items.Count);
        }
        catch (Exception exception)
        {
            Logs.Add($"Error moving item: {exception.Message}");
        }
    }

    [RelayCommand]
    private void ResetItems()
    {
        Items.Clear();
    }

    private void Items_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        string logEntry = $"Time: {DateTime.Now}" + Environment.NewLine;
        logEntry += $"Action: {e.Action}" + Environment.NewLine;

        logEntry += $"Old Starting Index: {e.OldStartingIndex}" + Environment.NewLine;

        if (e.OldItems is not null)
        {
            foreach (var oldItem in e.OldItems)
            {
                logEntry += $"Old Item: {oldItem}" + Environment.NewLine;
            }
        }
        else
        {
            logEntry += "Old Items: None" + Environment.NewLine;
        }

        logEntry += $"New Starting Index: {e.NewStartingIndex}" + Environment.NewLine;

        if (e.NewItems is not null)
        {
            foreach (var newItem in e.NewItems)
            {
                logEntry += $"New Item: {newItem}" + Environment.NewLine;
            }
        }
        else
        {
            logEntry += "New Items: None" + Environment.NewLine;
        }

        Logs.Add(logEntry);
        SelectedLog = logEntry;
    }
}

public sealed partial class Shell : Page
{
    public Shell()
    {
        InitializeComponent();
        LogsListView.Loaded += LogsListView_Loaded;
        LogsListView.Unloaded += LogsListView_Unloaded;
    }

    public ShellViewModel ViewModel { get; } = new();

    private void LogsListView_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (sender is not ListView listView || listView.ItemsPanelRoot is null)
            return;

        listView.ItemsPanelRoot.SizeChanged += ItemsPanelRoot_SizeChanged;
    }

    private void LogsListView_Unloaded(object sender, RoutedEventArgs e)
    {
        if (sender is not ListView listView || listView.ItemsPanelRoot is null)
            return;

        listView.ItemsPanelRoot?.SizeChanged -= ItemsPanelRoot_SizeChanged;
    }

    private void ItemsPanelRoot_SizeChanged(object sender, Microsoft.UI.Xaml.SizeChangedEventArgs e)
    {
        LogsListView.ScrollIntoView(ViewModel.SelectedLog);
    }
}

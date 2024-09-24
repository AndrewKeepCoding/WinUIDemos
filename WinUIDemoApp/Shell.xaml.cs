using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace WinUIDemoApp;

public partial class ShellViewModel : ObservableObject
{
    public ObservableCollection<string> Items { get; private set; } = ["Item#1", "Item#2", "Item#3"];

    private DispatcherQueue DispatcherQueue { get; } = DispatcherQueue.GetForCurrentThread();


    [RelayCommand]
    private async Task OnLoaded()
    {
        //var dispatcherQueue = DispatcherQueue.GetForCurrentThread();

        DispatcherQueue.TryEnqueue(
            DispatcherQueuePriority.Low,
            () =>
            {
                Items.Add("Item#4");
            });

        //DispatcherQueue.TryEnqueue(
        //    DispatcherQueuePriority.Normal,
        //    () =>
        //    {
        //        Items.Add("Item#5");
        //    });
        await DispatcherQueue.EnqueueAsync(() =>
        {
            Items.Add("Item#5");
        },
        DispatcherQueuePriority.Low);

        DispatcherQueue.TryEnqueue(
            DispatcherQueuePriority.High,
            () =>
            {
                Items.Add("Item#6");
            });

        //await Task.Delay(500);

        //await Task.Run(async () =>
        //{
        //    await DispatcherQueue.EnqueueAsync(() =>
        //    {
        //        Items.Clear();
        //    },
        //    DispatcherQueuePriority.High);
        //});
    }

}


public sealed partial class Shell : Page
{
    public Shell()
    {
        InitializeComponent();
        Loaded += Shell_Loaded;
    }

    private async void Shell_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        await ViewModel.LoadedCommand.ExecuteAsync(null);
        //await Task.Run(async () =>
        //{
        //    await ViewModel.LoadedCommand.ExecuteAsync(null);
        //});

    }

    private ShellViewModel ViewModel { get; } = new();
}

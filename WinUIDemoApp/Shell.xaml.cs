using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;

namespace WinUIDemoApp;

public class ClipboardHistoryItemTemplateSelector : DataTemplateSelector
{
    public DataTemplate DefaultTemplate { get; set; } = new();

    public DataTemplate? TextTemplate { get; set; }

    public DataTemplate? ImageTemplate { get; set; }

    protected override DataTemplate? SelectTemplateCore(object item, DependencyObject container)
    {
        return item switch
        {
            string => TextTemplate,
            WriteableBitmap => ImageTemplate,
            _ => base.SelectTemplateCore(item, container),
        };
    }

    protected override DataTemplate SelectTemplateCore(object item)
    {
        return base.SelectTemplateCore(item);
    }
}

public class ClipboardItem(object content, string id, DateTimeOffset timestamp)
{
    public object Content { get; } = content;

    public string Id { get; } = id;

    public DateTimeOffset Timestamp { get; } = timestamp;
}

public sealed partial class Shell : Page
{
    public Shell()
    {
        InitializeComponent();
        Loaded += Shell_Loaded;
        Clipboard.ContentChanged += Clipboard_ContentChanged;
    }

    private static async Task<ClipboardItem?> CreateClipboardItem(ClipboardHistoryItem clipboardHistoryItem)
    {
        if (clipboardHistoryItem.Content.Contains(StandardDataFormats.Text) is true)
        {
            var text = await clipboardHistoryItem.Content.GetTextAsync();
            return new ClipboardItem(text, clipboardHistoryItem.Id, clipboardHistoryItem.Timestamp);
        }

        if (clipboardHistoryItem.Content.Contains(StandardDataFormats.Bitmap) is true)
        {
            var stream = await clipboardHistoryItem.Content.GetBitmapAsync();
            using var imageStream = await stream.OpenReadAsync();
            var writeableBitmap = await StreamToWriteableBitmap(imageStream);
            return new ClipboardItem(writeableBitmap, clipboardHistoryItem.Id, clipboardHistoryItem.Timestamp);
        }

        return null;
    }

    private static async Task<WriteableBitmap> StreamToWriteableBitmap(IRandomAccessStream stream)
    {
        var decoder = await BitmapDecoder.CreateAsync(stream);
        var writeableBitmap = new WriteableBitmap((int)decoder.PixelWidth, (int)decoder.PixelHeight);
        stream.Seek(0);
        await writeableBitmap.SetSourceAsync(stream);
        return writeableBitmap;
    }

    public ObservableCollection<ClipboardItem> ClipboardItems { get; private set; } = [];

    private async void Shell_Loaded(object sender, RoutedEventArgs e)
    {
        await SyncClipboardItems();
    }

    private async Task SyncClipboardItems()
    {
        ClipboardItems.Clear();

        if (await Clipboard.GetHistoryItemsAsync() is not { } clipboardHistoryItems)
        {
            return;
        }

        foreach (var clipboardHistoryItem in clipboardHistoryItems.Items)
        {
            if (await CreateClipboardItem(clipboardHistoryItem) is not { } clipboardItem)
            {
                continue;
            }

            ClipboardItems.Add(clipboardItem);
            //break;
        }

        ClipboardHistoryItemsListView.ItemsSource = ClipboardItems;
    }

    private async void Clipboard_ContentChanged(object? sender, object e)
    {
        await SyncClipboardItems();
    }

    private void CopyTextButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(CopySourceTextBox.Text) is true)
        {
            return;
        }

        var dataPackage = new DataPackage();
        dataPackage.SetText(CopySourceTextBox.Text);
        Clipboard.SetContent(dataPackage);
    }
}

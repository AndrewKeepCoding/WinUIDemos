using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Data.Pdf;
using Windows.Storage.Streams;
using Windows.Storage;
using System;
using System.Linq;

namespace WinUIDemoApp;

public sealed partial class Shell : Page
{
    public Shell()
    {
        InitializeComponent();
        Unloaded += Shell_Unloaded;
    }

    public ObservableCollection<BitmapImage> Pages { get; } = [];

    private void Shell_Unloaded(object sender, RoutedEventArgs e)
    {
        Pages.Clear();
    }

    private void DroppingSpaceGrid_DragOver(object sender, DragEventArgs e)
    {
        e.AcceptedOperation = DataPackageOperation.Copy;
        e.DragUIOverride.IsCaptionVisible = false;
        e.DragUIOverride.IsGlyphVisible = false;
    }

    // TODO: The app cannot be closed after page scrolling.
    private async void DroppingSpaceGrid_Drop(object sender, DragEventArgs e)
    {
        if ((await e.DataView.GetStorageItemsAsync()).FirstOrDefault() is not StorageFile file ||
            file.FileType is not ".pdf")
        {
            return;
        }

        PdfDocument pdfDocument = await PdfDocument.LoadFromFileAsync(file);

        Pages.Clear();

        for (int i = 0; i < pdfDocument.PageCount; i++)
        {
            var bitmapImage = new BitmapImage();

            using PdfPage pdfPage = pdfDocument.GetPage((uint)i);
            using var stream = new InMemoryRandomAccessStream();
            await pdfPage.RenderToStreamAsync(stream);
            await bitmapImage.SetSourceAsync(stream);

            Pages.Add(bitmapImage);
        }
    }
}

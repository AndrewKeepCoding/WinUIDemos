using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI;
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
        Unloaded += (sender, e) =>
        {
            CanvasControl.RemoveFromVisualTree();
            CanvasControl = null;
        };
    }

    public ShellViewModel ViewModel { get; } = new();

    private void CanvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
    {
        var drawingSession = args.DrawingSession;
        drawingSession.DrawLine(
            x0: 0,
            y0: 0,
            x1: (float)sender.ActualWidth,
            y1: (float)sender.ActualHeight,
            Colors.SkyBlue);
    }
}

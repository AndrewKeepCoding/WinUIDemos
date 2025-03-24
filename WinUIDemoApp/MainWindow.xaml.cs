using Microsoft.UI.Xaml;

namespace WinUIDemoApp;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        SetTitleBar(TitleBarControl);
        ExtendsContentIntoTitleBar = true;
        AppWindow.SetIcon("Assets/GalleryIcon.ico");
    }
}

using Microsoft.UI.Xaml.Controls;

namespace WinUIDemoApp;

public sealed partial class Shell : Page
{
    public Shell()
    {
        InitializeComponent();

        DotNet7WinUILibrary.Class1 dotNet7WinUILibrary = new();
        //DotNet8WinUILibrary.Class1 dotNet8WinUILibrary = new();
        //DotNet9WinUILibrary.Class1 dotNet9WinUILibrary = new();
    }
}

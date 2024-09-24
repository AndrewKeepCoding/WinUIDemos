using Microsoft.UI.Xaml.Controls;

namespace WinUIDemoApp;
public sealed partial class Shell : Page
{
    public Shell()
    {
        InitializeComponent();

        Content = new MainPage();
        //Content = new MainPage(new MainPageViewModel());
    }
}

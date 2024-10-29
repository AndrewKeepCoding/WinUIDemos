using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace WinUIDemoApp;

public partial class Member : ObservableObject
{
    [ObservableProperty]
    private int _id;

    [ObservableProperty]
    private string _name = string.Empty;
}

public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Member> _members = [];

    public ShellViewModel()
    {
        for (int i = 0; i < 1000; i++)
        {
            Members.Add(new Member { Id = i, Name = $"Item {i}" });
        }
    }
}

public sealed partial class Shell : Page
{
    public Shell()
    {
        InitializeComponent();
    }

    public ShellViewModel ViewModel { get; } = new();

    private void GoToMemberButton_Click(object sender, RoutedEventArgs e)
    {
        int memberIndex = (int)MemberIndexNumberBox.Value;
        var memberItem = MemberItemsRepeater.GetOrCreateElement(memberIndex);
        memberItem.UpdateLayout();
        memberItem.StartBringIntoView();
    }
}

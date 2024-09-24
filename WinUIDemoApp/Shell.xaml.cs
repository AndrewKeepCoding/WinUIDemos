using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace WinUIDemoApp;

public partial class Parent(string name) : ObservableObject
{
    [ObservableProperty]
    private string _name = name;

    [ObservableProperty]
    private ObservableCollection<Child> _children = [];
}

public partial class Child(string name) : ObservableObject
{
    [ObservableProperty]
    private string _name = name;
}

public partial class ShellViewModel : ObservableObject
{

    [ObservableProperty]
    private ObservableCollection<Parent> _parents = [];

    public ShellViewModel()
    {
        var parent1 = new Parent("Parent 1");
        parent1.Children.Add(new Child("Child 1"));

        var parent2 = new Parent("Parent 2");
        parent2.Children.Add(new Child("Child 1"));
        parent2.Children.Add(new Child("Child 2"));

        var parent3 = new Parent("Parent 3");
        parent3.Children.Add(new Child("Child 1"));
        parent3.Children.Add(new Child("Child 2"));
        parent3.Children.Add(new Child("Child 3"));

        Parents.Add(parent1);
        Parents.Add(parent2);
        Parents.Add(parent3);
    }
}

public class TreeViewItemTemplateSelector : DataTemplateSelector
{
    public DataTemplate ParentItemTemplate { get; set; } = new();

    public DataTemplate ChildItemTemplate { get; set; } = new();

    protected override DataTemplate SelectTemplateCore(object item)
    {
        return item switch
        {
            Parent => ParentItemTemplate,
            Child => ChildItemTemplate,
            _ => base.SelectTemplateCore(item)
        };
    }
}

public sealed partial class Shell : Page
{
    public Shell()
    {
        InitializeComponent();
    }

    public ShellViewModel ViewModel { get; } = new();
}

using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WinUIDemoApp;

public interface INode
{
    object Content { get; }
}

public class LeafNode(object content) : INode
{
    public object Content { get; } = content;
}

public class BranchNode(object content) : INode
{
    public object Content { get; } = content;

    public ObservableCollection<INode> Children { get; } = [];
}

public partial class StringToDataTemplateDictionary : Dictionary<string, DataTemplate>;

public partial class TreeViewItemTemplateSelector : DataTemplateSelector
{
    public StringToDataTemplateDictionary DataTemplates { get; } = [];

    public DataTemplate DefaultDataTemplate { get; set; } = new();

    protected override DataTemplate SelectTemplateCore(object? item)
    {
        if (item?.GetType().Name is string key &&
            DataTemplates.TryGetValue(key, out DataTemplate? dataTemplate) is true)
        {
            return dataTemplate;
        }

        return DefaultDataTemplate;
    }

    protected override DataTemplate? SelectTemplateCore(object? item, DependencyObject container)
    {
        return SelectTemplateCore(item);
    }
}

public partial class ShellViewModel : ObservableObject
{
    public ObservableCollection<INode> Nodes { get; } = [];

    public ShellViewModel()
    {
        Nodes.Add(new BranchNode("Branch 1")
        {
            Children =
            {
                new LeafNode("Leaf 1.1"),
                new LeafNode("Leaf 1.2"),
                new BranchNode("Branch 1.3")
                {
                    Children =
                    {
                        new LeafNode("Leaf 1.3.1"),
                        new LeafNode("Leaf 1.3.2")
                    }
                }
            }
        });
        Nodes.Add(new BranchNode("Branch 2")
        {
            Children =
            {
                new LeafNode("Leaf 2.1"),
                new LeafNode("Leaf 2.2")
            }
        });
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

using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace WinUIDemoApp;

public partial class Section : ObservableObject
{
    [ObservableProperty]
    public partial string Header { get; set; }

    [ObservableProperty]
    public partial string Description { get; set; }

    [ObservableProperty]
    public partial string Content { get; set; }

    [ObservableProperty]
    public partial ObservableCollection<Section> SubSections { get; set; } = [];
}

public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    public partial ObservableCollection<Section> Sections { get; set; } = [];

    public ShellViewModel()
    {
        Sections.Add(new Section
        {
            Header = "Section 1",
            Description = "Description 1",
            Content = "Content 1",
            SubSections =
            {
                new Section
                {
                    Header = "SubSection 1",
                    Description = "SubDescription 1",
                    Content = "SubContent 1"
                },
                new Section
                {
                    Header = "SubSection 2",
                    Description = "SubDescription 2",
                    Content = "SubContent 2"
                }
            }
        });
        Sections.Add(new Section
        {
            Header = "Section 2",
            Description = "Description 2",
            Content = "Content 2",
            SubSections =
            {
                new Section
                {
                    Header = "SubSection 3",
                    Description = "SubDescription 3",
                    Content = "SubContent 3"
                },
                new Section
                {
                    Header = "SubSection 4",
                    Description = "SubDescription 4",
                    Content = "SubContent 4"
                }
            }
        });
    }
}

public partial class SectionContentTemplateSelector : DataTemplateSelector
{
    public DataTemplate? DataTemplate { get; set; }

    protected override DataTemplate SelectTemplateCore(object item)
    {
        return DataTemplate!;
    }

    protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
    {
        return SelectTemplateCore(item);
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
